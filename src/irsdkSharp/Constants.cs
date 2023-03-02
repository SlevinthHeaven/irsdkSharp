namespace irsdkSharp
{
    internal static class Constants
    {
        public const uint DesiredAccess = 2031619;
        public const string DataValidEventName = "Local\\IRSDKDataValidEvent";
        public const string MemMapFileName = "Local\\IRSDKMemMapFileName";
        public const string BroadcastMessageName = "IRSDK_BROADCASTMSG";
        public const string PadCarNumName = "IRSDK_PADCARNUM";
        public const int MaxString = 32;
        public const int MaxDesc = 64;
        public const int MaxVars = 4096;
        public const int MaxBufs = 4;
        public const int StatusConnected = 1;
        public const int SessionStringLength = 0x20000; // 128k
        
        public const int VarOffsetOffset = 4;
        public const int VarCountOffset = 8;
        public const int VarNameOffset = 16;
        public const int VarDescOffset = 48;
        public const int VarUnitOffset = 112;
    
        public const char EndChar = '\0';
    }
}
