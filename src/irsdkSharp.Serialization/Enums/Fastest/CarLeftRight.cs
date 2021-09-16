using System;

namespace irsdkSharp.Serialization.Enums.Fastest
{
    public enum CarLeftRight
    {
        /// <summary>
        /// Left / right spotter is off.
        /// </summary>
        LROff,

        /// <summary>
        ///  No cars around us.
        /// </summary>
        LRClear,

        /// <summary>
        ///  There is a car to our left.
        /// </summary>
        LRCarLeft,

        /// <summary>
        ///  There is a car to our right.
        /// </summary>
        LRCarRight,

        /// <summary>
        ///  There are cars on each side.
        /// </summary>
        LRCarLeftRight,

        /// <summary>
        ///  There are two cars to our left.
        /// </summary>
        LR2CarsLeft,

        /// <summary>
        ///  There are two cars to our right.
        /// </summary>
        LR2CarsRight,
    }
}