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
using System.Runtime.Versioning;

namespace irsdkSharp
{
    [SupportedOSPlatform("windows")]
    public class IRacingSDK : IDisposable
    {
        #region Fields
        private readonly Encoding _encoding;
        private readonly ILogger<IRacingSDK>? _logger;
        
        private MemoryMappedFile? _iRacingFile;
        private Dictionary<string, VarHeader>? _varHeaders;
        private bool _disposed = false;
        
        private CancellationTokenSource? _loopCancellationSource;
        #endregion

        #region Properties
        /// <summary>
        /// Options for the SDK.
        /// </summary>
        public IRacingSdkOptions Options { get; }
        
        /// <summary>
        /// If the data loop has been started.
        /// </summary>
        /// <exception cref="ObjectDisposedException"/>
        public bool IsStarted => (_disposed) 
            ? throw new ObjectDisposedException(nameof(IRacingSDK)) 
            : _loopCancellationSource != null && !_loopCancellationSource.IsCancellationRequested;

        public IRacingSdkHeader? Header { get; private set; }
        public MemoryMappedViewAccessor? FileMapView { get; protected set; }
        public Dictionary<string, VarHeader>? VarHeaders => _varHeaders ??= Header?.GetVarHeaders(_encoding);
        #endregion

        #region Events
        [Obsolete("Use the DataChanged event instead.", true)]
        public event Action OnDataChanged;
        
        [Obsolete("Use the Connected event instead.", true)]
        public event Action OnConnected;
        
        [Obsolete("Use the Disconnected event instead.", true)]
        public event Action OnDisconnected;

        /// <summary>
        /// Invoked every time the SDK reads the data from iRacing. Not necessarily new data.
        /// </summary>
        public event EventHandler? DataChanged;
        
        /// <summary>
        /// Invoked when the SDK starts receiving data from iRacing.
        /// </summary>
        public event EventHandler? Connected;
        
        /// <summary>
        /// Invoked when the SDK stops receiving data from iRacing.
        /// </summary>
        public event EventHandler? Disconnected;
        #endregion

        [Obsolete("Use the getter of FileMapView instead.")]
        public static MemoryMappedViewAccessor GetFileMapView(IRacingSDK racingSDK) 
            => racingSDK.FileMapView;

        [Obsolete("Use the getter of VarHeaders instead.")]
        public static Dictionary<string, VarHeader> GetVarHeaders(IRacingSDK racingSDK)
            => racingSDK.VarHeaders;

        public IRacingSDK(IRacingSdkOptions? options, ILogger<IRacingSDK>? logger, bool autoStart = true)
        {
            // Register CP1252 encoding
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _encoding = Encoding.GetEncoding(1252);
            
            Options = options ?? IRacingSdkOptions.Default;
            
            _logger = logger;
            
            if (autoStart)
                Start();
        }
        
        public IRacingSDK(bool autoStart = true) : this(null, null, autoStart)
        {
        }
        
        public IRacingSDK(IRacingSdkOptions? options, bool autoStart = true) : this(options, null, autoStart)
        {
        }

        public IRacingSDK(ILogger<IRacingSDK> logger, bool autoStart = true) : this(null, logger, autoStart)
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
            if (_disposed)
                throw new ObjectDisposedException(nameof(IRacingSDK));
            
            if (IsStarted) 
                return;
            
            _loopCancellationSource = new();
            
            Task.Factory.StartNew(() => 
                Loop(_loopCancellationSource.Token), _loopCancellationSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
        
        public void Stop()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(IRacingSDK));
            
            _loopCancellationSource?.Cancel();
            _loopCancellationSource?.Dispose();
            _loopCancellationSource = null;
        }

        public void Dispose()
        {
            if (_disposed) return;
            
            Stop();
            
            FileMapView?.Dispose();
            FileMapView = null;
            
            _iRacingFile?.Dispose();
            _iRacingFile = null;
            
            Header = null;
            
            _varHeaders = null;
            
            _disposed = true;
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
                            Disconnected?.Invoke(this, EventArgs.Empty);
                        
                        Header = null;
                        _varHeaders = null;
                        
                        _logger?.LogWarning($"Not connected to iRacing ({ex.Message})");
                        
                        try
                        {
                            await Task.Delay(Options.CheckConnectionDelay, token);
                        }
                        catch (OperationCanceledException)
                        {
                            break;
                        }
                        continue;
                    }
                }
                
                if (Header == null)
                {
                    Header = new IRacingSdkHeader(FileMapView);
                    Connected?.Invoke(this, EventArgs.Empty);
                }
                
                DataChanged?.Invoke(this, EventArgs.Empty);
                
                try
                {
                    await Task.Delay(Options.UpdateDelay, token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

        public object? GetData(string name)
        {
            if (!IsConnected()) return null;
            if (VarHeaders == null || !VarHeaders.TryGetValue(name, out var requestedHeader)) return null;

            int varOffset = requestedHeader.Offset;
            int count = requestedHeader.Count;

            switch (requestedHeader.Type)
            {
                case VarType.irChar:
                    {
                        byte[] data = new byte[count];
                        FileMapView.ReadArray(Header.Offset + varOffset, data, 0, count);
                        return _encoding.GetString(data).TrimEnd(Constants.EndChar);
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

        public string? GetSessionInfo()
            => (Header == null) ? null : FileMapView?.ReadString(Header.SessionInfoOffset, Header.SessionInfoLength);
        
        /// <summary>
        /// If the sim is connected.
        /// </summary>
        /// <exception cref="ObjectDisposedException"/>
        // Should be a property but for compatibility we keep it as a method.
        public bool IsConnected()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(IRacingSDK));
            
            if (Header != null)
                return (Header.Status & 1) > 0;

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