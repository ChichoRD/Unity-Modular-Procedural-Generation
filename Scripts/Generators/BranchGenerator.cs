using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BranchGenerator : MonoBehaviour, IProceduralGenerator
{
    [SerializeField] private Object[] _nextGeneratorObjects;
    private IEnumerable<IProceduralGenerator> NextGenerators => _nextGeneratorObjects.Cast<IProceduralGenerator>();

    public GenerationData Generate(int depth)
    {
        var data = new GenerationData(this);

        foreach (var nextGenerator in NextGenerators)
        {
            if (nextGenerator != null)
                data.childrenData.Add(nextGenerator.Generate(depth - 1));
        }
        return data;
    }
}