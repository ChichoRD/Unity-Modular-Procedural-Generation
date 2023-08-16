using UnityEngine;

public interface IModuleBoundsProvider
{
    Bounds GetBounds();
    bool UseCache { get; }
    float BoundsExpandBias { get; }
}
