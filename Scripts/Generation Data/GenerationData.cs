
public readonly struct GenerationData : IGenerationData
{
    public IProceduralGenerator Generator { get; }
    public GenerationStatus Status { get; }

    public GenerationData(IProceduralGenerator generator, GenerationStatus status)
    {
        Generator = generator;
        Status = status;
    }
}