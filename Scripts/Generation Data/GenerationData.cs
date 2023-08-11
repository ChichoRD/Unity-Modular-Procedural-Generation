using System.Collections.Generic;

public struct GenerationData
{
    public GenerationData(IProceduralGenerator generator) : this()
    {
        childrenData = new List<GenerationData>();

        Generator = generator;
    }

    public List<GenerationData> childrenData;
    public IProceduralGenerator foundGenerator;
    public readonly IProceduralGenerator Generator { get; }
}