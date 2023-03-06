using System;

namespace irsdkSharp;

/// <summary>
/// Options for the iRacing SDK
/// </summary>
public class IRacingSdkOptions
{
    /// <summary>
    /// The default sdk options.
    /// </summary>
    public static readonly IRacingSdkOptions Default = new();
    
    private int _updateFrequency = IRacingSdkDefaults.DefaultUpdateFrequency;
    private int _checkConnectionDelay = IRacingSdkDefaults.DefaultCheckConnectionDelay;

    /// <summary>
    /// The actual delay between updates in milliseconds
    /// </summary>
    internal int UpdateDelay => 1000 / UpdateFrequency;
    
    /// <summary>
    /// Updates per second (1-60)
    /// </summary>
    public int UpdateFrequency
    {
        get => _updateFrequency;
        set
        {
            if (value <= 0 || value > IRacingSdkDefaults.MaxUpdateFrequency)
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