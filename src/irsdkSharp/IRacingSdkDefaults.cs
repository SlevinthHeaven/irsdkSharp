namespace irsdkSharp;

/// <summary>
/// The default values for the iRacing SDK
/// </summary>
public static class IRacingSdkDefaults
{
    /// <summary>
    /// The default updates per second.
    /// </summary>
    public const int DefaultUpdateFrequency = 1;
    
    /// <summary>
    /// The maximum updates per second supported by iRacing.
    /// </summary>
    public const int MaxUpdateFrequency = 60;
    
    /// <summary>
    /// The default delay between a connection check when the sim is not connected.
    /// </summary>
    public const int DefaultCheckConnectionDelay = 5000;
}