﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO.MemoryMappedFiles;
using irsdkSharp.Enums;
using irsdkSharp.Models;
using System.Threading;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using irsdkSharp.Extensions;
using System.IO;

namespace irsdkSharp
{
    public class IrSdk
    {
        private readonly Encoding _encoding;
        private readonly char[] trimChars = { '\0' };
        private bool IsStarted = false;

        //VarHeader offsets
        public const int VarOffsetOffset = 4;
        public const int VarCountOffset = 8;
        public const int VarNameOffset = 16;
        public const int VarDescOffset = 48;
        public const int VarUnitOffset = 112;

        MemoryMappedFile iRacingFile;
        protected MemoryMappedViewAccessor FileMapView;
        protected Dictionary<string, VarHeader> VarHeaders;

        //events
        public event Action OnDataChanged;
        public event Action OnConnected;
        public event Action OnDisconnected;

        public static MemoryMappedViewAccessor GetFileMapView(IrSdk irSdk)
        {
            return irSdk.FileMapView;
        }

        public static Dictionary<string, VarHeader> GetVarHeaders(IrSdk irSdk)
        {
            return irSdk.VarHeaders;
        }

        public IRacingSdkHeader Header = null;

        private AutoResetEvent _gameLoopEvent;
        private IntPtr _hEvent;
        private readonly ILogger<IrSdk> _logger;
        private readonly CancellationTokenSource _waitValidDataLoopCancellation;
        private readonly CancellationToken _waitValidDataLoopCancellationToken;


        private readonly CancellationTokenSource _connectionLoopCancellation;
        private readonly CancellationToken _connectionLoopCancellationToken;

        public IrSdk()
        {
            // Register CP1252 encoding
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _encoding = Encoding.GetEncoding(1252);

            _waitValidDataLoopCancellation = new CancellationTokenSource();
            _waitValidDataLoopCancellationToken = _waitValidDataLoopCancellation.Token;
            Task.Run(WaitValidDataLoop, _waitValidDataLoopCancellationToken);

            _connectionLoopCancellation = new CancellationTokenSource();
            _connectionLoopCancellationToken = _connectionLoopCancellation.Token;
            Task.Run(ConnectionLoop, _waitValidDataLoopCancellationToken);
        }

        public IrSdk(ILogger<IrSdk> logger) : this()
        {
            _logger = logger;
        }

        public IrSdk(MemoryMappedViewAccessor accessor)
        {
            // Register CP1252 encoding
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _encoding = Encoding.GetEncoding(1252);

            FileMapView = accessor;

            Header = new IRacingSdkHeader(FileMapView);
            GetVarHeaders();
            IsStarted = true;
        }

        private void ConnectionLoop()
        {
            while (true)
            {
                if (_connectionLoopCancellationToken.IsCancellationRequested) break;
                try
                {
                    if (!IsConnected() && !IsStarted && Header == null)
                    {
                        if (iRacingFile == null)
                        {
                            iRacingFile = MemoryMappedFile.OpenExisting(Constants.MemMapFileName);
                            FileMapView = iRacingFile.CreateViewAccessor();

                            _hEvent = OpenEvent(Constants.DesiredAccess, false, Constants.DataValidEventName);
                            _gameLoopEvent = new AutoResetEvent(false)
                            {
                                SafeWaitHandle = new SafeWaitHandle(_hEvent, true)
                            };

                            IsStarted = true;
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                    IsStarted = false;
                    Header = null;
                    VarHeaders = null;
                    _logger?.LogDebug($"Not Connected {ex.Message}");
                }
                finally
                {
                    Thread.Sleep(1000);
                }

            }
        }

        private void WaitValidDataLoop()
        {
            while (true)
            {
                if (_waitValidDataLoopCancellationToken.IsCancellationRequested) break;
                if (IsStarted)
                {
                    try
                    {
                        var valid = _gameLoopEvent.WaitOne(1000);

                        if (valid)
                        {
                            if (Header == null)
                            {
                                Header = new IRacingSdkHeader(FileMapView);
                                OnConnected?.Invoke();
                            }
                            if (VarHeaders == null)
                            { 
                                GetVarHeaders();
                            }
                            OnDataChanged?.Invoke();
                        }
                        else
                        {
                            if (Header != null)
                            {
                                Header = null;
                                OnDisconnected?.Invoke();
                            }
                            if (VarHeaders != null)
                            {
                                VarHeaders = null;
                            }
                        }
                    }
                    catch
                    {
                    }
                } 
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }

        private void GetVarHeaders()
        {
            VarHeaders = new Dictionary<string, VarHeader>(Header.VarCount);
            for (int i = 0; i < Header.VarCount; i++)
            {
                int type = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeader.Size)));
                int offset = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarOffsetOffset));
                int count = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarCountOffset));
                byte[] name = new byte[Constants.MaxString];
                byte[] desc = new byte[Constants.MaxDesc];
                byte[] unit = new byte[Constants.MaxString];
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarNameOffset), name, 0, Constants.MaxString);
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarDescOffset), desc, 0, Constants.MaxDesc);
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarUnitOffset), unit, 0, Constants.MaxString);
                string nameStr = _encoding.GetString(name).TrimEnd(trimChars);
                string descStr = _encoding.GetString(desc).TrimEnd(trimChars);
                string unitStr = _encoding.GetString(unit).TrimEnd(trimChars);
                var header = new VarHeader(type, offset, count, nameStr, descStr, unitStr);
                VarHeaders[header.Name] = header;  
            }
        }

        public object GetData(string name)
        {
            if (!IsStarted || Header == null) return null;
            if (!VarHeaders.TryGetValue(name, out var requestedHeader)) return null;

            int varOffset = requestedHeader.Offset;
            int count = requestedHeader.Count;

            switch (requestedHeader.Type)
            {
                case VarType.irChar:
                    {
                        byte[] data = new byte[count];
                        FileMapView.ReadArray(Header.Offset + varOffset, data, 0, count);
                        return _encoding.GetString(data).TrimEnd(trimChars);
                    }
                case VarType.irBool:
                    {
                        if (count > 1)
                        {
                            bool[] data = new bool[count];
                            FileMapView.ReadArray(Header.Offset + varOffset, data, 0, count);
                            return data;
                        }
                        else
                        {
                            return FileMapView.ReadBoolean(Header.Offset + varOffset);
                        }
                    }
                case VarType.irInt:
                case VarType.irBitField:
                    {
                        if (count > 1)
                        {
                            int[] data = new int[count];
                            FileMapView.ReadArray(Header.Offset + varOffset, data, 0, count);
                            return data;
                        }
                        else
                        {
                            return FileMapView.ReadInt32(Header.Offset + varOffset);
                        }
                    }
                case VarType.irFloat:
                    {
                        if (count > 1)
                        {
                            float[] data = new float[count];
                            FileMapView.ReadArray(Header.Offset + varOffset, data, 0, count);
                            return data;
                        }
                        else
                        {
                            return FileMapView.ReadSingle(Header.Offset + varOffset);
                        }
                    }
                case VarType.irDouble:
                    {
                        if (count > 1)
                        {
                            double[] data = new double[count];
                            FileMapView.ReadArray(Header.Offset + varOffset, data, 0, count);
                            return data;
                        }
                        else
                        {
                            return FileMapView.ReadDouble(Header.Offset + varOffset);
                        }
                    }
                default: return null;
            }
        }

        public string GetSessionInfo() =>
            (IsStarted && Header != null) switch
            {
                true => FileMapView.ReadString(Header.SessionInfoOffset, Header.SessionInfoLength),
                _ => null
            };

        public bool IsConnected()
        {
            if (IsStarted && Header != null)
            {
                return (Header.Status & 1) > 0;
            }
            return false;
        }

        IntPtr GetBroadcastMessageID()
        {
            return RegisterWindowMessage(Constants.BroadcastMessageName);
        }

        IntPtr GetPadCarNumID()
        {
            return RegisterWindowMessage(Constants.PadCarNumName);
        }

        public int BroadcastMessage(BroadcastMessageTypes msg, int var1, int var2, int var3)
        {
            return BroadcastMessage(msg, var1, MakeLong((short)var2, (short)var3));
        }

        public int BroadcastMessage(BroadcastMessageTypes msg, int var1, int var2)
        {
            IntPtr msgId = GetBroadcastMessageID();
            IntPtr hwndBroadcast = IntPtr.Add(IntPtr.Zero, 0xffff);
            IntPtr result = IntPtr.Zero;
            if (msgId != IntPtr.Zero)
            {
                result = PostMessage(hwndBroadcast, msgId.ToInt32(), MakeLong((short)msg, (short)var1), var2);
            }
            return result.ToInt32();
        }

        [DllImport("user32.dll")]
        private static extern IntPtr RegisterWindowMessage(string lpProcName);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr OpenEvent(UInt32 dwDesiredAccess, Boolean bInheritHandle, String lpName);

        public int MakeLong(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }

        public static short HiWord(int dword)
        {
            return (short)(dword >> 16);
        }

        public static short LoWord(int dword)
        {
            return (short)dword;
        }
    }
}