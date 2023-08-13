using System;

[Flags]
public enum AnchorLayer : byte
{
    AnchorLayer0 = 1 << 0,
    AnchorLayer1 = 1 << 1,
    AnchorLayer2 = 1 << 2,
    AnchorLayer3 = 1 << 3,
    AnchorLayer4 = 1 << 4,
    AnchorLayer5 = 1 << 5,
    AnchorLayer6 = 1 << 6,
    AnchorLayer7 = 1 << 7,
}