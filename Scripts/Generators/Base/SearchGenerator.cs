using UnityEngine;

public class SearchGenerator : MonoBehaviour, IProceduralGenerator
{
    [SerializeField] private FixedGenerator _generator;

    public IGenerationData Generate(int depth)
    {
        IGenerationData data = _generator.Generate(depth);

        if (depth < 1
            || data is not { Status: GenerationStatus.Success }
            || data is not IInstanceGenerationData instanceGenerationData
            || data is not IBranchingGenerationData branchingGenerationData
            || !instanceGenerationData.HasInstanceGenerator()) 
            return data;

        branchingGenerationData.ChildrenData.Add(instanceGenerationData.InstanceGenerator.Generate(depth - 1));
        return branchingGenerationData;
    }

    [ContextMenu(nameof(TestGenerate))]
    private void TestGenerate()
    {
        const int DEPTH = 8;
        var data = Generate(DEPTH - 1);
        Debug.Log(data);
    }

}