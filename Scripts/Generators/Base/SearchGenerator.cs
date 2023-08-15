using UnityEngine;

public class SearchGenerator : MonoBehaviour, IProceduralGenerator
{
    [SerializeField] private FixedGenerator _generator;

    public IGenerationData Generate(int depth)
    {
        IGenerationData data = _generator.Generate(depth);

        if (data is not InstanceGenerationData instanceData
            || !instanceData.HasInstanceGenerator()
            || depth < 1) return data;

        instanceData.ChildrenData.Add(instanceData.InstanceGenerator.Generate(depth - 1));
        return data;
    }

    [ContextMenu(nameof(TestGenerate))]
    private void TestGenerate()
    {
        const int DEPTH = 8;
        var data = Generate(DEPTH - 1);
        Debug.Log(data);
    }

}