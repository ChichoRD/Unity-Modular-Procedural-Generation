using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BranchGenerator : MonoBehaviour, IProceduralGenerator
{
    [SerializeField] private Object[] _nextGeneratorObjects;
    private IEnumerable<IProceduralGenerator> NextGenerators => _nextGeneratorObjects.Cast<IProceduralGenerator>();

    public IGenerationData Generate(int depth)
    {
        if (depth < 1) return new BranchingGenerationData(new GenerationData(this, GenerationStatus.Failed));
        BranchingGenerationData data = new BranchingGenerationData(new GenerationData(this, GenerationStatus.Success));

        foreach (var nextGenerator in NextGenerators)
        {
            if (nextGenerator != null)
                data.ChildrenData.Add(nextGenerator.Generate(depth - 1));
        }

        return data;
    }
}