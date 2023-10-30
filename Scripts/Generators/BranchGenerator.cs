using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BranchGenerator : MonoBehaviour, IProceduralGenerator
{
    [SerializeField] private Object[] _nextGeneratorObjects;
    private IEnumerable<IProceduralGenerator> NextGenerators => _nextGeneratorObjects.Cast<IProceduralGenerator>();

    public IGenerationData Generate(int depth)
    {
        IGenerationData data = new GenerationData(this, GenerationStatus.Failed);

        if (depth < 1)
            return new BranchingGenerationData(data, default);

        IGenerationData<GeneratedBranchData> branchData = new BranchingGenerationData(
            new GenerationData(this, GenerationStatus.Success),
            new GeneratedBranchData(new List<IGenerationData>()));

        foreach (var nextGenerator in NextGenerators)
        {
            if (nextGenerator != null)
                branchData.Data.ChildrenData.Add(nextGenerator.Generate(depth - 1));
        }

        return data;
    }
}