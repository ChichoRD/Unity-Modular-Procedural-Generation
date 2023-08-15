using System.Collections.Generic;
using UnityEngine;

public struct InstanceGenerationData : IGenerationData
{
    public InstanceGenerationData(GenerationData generationData, GameObject instanceRoot, IProceduralGenerator instanceGenerator)
    {
        _generationData = generationData;
        InstanceRoot = instanceRoot;
        InstanceGenerator = instanceGenerator;
    }

    private readonly GenerationData _generationData;

    public IProceduralGenerator Generator => ((IGenerationData)_generationData).Generator;
    public GenerationStatus Status => ((IGenerationData)_generationData).Status;

    public GameObject InstanceRoot { get; set; }
    public IProceduralGenerator InstanceGenerator { get; set; }
}
