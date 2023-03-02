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
    public class IRacingSDK : IDisposable
    {
        private readonly Encoding _encoding;
        private readonly ILogger<IRacingSDK>? _logger;
        private readonly IrSdkOptions _options;

        private MemoryMappedFile? _iRacingFile;
        private CancellationTokenSource? _loopCancellationSource;
        private Dictionary<string, VarHeader>? _varHeaders;
        private bool _isDisposed = false;

        #region Properties
        /// <summary>
        /// Options for the SDK.
        /// </summary>
        public IrSdkOptions Options => _options;

        /// <summary>
        /// If the data loop has been started.
        /// </summary>
        public bool IsStarted
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(IRacingSDK));
                
                return _loopCancellationSource != null && !_loopCancellationSource.IsCancellationRequested;
            }
        }

        /// <summary>
        /// If the sim is connected.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(IRacingSDK));
                
                if (Header != null)
                    return (Header.Status & 1) > 0;

                return false;
            }
        }

        public IRacingSdkHeader? Header { get; private set; }
        public MemoryMappedViewAccessor? FileMapView { get; private set; }
        public Dictionary<string, VarHeader>? VarHeaders => _varHeaders ??= Header?.GetVarHeaders(_encoding);
        #endregion

        #region Events
        public event EventHandler? OnDataChanged;
        public event EventHandler? OnConnected;
        public event EventHandler? OnDisconnected;
        #endregion

        public IRacingSDK(IrSdkOptions? options, ILogger<IRacingSDK>? logger, bool autoStart = false)
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
        
        public IRacingSDK(bool autoStart = false) : this(null, null, autoStart)
        {
        }
        
        public IRacingSDK(IrSdkOptions? options, bool autoStart = false) : this(options, null, autoStart)
        {
        }

        public IRacingSDK(ILogger<IRacingSDK> logger, bool autoStart = false) : this(null, logger, autoStart)
        {
        }

        public IRacingSDK(MemoryMappedViewAccessor accessor) : this(null, null, false)
        {
            FileMapView = accessor;

            Header = new IRacingSdkHeader(FileMapView);
        }

        public void Start(int updateFrequency)
        {
            Options.UpdateFrequency = updateFrequency;
            Start();
        }
        
        public void Start()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(IRacingSDK));
            
            if (IsStarted) 
                return;
            
            _loopCancellationSource = new();
            
            Task.Factory.StartNew(() => 
                Loop(_loopCancellationSource.Token), _loopCancellationSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
        
        public void Stop()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(IRacingSDK));
            
            _loopCancellationSource?.Cancel();
            _loopCancellationSource?.Dispose();
            _loopCancellationSource = null;
        }
        
        public void Dispose()
        {
            if (_isDisposed)
                return;
            
            Stop();
            
            FileMapView?.Dispose();
            FileMapView = null;
            
            _iRacingFile?.Dispose();
            _iRacingFile = null;
            
            Header = null;
            
            _varHeaders = null;
            
            _isDisposed = true;
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
                        _varHeaders = null;
                        
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

        public object? GetData(string name)
        {
            if (!IsConnected) return null;
            if (!VarHeaders?.TryGetValue(name, out var requestedHeader) ?? true) return null;

            int varOffset = requestedHeader.Offset;
            int count = requestedHeader.Count;

            switch (requestedHeader.Type)
            {
                case VarType.irChar:
                    {
                        byte[] data = new byte[count];
                        FileMapView.ReadArray(Header.Offset + varOffset, data, 0, count);
                        return _encoding.GetString(data).TrimEnd(IrSdkConstants.EndChar);
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