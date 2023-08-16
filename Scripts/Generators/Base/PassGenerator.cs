using UnityEngine;

public class PassGenerator : MonoBehaviour, IProceduralGenerator
{
    [SerializeField] private FixedGenerator _generator;

    [RequireInterface(typeof(IProceduralGenerator))]
    [SerializeField] private Object _nextGeneratorObject;
    private IProceduralGenerator NextGenerator => _nextGeneratorObject as IProceduralGenerator;

    public IGenerationData Generate(int depth)
    {
        IGenerationData data = _generator.Generate(depth);

        if (depth < 1
            || data.Status != GenerationStatus.Success
            || data is not IBranchingGenerationData branchingGenerationData)
            return data;

        branchingGenerationData.ChildrenData.Add(NextGenerator.Generate(depth - 1));
        return branchingGenerationData;
    }

    [ContextMenu(nameof(TestGenerate))]
    private void TestGenerate()
    {
        const int DEPTH = 4;
        var data = Generate(DEPTH - 1);
        Debug.Log(data);
    }
}