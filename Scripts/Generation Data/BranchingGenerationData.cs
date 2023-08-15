using System.Collections.Generic;

public readonly struct BranchingGenerationData : IGenerationData
{
    public BranchingGenerationData(GenerationData generationData) : this()
    {
        _generationData = generationData;
        _childrenData = new List<IGenerationData>();
    }

    private readonly GenerationData _generationData;

    public IProceduralGenerator Generator => ((IGenerationData)_generationData).Generator;
    public GenerationStatus Status => ((IGenerationData)_generationData).Status;

    private readonly List<IGenerationData> _childrenData;
    public readonly IList<IGenerationData> ChildrenData => _childrenData;
}