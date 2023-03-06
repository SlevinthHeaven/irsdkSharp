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
using System.Runtime.Versioning;

namespace irsdkSharp
{
    /// <summary>
    /// Connects to the iRacing SDK.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class IRacingSDK : IDisposable
    {
        #region Fields
        private readonly Encoding _encoding;
        private readonly ILogger<IRacingSDK>? _logger;
        
        private Dictionary<string, VarHeader>? _varHeaders;
        private bool _disposed;
        
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
#pragma warning disable CS0067
        /// <summary>
        /// Not used anymore. Use the DataChanged event instead.
        /// </summary>
        [Obsolete("Use the DataChanged event instead.", true)]
        public event Action OnDataChanged = null!;
        
        /// <summary>
        /// Not used anymore. Use the DataChanged event instead.
        /// </summary>
        [Obsolete("Use the Connected event instead.", true)]
        public event Action OnConnected = null!;
        
        /// <summary>
        /// Not used anymore. Use the DataChanged event instead.
        /// </summary>
        [Obsolete("Use the Disconnected event instead.", true)]
        public event Action OnDisconnected = null!;
#pragma warning restore CS0067

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

        /// <summary>
        /// Not used anymore. Use the getter of FileMapView instead.
        /// </summary>
        /// <param name="racingSdk">The Sdk.</param>
        /// <returns>The FileMapView property of the <paramref name="racingSdk"/>.</returns>
        [Obsolete("Use the getter of FileMapView instead.")]
        public static MemoryMappedViewAccessor? GetFileMapView(IRacingSDK racingSdk) 
            => racingSdk.FileMapView;

        /// <summary>
        /// Not used anymore. Use the getter of VarHeaders instead.
        /// </summary>
        /// <param name="racingSdk">The Sdk.</param>
        /// <returns>The VarHeaders property of the <paramref name="racingSdk"/>.</returns>
        [Obsolete("Use the getter of VarHeaders instead.")]
        public static Dictionary<string, VarHeader>? GetVarHeaders(IRacingSDK racingSdk)
            => racingSdk.VarHeaders;

        /// <summary>
        /// Creates a new instance of the SDK.
        /// </summary>
        /// <param name="options">The options used when the Sdk is started.</param>
        /// <param name="autoStart">Whether or not the Sdk should be auto started on initialization.</param>
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
        
        /// <summary>
        /// Creates a new instance of the SDK.
        /// </summary>
        /// <param name="autoStart">Whether or not the Sdk should be auto started on initialization.</param>
        public IRacingSDK(bool autoStart = true) : this(null, null, autoStart)
        {
        }
        
        /// <summary>
        /// Creates a new instance of the SDK.
        /// </summary>
        /// <param name="options">The options used when the Sdk is started.</param>
        /// <param name="autoStart">Whether or not the Sdk should be auto started on initialization.</param>
        public IRacingSDK(IRacingSdkOptions? options, bool autoStart = true) : this(options, null, autoStart)
        {
        }

        /// <summary>
        /// Creates a new instance of the SDK.
        /// </summary>
        /// <param name="autoStart">Whether or not the Sdk should be auto started on initialization.</param>
        public IRacingSDK(ILogger<IRacingSDK> logger, bool autoStart = true) : this(null, logger, autoStart)
        {
        }

        public IRacingSDK(MemoryMappedViewAccessor accessor) : this(null, null, false)
        {
            FileMapView = accessor;

            Header = new IRacingSdkHeader(FileMapView);
        }
        
        /// <summary>
        /// Start listening for data from iRacing.
        /// </summary>
        /// <param name="updateFrequency">Set the update per second (1-60).</param>
        /// <exception cref="ObjectDisposedException"/>
        public void Start(int updateFrequency)
        {
            Options.UpdateFrequency = updateFrequency;
            Start();
        }
        
        /// <summary>
        /// Start listening for data from iRacing.
        /// </summary>
        /// <exception cref="ObjectDisposedException"/>
        public void Start()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(IRacingSDK));
            
            if (IsStarted) 
                return;
            
            _loopCancellationSource = new();
            
            Task.Factory.StartNew(() => 
                Loop(_loopCancellationSource.Token), _loopCancellationSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            
            _logger?.LogInformation("Started the Sdk.");
        }
        
        /// <summary>
        /// Stop listening for data from iRacing.
        /// </summary>
        /// <exception cref="ObjectDisposedException"/>
        public void Stop()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(IRacingSDK));
            
            _loopCancellationSource?.Cancel();
            _loopCancellationSource?.Dispose();
            _loopCancellationSource = null;
            
            _logger?.LogInformation("Stopped the Sdk.");
        }

        /// <summary>
        /// Dispose the Sdk.
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;

            Stop();
            
            FileMapView?.Dispose();
            FileMapView = null;
            
            Header = null;
            
            _varHeaders = null;
            
            _disposed = true;
            
            GC.SuppressFinalize(this);
        }
        
        private async void Loop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (!IsConnected() && FileMapView == null)
                {
                    try
                    {
                        using var iRacingFile = MemoryMappedFile.OpenExisting(Constants.MemMapFileName, MemoryMappedFileRights.Read);
                        FileMapView = iRacingFile.CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read);
                    }
                    catch (FileNotFoundException ex)
                    {
                        if (Header != null)
                        {
                            Disconnected?.Invoke(this, EventArgs.Empty);
                            _logger?.LogWarning($"Disconnected from iRacing ({ex.Message})");
                        }
                        else
                        {
                            _logger?.LogWarning($"Not connected to iRacing ({ex.Message})");
                        }
                        
                        Header = null;
                        _varHeaders = null;

                        try
                        {
                            _logger?.LogInformation($"Waiting {Options.CheckConnectionDelay} ms before retrying to connect to iRacing.");
                            await Task.Delay(Options.CheckConnectionDelay, token);
                        }
                        catch (OperationCanceledException)
                        {
                            _logger?.LogTrace("The CancellationToken was cancelled while waiting for next connect retry to iRacing.");
                            break;
                        }
                        continue;
                    }
                }
                
                bool headerWasNull = Header == null;
                
                if (headerWasNull)
                    Header = new IRacingSdkHeader(FileMapView);

                if (IsConnected())
                {
                    if (headerWasNull)
                    {
                        Connected?.Invoke(this, EventArgs.Empty);
                        _logger?.LogInformation("Connected to iRacing.");
                    }
                    
                    DataChanged?.Invoke(this, EventArgs.Empty);
                    _logger?.LogDebug("Data changed.");
                }
                
                try
                {
                    _logger?.LogTrace($"Waiting {Options.UpdateDelay} ms before next update.");
                    await Task.Delay(Options.UpdateDelay, token);
                }
                catch (OperationCanceledException)
                {
                    _logger?.LogTrace("The CancellationToken was cancelled while waiting for next update.");
                    break;
                }
            }
            
            _logger?.LogDebug("Exited the Sdk loop.");
        }

        public object? GetData(string name)
        {
            if (!IsConnected()) return null;
            if (VarHeaders == null || !VarHeaders.TryGetValue(name, out var requestedHeader)) return null;

            int varOffset = requestedHeader.Offset;
            int count = requestedHeader.Count;
            int position = Header.Offset + varOffset;

            switch (requestedHeader.Type)
            {
                case VarType.irChar:
                    {
                        byte[] data = new byte[count];
                        FileMapView.ReadArray(position, data, 0, count);
                        return _encoding.GetString(data).TrimEnd(Constants.EndChar);
                    }
                case VarType.irBool:
                    {
                        if (count > 1)
                        {
                            return FileMapView.ReadArray<bool>(position, count);
                        }
                        else
                        {
                            return FileMapView.ReadBoolean(position);
                        }
                    }
                case VarType.irInt:
                case VarType.irBitField:
                    {
                        if (count > 1)
                        {
                            return FileMapView.ReadArray<int>(position, count);
                        }
                        else
                        {
                            return FileMapView.ReadInt32(position);
                        }
                    }
                case VarType.irFloat:
                    {
                        if (count > 1)
                        {
                            return FileMapView.ReadArray<float>(position, count);
                        }
                        else
                        {
                            return FileMapView.ReadSingle(position);
                        }
                    }
                case VarType.irDouble:
                    {
                        if (count > 1)
                        {
                            return FileMapView.ReadArray<double>(position, count);
                        }
                        else
                        {
                            return FileMapView.ReadDouble(position);
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