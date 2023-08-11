using System.Collections.Generic;
using UnityEngine;

public class PassGenerator : MonoBehaviour, IProceduralGenerator
{
    [SerializeField] private FixedGenerator _generator;

    [RequireInterface(typeof(IProceduralGenerator))]
    [SerializeField] private Object _nextGeneratorObject;
    private IProceduralGenerator NextGenerator => _nextGeneratorObject as IProceduralGenerator;

    public GenerationData Generate(int depth)
    {
        var data = _generator.Generate(depth);
        if (NextGenerator == null || depth < 1) return data;

        data.childrenData.Add(NextGenerator.Generate(depth - 1));
        return data;
    }

    [ContextMenu(nameof(TestGenerate))]
    private void TestGenerate()
    {
        const int DEPTH = 4;
        var data = Generate(DEPTH - 1);
        Debug.Log(data);
    }
}