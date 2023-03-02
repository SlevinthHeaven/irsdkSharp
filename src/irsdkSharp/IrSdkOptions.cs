using System;

namespace irsdkSharp;

/// <summary>
/// Options for the iRacing SDK
/// </summary>
public class IrSdkOptions
{
    /// <summary>
    /// The default sdk options.
    /// </summary>
    public static readonly IrSdkOptions Default = new();
    
    private int _updateFrequency = IrSdkDefaults.DefaultUpdateFrequency;
    private int _checkConnectionDelay = IrSdkDefaults.DefaultCheckConnectionDelay;

    internal int UpdateDelay => 1000 / UpdateFrequency;
    
    /// <summary>
    /// Updates per second (1-60)
    /// </summary>
    public int UpdateFrequency
    {
        get => _updateFrequency;
        set
        {
            if (value <= 0 || value > IrSdkDefaults.MaxUpdateFrequency)
                throw new ArgumentOutOfRangeException(nameof(value), 
                    "The UpdateFrequency must be between 1 and 60");
                
            _updateFrequency = value;
        }
    }

    /// <summary>
    /// The delay between a connection check when the sim is not connected
    /// </summary>
    public int CheckConnectionDelay
    {
        get => _checkConnectionDelay;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), 
                    "The CheckConnectionDelay must be greater than 0");
            
            _checkConnectionDelay = value;
        }
    }
}