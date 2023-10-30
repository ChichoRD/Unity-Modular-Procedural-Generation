public readonly struct InstanceGenerationData : IGenerationData<GeneratedInstanceData>
{
    public InstanceGenerationData(IGenerationData generationData, GeneratedInstanceData generatedInstanceData)
    {
        _generationData = generationData;
        Data = generatedInstanceData;
    }

    private readonly IGenerationData _generationData;

    public readonly IProceduralGenerator Generator => _generationData.Generator;
    public readonly GenerationStatus Status => _generationData.Status;

    public readonly GeneratedInstanceData Data { get; }
}