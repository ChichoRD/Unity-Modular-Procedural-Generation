using System.Collections.Generic;

public readonly struct BranchingGenerationData : IBranchingGenerationData
{
    public BranchingGenerationData(IGenerationData generationData) : this()
    {
        _generationData = generationData;
        _childrenData = new List<IGenerationData>();
    }

    private readonly IGenerationData _generationData;

    public IProceduralGenerator Generator => _generationData.Generator;
    public GenerationStatus Status => _generationData.Status;

    private readonly List<IGenerationData> _childrenData;
    public readonly IList<IGenerationData> ChildrenData => _childrenData;
}