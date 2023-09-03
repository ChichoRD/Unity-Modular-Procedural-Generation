using System;

[Flags]
public enum AnchorLayer : ushort
{
    InAnchorLayer0 = 1 << 0,
    InAnchorLayer1 = 1 << 1,
    InAnchorLayer2 = 1 << 2,
    InAnchorLayer3 = 1 << 3,
    InAnchorLayer4 = 1 << 4,
    InAnchorLayer5 = 1 << 5,
    InAnchorLayer6 = 1 << 6,
    InAnchorLayer7 = 1 << 7,

    OutAnchorLayer0 = 1 << 8,
    OutAnchorLayer1 = 1 << 9,
    OutAnchorLayer2 = 1 << 10,
    OutAnchorLayer3 = 1 << 11,
    OutAnchorLayer4 = 1 << 12,
    OutAnchorLayer5 = 1 << 13,
    OutAnchorLayer6 = 1 << 14,
    OutAnchorLayer7 = 1 << 15,
}