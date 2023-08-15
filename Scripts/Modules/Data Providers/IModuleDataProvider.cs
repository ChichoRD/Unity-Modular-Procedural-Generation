using System.Collections.Generic;
using UnityEngine;

public interface IModuleDataProvider
{
    Transform RootTransform { get; }
    IEnumerable<ConnectionAnchor> ConnectionAnchors { get; }
    bool TryGetGenerator(out IProceduralGenerator generator);
    Bounds GetBounds();
}
