public readonly struct GenerationData : IGenerationData
{
    public GenerationData(IProceduralGenerator generator, GenerationStatus status = GenerationStatus.Success) : this()
    {
        Generator = generator;
        Status = status;
    }

    public readonly IProceduralGenerator Generator { get; }
    public GenerationStatus Status { get; }
}
