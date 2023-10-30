public readonly struct BranchingGenerationData : IGenerationData<GeneratedBranchData>
{
    public BranchingGenerationData(IGenerationData generationData, GeneratedBranchData generatedBranchData) : this()
    {
        _generationData = generationData;
        Data = generatedBranchData;
    }

    private readonly IGenerationData _generationData;

    public readonly IProceduralGenerator Generator => _generationData.Generator;
    public readonly GenerationStatus Status => _generationData.Status;

    public readonly GeneratedBranchData Data { get; }
}
