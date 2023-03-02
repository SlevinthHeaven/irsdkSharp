using System;
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
        public const int DefaultUpdateFrequency = 10;
        private const char TrimChar = '\0';
        
        private readonly Encoding _encoding;
        private readonly ILogger<IrSdk>? _logger;
        
        private int _updateFrequency = DefaultUpdateFrequency;
        private MemoryMappedFile _iRacingFile;
        private CancellationTokenSource? _loopCancellationSource;

        #region Properties
        /// <summary>
        /// Update delay in milliseconds based on the <see cref="UpdateFrequency"/>
        /// </summary>
        protected int WaitDelay => (int)Math.Round(1000 / (double)UpdateFrequency);
        
        /// <summary>
        /// Updates per second (1-60)
        /// </summary>
        public int UpdateFrequency
        {
            get => _updateFrequency;
            set
            {
                if (value <= 0 || value > 60)
                    throw new ArgumentOutOfRangeException(nameof(value), 
                        "The UpdateFrequency must be between 1 and 60");
                
                _updateFrequency = value;
            }
        }
        
        /// <summary>
        /// The delay between a connection check when the sim is not connected
        /// </summary>
        public int CheckConnectionDelay { get; set; } = 5000;
        
        /// <summary>
        /// If the data loop has been started
        /// </summary>
        public bool IsStarted => _loopCancellationSource != null && !_loopCancellationSource.IsCancellationRequested;
        
        public IRacingSdkHeader? Header { get; private set; } = null;
        public MemoryMappedViewAccessor? FileMapView { get; private set; }
        public Dictionary<string, VarHeader>? VarHeaders { get; private set; }
        #endregion

        //VarHeader offsets
        public const int VarOffsetOffset = 4;
        public const int VarCountOffset = 8;
        public const int VarNameOffset = 16;
        public const int VarDescOffset = 48;
        public const int VarUnitOffset = 112;

        #region Events
        public event EventHandler? OnDataChanged;
        public event EventHandler? OnConnected;
        public event EventHandler? OnDisconnected;
        #endregion

        public IrSdk(bool autoStart = false)
        {
            // Register CP1252 encoding
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _encoding = Encoding.GetEncoding(1252);
            
            if (autoStart)
                Start();
        }

        public IrSdk(ILogger<IrSdk> logger, bool autoStart = false) : this(autoStart)
        {
            _logger = logger;
        }

        public IrSdk(MemoryMappedViewAccessor accessor) : this(false)
        {
            FileMapView = accessor;

            Header = new IRacingSdkHeader(FileMapView);
            GetVarHeaders();
        }

        public void Start(int updateFrequency)
        {
            UpdateFrequency = updateFrequency;
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
                if (!IsConnected() && _iRacingFile == null)
                {
                    try
                    {
                        _iRacingFile = MemoryMappedFile.OpenExisting(Constants.MemMapFileName);
                        FileMapView = _iRacingFile.CreateViewAccessor();
                    }
                    catch (FileNotFoundException ex)
                    {
                        if (Header != null)
                            OnDisconnected?.Invoke(this, EventArgs.Empty);
                        
                        Header = null;
                        VarHeaders = null;
                        
                        _logger?.LogWarning($"Not connected to iRacing ({ex.Message})");
                        
                        await SafeDelay(CheckConnectionDelay, token);
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
                
                await SafeDelay(WaitDelay, token);
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
                int offset = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarOffsetOffset));
                int count = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarCountOffset));
                byte[] name = new byte[Constants.MaxString];
                byte[] desc = new byte[Constants.MaxDesc];
                byte[] unit = new byte[Constants.MaxString];
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarNameOffset), name, 0, Constants.MaxString);
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarDescOffset), desc, 0, Constants.MaxDesc);
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarUnitOffset), unit, 0, Constants.MaxString);
                string nameStr = _encoding.GetString(name).TrimEnd(TrimChar);
                string descStr = _encoding.GetString(desc).TrimEnd(TrimChar);
                string unitStr = _encoding.GetString(unit).TrimEnd(TrimChar);
                var header = new VarHeader(type, offset, count, nameStr, descStr, unitStr);
                VarHeaders[header.Name] = header;  
            }
        }

        public object GetData(string name)
        {
            if (!IsConnected()) return null;
            if (!VarHeaders.TryGetValue(name, out var requestedHeader)) return null;

            int varOffset = requestedHeader.Offset;
            int count = requestedHeader.Count;

            switch (requestedHeader.Type)
            {
                case VarType.irChar:
                    {
                        byte[] data = new byte[count];
                        FileMapView.ReadArray(Header.Offset + varOffset, data, 0, count);
                        return _encoding.GetString(data).TrimEnd(TrimChar);
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
            (IsConnected()) switch
            {
                true => FileMapView.ReadString(Header.SessionInfoOffset, Header.SessionInfoLength),
                _ => null
            };

        public bool IsConnected()
        {
            if (Header != null)
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