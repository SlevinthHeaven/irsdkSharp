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
    public class IRacingSDK
    {
        #region Fields
        private readonly Encoding _encoding;
        private readonly ILogger<IRacingSDK>? _logger;
        
        private MemoryMappedFile? _iRacingFile;
        private Dictionary<string, VarHeader>? _varHeaders;
        
        private bool _isStarted = false;

        // TODO: Change to only use one CTS
        private AutoResetEvent _gameLoopEvent;
        private IntPtr _hEvent;
        private readonly CancellationTokenSource _waitValidDataLoopCancellation;
        private readonly CancellationToken _waitValidDataLoopCancellationToken;
        private readonly CancellationTokenSource _connectionLoopCancellation;
        private readonly CancellationToken _connectionLoopCancellationToken;
        #endregion

        #region Properties
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

        public IRacingSDK(ILogger<IRacingSDK>? logger)
        {
            // Register CP1252 encoding
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _encoding = Encoding.GetEncoding(1252);
            
            _logger = logger;
        }
        
        public IRacingSDK() : this(logger: null)
        {
            _waitValidDataLoopCancellation = new CancellationTokenSource();
            _waitValidDataLoopCancellationToken = _waitValidDataLoopCancellation.Token;
            Task.Run(WaitValidDataLoop, _waitValidDataLoopCancellationToken);

            _connectionLoopCancellation = new CancellationTokenSource();
            _connectionLoopCancellationToken = _connectionLoopCancellation.Token;
            Task.Run(ConnectionLoop, _waitValidDataLoopCancellationToken);
        }

        public IRacingSDK(MemoryMappedViewAccessor accessor) : this(logger: null)
        {
            FileMapView = accessor;

            Header = new IRacingSdkHeader(FileMapView);
            _isStarted = true;
        }

        private void ConnectionLoop()
        {
            while (true)
            {
                if (_connectionLoopCancellationToken.IsCancellationRequested) break;
                try
                {
                    if (!IsConnected() && !_isStarted && Header == null)
                    {
                        if (_iRacingFile == null)
                        {
                            _iRacingFile = MemoryMappedFile.OpenExisting(Constants.MemMapFileName);
                            FileMapView = _iRacingFile.CreateViewAccessor();

                            _hEvent = OpenEvent(Constants.DesiredAccess, false, Constants.DataValidEventName);
                            _gameLoopEvent = new AutoResetEvent(false)
                            {
                                SafeWaitHandle = new SafeWaitHandle(_hEvent, true)
                            };

                            _isStarted = true;
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                    _isStarted = false;
                    Header = null;
                    _varHeaders = null;
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
                if (_isStarted)
                {
                    try
                    {
                        var valid = _gameLoopEvent.WaitOne(1000);

                        if (valid)
                        {
                            if (Header == null)
                            {
                                Header = new IRacingSdkHeader(FileMapView);
                                Connected?.Invoke(this, EventArgs.Empty);
                            }
                            DataChanged?.Invoke(this, EventArgs.Empty);
                        }
                        else
                        {
                            if (Header != null)
                            {
                                Header = null;
                                Disconnected?.Invoke(this, EventArgs.Empty);
                            }
                            if (VarHeaders != null)
                            {
                                _varHeaders = null;
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

        public object GetData(string name)
        {
            if (!_isStarted || Header == null) return null;
            if (!VarHeaders.TryGetValue(name, out var requestedHeader)) return null;

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

        public string GetSessionInfo() =>
            (_isStarted && Header != null) switch
            {
                true => FileMapView.ReadString(Header.SessionInfoOffset, Header.SessionInfoLength),
                _ => null
            };

        public bool IsConnected()
        {
            if (_isStarted && Header != null)
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