using System.Collections.Generic;

public readonly struct GeneratedBranchData
{
    public readonly IList<IGenerationData> ChildrenData { get; }
    public GeneratedBranchData(IList<IGenerationData> childrenData)
    {
        ChildrenData = childrenData;
    }
}