using System;

namespace irsdkSharp.Enums
{
    [Flags]
    public enum CameraState
    {
        None = 0x0000,
        IsSessionScreen = 0x0001,
        IsScenicActive = 0x0002,
        CamToolActive = 0x0004,
        UIHidden = 0x0008,
        UseAutoShotSelection = 0x0010,
        UseTemporaryEdits = 0x0020,
        UseKeyAcceleration = 0x0040,
        UseKey10xAcceleration = 0x0080,
        UseMouseAimMode = 0x0100
    }
}
