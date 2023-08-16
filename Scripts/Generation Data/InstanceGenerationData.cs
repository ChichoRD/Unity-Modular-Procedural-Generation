using UnityEngine;

public struct InstanceGenerationData : IInstanceGenerationData
{
    public InstanceGenerationData(IGenerationData generationData, GameObject instanceRoot, IProceduralGenerator instanceGenerator)
    {
        _generationData = generationData;
        InstanceRoot = instanceRoot;
        InstanceGenerator = instanceGenerator;
    }

    private readonly IGenerationData _generationData;

    public IProceduralGenerator Generator => _generationData.Generator;
    public GenerationStatus Status => _generationData.Status;

    public GameObject InstanceRoot { get; set; }
    public IProceduralGenerator InstanceGenerator { get; set; }
}
