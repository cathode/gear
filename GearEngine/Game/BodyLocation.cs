using System;

namespace Gear.Game
{
    /// <summary>
    /// Enumerates locations of the body.
    /// </summary>
    [Flags]
    public enum BodyLocation : ulong
    {
        None            = 0x0,

        // Head Area

        Head            = 0x1,
        Face            = 0x2,
        LeftEye         = 0x4,
        RightEye        = 0x8,
        LeftEar         = 0x10,
        RightEar        = 0x20,
        Neck            = 0x40,

        // Upper Body Area

        LeftShoulder    = 0x80,
        RightShoulder   = 0x100,
        LeftArm         = 0x200,
        RightArm        = 0x400,
        LeftElbow       = 0x800,
        RightElbow      = 0x1000,
        LeftForearm     = 0x2000,
        RightForearm    = 0x4000,
        LeftWrist       = 0x8000,
        RightWrist      = 0x10000,
        LeftHand        = 0x20000,
        RightHand       = 0x40000,
        LeftFinger      = 0x80000,
        RightFinger     = 0x100000,
        Chest           = 0x200000,
        Back            = 0x400000,

        // Midsection Area

        Waist           = 0x800000,

        // Lower Body Area

        LeftLeg         = 0x1000000,
        RightLeg        = 0x2000000,
        LeftKnee        = 0x4000000,
        RightKnee       = 0x8000000,
        LeftShin        = 0x10000000,
        RightShin       = 0x20000000,
        LeftFoot        = 0x40000000,
        RightFoot       = 0x80000000,

        // Equipment locations

        LeftGrip        = 0x100000000,
        RightGrip       = 0x200000000,
        LeftScabbard    = 0x400000000,
        RightScabbard   = 0x800000000,
        Cape            = 0x1000000000,
        LeftQuiver      = 0x2000000000,
        RightQuiver     = 0x4000000000,

        // Misc. special locations (eg. non-humanoids)

        Special1        = 0x100000000000000,
        Special2        = 0x200000000000000,
        Special3        = 0x400000000000000,
        Special4        = 0x800000000000000,
        Special5        = 0x1000000000000000,
        Special6        = 0x2000000000000000,
        Special7        = 0x4000000000000000,
        Special8        = 0x8000000000000000,

        // Combinations

        Eyes            = LeftEye | RightEye,
        Ears            = LeftEar | RightEar,
        Shoulders       = LeftShoulder | RightShoulder,
        Arms            = LeftArm | RightArm,
        Elbows          = LeftElbow | RightElbow,
        Forearms        = LeftForearm | RightForearm,
        Wrists          = LeftWrist | RightWrist,
        Hands           = LeftHand | RightHand,
        Fingers         = LeftFinger | RightFinger,
        Legs            = LeftLeg | RightLeg,
        Knees           = LeftKnee | RightKnee,
        Shins           = LeftShin | RightShin,
        Feet            = LeftFoot | RightFoot,
        HeadArea        = Head | Eyes | Ears | Face | Neck,
        UpperBodyArea   = Shoulders | Arms | Elbows | Forearms | Wrists | Hands | Fingers | Chest | Back,
        LowerBodyArea   = Waist | Legs | Knees | Shins | Feet,
    }
}
