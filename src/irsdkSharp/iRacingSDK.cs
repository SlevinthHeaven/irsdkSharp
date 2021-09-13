using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO.MemoryMappedFiles;
using irsdkSharp.Enums;
using irsdkSharp.Models;
using System.Threading;
using System.Text;

namespace irsdkSharp
{
    public class IRacingSDK
    {
        private readonly Encoding _encoding;
        private char[] trimChars = { '\0' };

        //VarHeader offsets
        public const int VarOffsetOffset = 4;
        public const int VarCountOffset = 8;
        public const int VarNameOffset = 16;
        public const int VarDescOffset = 48;
        public const int VarUnitOffset = 112;


        public bool IsInitialized = false;
        private MemoryMappedFile _iRacingFile;
        internal MemoryMappedViewAccessor FileMapView;

        public static MemoryMappedViewAccessor GetFileMapView(IRacingSDK racingSDK)
        {
            return racingSDK.FileMapView;
        }

        public IRacingSdkHeader Header = null;

        public Dictionary<string, VarHeader> Headers;

        public Data Data { get; private set; }

        public IRacingSDK()
        {
            // Register CP1252 encoding
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _encoding = Encoding.GetEncoding(1252);
        }

        public IRacingSDK(MemoryMappedViewAccessor accessor)
        {
            // Register CP1252 encoding
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _encoding = Encoding.GetEncoding(1252);
            FileMapView = accessor;
        }

        public bool Startup(bool openWaitHandle = true)
        {
            if (IsInitialized) return true;

            try
            {
                if (openWaitHandle)
                {
                    _iRacingFile = MemoryMappedFile.OpenExisting(Constants.MemMapFileName);
                    FileMapView = _iRacingFile.CreateViewAccessor();
                    using var hEvent = EventWaitHandle.OpenExisting(Constants.DataValidEventName);
                    if (!hEvent.WaitOne(TimeSpan.FromSeconds(1)))
                    {
                        Console.WriteLine("Failed to wait on DataValid Event.");
                        return false;
                    }
                }

                Header = new IRacingSdkHeader(FileMapView);
                Headers = new Dictionary<string, VarHeader>(Header.VarCount);
                GetVarHeaders();
                IsInitialized = true;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void GetVarHeaders()
        {
            for (int i = 0; i < Header.VarCount; i++)
            {
                int type = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeader.Size)));
                int offset = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarOffsetOffset));
                int count = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarCountOffset));
                byte[] name = new byte[Constants.MaxString];
                byte[] desc = new byte[Constants.MaxDesc];
                byte[] unit = new byte[Constants.MaxString];
                FileMapView.ReadArray(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarNameOffset), name, 0, Constants.MaxString);
                FileMapView.ReadArray(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarDescOffset), desc, 0, Constants.MaxDesc);
                FileMapView.ReadArray(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarUnitOffset), unit, 0, Constants.MaxString);
                string nameStr = _encoding.GetString(name).TrimEnd(trimChars);
                string descStr = _encoding.GetString(desc).TrimEnd(trimChars);
                string unitStr = _encoding.GetString(unit).TrimEnd(trimChars);
                var header = new VarHeader(type, offset, count, nameStr, descStr, unitStr);
                Headers[header.Name] = header;
                Data = new Data(this);
            }
        }

        public object GetData(string name)
        {
            if (!IsInitialized || Header == null) return null;
            if (!Headers.TryGetValue(name, out var requestedHeader)) return null;

            int varOffset = requestedHeader.Offset;
            int count = requestedHeader.Count;

            switch (requestedHeader.Type)
            {
                case VarType.irChar:
                    {
                        byte[] data = new byte[count];
                        FileMapView.ReadArray<byte>(Header.Offset + varOffset, data, 0, count);
                        return _encoding.GetString(data).TrimEnd(new char[] { '\0' });
                    }
                case VarType.irBool:
                    {
                        if (count > 1)
                        {
                            bool[] data = new bool[count];
                            FileMapView.ReadArray<bool>(Header.Offset + varOffset, data, 0, count);
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
                            FileMapView.ReadArray<int>(Header.Offset + varOffset, data, 0, count);
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
                            FileMapView.ReadArray<float>(Header.Offset + varOffset, data, 0, count);
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
                            FileMapView.ReadArray<double>(Header.Offset + varOffset, data, 0, count);
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
            (IsInitialized && Header != null) switch
            {
                true => FileMapView.ReadString(Header.SessionInfoOffset, Header.SessionInfoLength, 2400),
                _ => null
            };

        public bool IsConnected()
        {
            if (IsInitialized && Header != null)
            {
                return (Header.Status & 1) > 0;
            }
            return false;
        }

        public void Shutdown()
        {
            IsInitialized = false;
            Header = null;
            FileMapView?.Dispose();
            _iRacingFile?.Dispose();
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
        private static extern IntPtr PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public int MakeLong(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }
    }
}