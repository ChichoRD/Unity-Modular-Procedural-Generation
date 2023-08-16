using System.Collections.Generic;

public interface IBranchingGenerationData : IGenerationData
{
    IList<IGenerationData> ChildrenData { get; }
}
