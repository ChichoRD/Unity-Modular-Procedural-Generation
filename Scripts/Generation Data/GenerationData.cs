public readonly struct GenerationData : IGenerationData
{
    public GenerationData(IProceduralGenerator generator, GenerationStatus status) : this()
    {
        Generator = generator;
        Status = status;
    }

    public readonly IProceduralGenerator Generator { get; }
    public GenerationStatus Status { get; }
}
