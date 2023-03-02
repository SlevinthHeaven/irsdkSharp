using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO.MemoryMappedFiles;
using irsdkSharp.Enums;
using irsdkSharp.Models;
using System.Threading;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using irsdkSharp.Extensions;
using System.IO;

namespace irsdkSharp
{
    public class IrSdk
    {
        private readonly Encoding _encoding;
        private readonly ILogger<IrSdk>? _logger;
        private readonly IrSdkOptions _options;
        
        private MemoryMappedFile? _iRacingFile;
        private CancellationTokenSource? _loopCancellationSource;

        #region Properties
        /// <summary>
        /// Options for the SDK.
        /// </summary>
        public IrSdkOptions Options => _options;
        
        /// <summary>
        /// If the data loop has been started.
        /// </summary>
        public bool IsStarted => _loopCancellationSource != null && !_loopCancellationSource.IsCancellationRequested;

        /// <summary>
        /// If the sim is connected.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (Header != null)
                    return (Header.Status & 1) > 0;

                return false;
            }
        }

        public IRacingSdkHeader? Header { get; private set; } = null;
        public MemoryMappedViewAccessor? FileMapView { get; private set; }
        public Dictionary<string, VarHeader>? VarHeaders { get; private set; }
        #endregion

        #region Events
        public event EventHandler? OnDataChanged;
        public event EventHandler? OnConnected;
        public event EventHandler? OnDisconnected;
        #endregion

        public IrSdk(IrSdkOptions? options, ILogger<IrSdk>? logger, bool autoStart = false)
        {
            // Register CP1252 encoding
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _encoding = Encoding.GetEncoding(1252);
            
            _options = options ?? IrSdkOptions.Default;
            
            if (logger != null)
                _logger = logger;
            
            if (autoStart)
                Start();
        }
        
        public IrSdk(bool autoStart = false) : this(null, null, autoStart)
        {
        }
        
        public IrSdk(IrSdkOptions? options, bool autoStart = false) : this(options, null, autoStart)
        {
        }

        public IrSdk(ILogger<IrSdk> logger, bool autoStart = false) : this(null, logger, autoStart)
        {
        }

        public IrSdk(MemoryMappedViewAccessor accessor) : this(null, null, false)
        {
            FileMapView = accessor;

            Header = new IRacingSdkHeader(FileMapView);
            GetVarHeaders();
        }

        public void Start(int updateFrequency)
        {
            Options.UpdateFrequency = updateFrequency;
            Start();
        }
        
        public void Start()
        {
            if (IsStarted) 
                return;
            
            _loopCancellationSource = new();
            
            Task.Factory.StartNew(() => 
                Loop(_loopCancellationSource.Token), _loopCancellationSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
        
        public void Stop()
        {
            _loopCancellationSource?.Cancel();
            _loopCancellationSource?.Dispose();
            _loopCancellationSource = null;
        }
        
        private async void Loop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (!IsConnected && _iRacingFile == null)
                {
                    try
                    {
                        _iRacingFile = MemoryMappedFile.OpenExisting(IrSdkConstants.MemMapFileName);
                        FileMapView = _iRacingFile.CreateViewAccessor();
                    }
                    catch (FileNotFoundException ex)
                    {
                        if (Header != null)
                            OnDisconnected?.Invoke(this, EventArgs.Empty);
                        
                        Header = null;
                        VarHeaders = null;
                        
                        _logger?.LogWarning($"Not connected to iRacing ({ex.Message})");
                        
                        await SafeDelay(Options.CheckConnectionDelay, token);
                        continue;
                    }
                }
                
                if (Header == null)
                {
                    Header = new IRacingSdkHeader(FileMapView);
                    OnConnected?.Invoke(this, EventArgs.Empty);
                }
                
                if (VarHeaders == null)
                { 
                    GetVarHeaders();
                }
                
                OnDataChanged?.Invoke(this, EventArgs.Empty);
                
                await SafeDelay(Options.UpdateDelay, token);
            }
        }
        
        private static async Task SafeDelay(int delay, CancellationToken token)
        {
            try
            {
                await Task.Delay(delay, token);
            }
            catch (OperationCanceledException)
            {
                // Ignore
            }
        }

        private void GetVarHeaders()
        {
            VarHeaders = new Dictionary<string, VarHeader>(Header.VarCount);
            for (int i = 0; i < Header.VarCount; i++)
            {
                int type = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeader.Size)));
                int offset = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeader.Size) + IrSdkConstants.VarOffsetOffset));
                int count = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeader.Size) + IrSdkConstants.VarCountOffset));
                byte[] name = new byte[IrSdkConstants.MaxString];
                byte[] desc = new byte[IrSdkConstants.MaxDesc];
                byte[] unit = new byte[IrSdkConstants.MaxString];
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeader.Size) + IrSdkConstants.VarNameOffset), name, 0, IrSdkConstants.MaxString);
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeader.Size) + IrSdkConstants.VarDescOffset), desc, 0, IrSdkConstants.MaxDesc);
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeader.Size) + IrSdkConstants.VarUnitOffset), unit, 0, IrSdkConstants.MaxString);
                string nameStr = _encoding.GetString(name).TrimEnd(IrSdkConstants.TrimChar);
                string descStr = _encoding.GetString(desc).TrimEnd(IrSdkConstants.TrimChar);
                string unitStr = _encoding.GetString(unit).TrimEnd(IrSdkConstants.TrimChar);
                var header = new VarHeader(type, offset, count, nameStr, descStr, unitStr);
                VarHeaders[header.Name] = header;  
            }
        }

        public object GetData(string name)
        {
            if (!IsConnected) return null;
            if (!VarHeaders.TryGetValue(name, out var requestedHeader)) return null;

            int varOffset = requestedHeader.Offset;
            int count = requestedHeader.Count;

            switch (requestedHeader.Type)
            {
                case VarType.irChar:
                    {
                        byte[] data = new byte[count];
                        FileMapView.ReadArray(Header.Offset + varOffset, data, 0, count);
                        return _encoding.GetString(data).TrimEnd(IrSdkConstants.TrimChar);
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
            (IsConnected) switch
            {
                true => FileMapView.ReadString(Header.SessionInfoOffset, Header.SessionInfoLength),
                _ => null
            };

        IntPtr GetBroadcastMessageID()
        {
            return RegisterWindowMessage(IrSdkConstants.BroadcastMessageName);
        }

        IntPtr GetPadCarNumID()
        {
            return RegisterWindowMessage(IrSdkConstants.PadCarNumName);
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

        private static int MakeLong(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }
    }
}