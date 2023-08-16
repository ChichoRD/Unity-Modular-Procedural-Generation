using System.Collections.Generic;
using UnityEngine;

public readonly struct BranchingInstanceGenerationData : IInstanceGenerationData, IBranchingGenerationData
{
    public BranchingInstanceGenerationData(IInstanceGenerationData instanceGenerationData)
    {
        _branchingGenerationData = new BranchingGenerationData(instanceGenerationData);
        _instanceGenerationData = instanceGenerationData;
    }

    private readonly IBranchingGenerationData _branchingGenerationData;
    private readonly IInstanceGenerationData _instanceGenerationData;

    public IList<IGenerationData> ChildrenData => _branchingGenerationData.ChildrenData;
    public IProceduralGenerator Generator => _branchingGenerationData.Generator;
    public GenerationStatus Status => _branchingGenerationData.Status;

    public GameObject InstanceRoot => _instanceGenerationData.InstanceRoot;
    public IProceduralGenerator InstanceGenerator => _instanceGenerationData.InstanceGenerator;
}