public interface IGenerationData
{
    IProceduralGenerator Generator { get; }
    GenerationStatus Status { get; }
}
