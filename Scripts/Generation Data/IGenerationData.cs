public interface IGenerationData
{
    IProceduralGenerator Generator { get; }
    GenerationStatus Status { get; }
}

public interface IGenerationData<out TData> : IGenerationData
{
    TData Data { get; }
}
