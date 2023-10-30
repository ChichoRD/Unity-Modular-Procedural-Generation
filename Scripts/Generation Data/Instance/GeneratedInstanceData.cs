using UnityEngine;

public readonly struct GeneratedInstanceData
{
    public GameObject InstanceRoot { get; }
    public IProceduralGenerator InstanceGenerator { get; }

    public GeneratedInstanceData(GameObject instanceRoot, IProceduralGenerator instanceGenerator)
    {
        InstanceRoot = instanceRoot;
        InstanceGenerator = instanceGenerator;
    }
}