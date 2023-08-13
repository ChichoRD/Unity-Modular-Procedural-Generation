using UnityEngine;

public class SearchGenerator : MonoBehaviour, IProceduralGenerator
{
    [SerializeField] private FixedGenerator _generator;

    public GenerationData Generate(int depth)
    {
        var data = _generator.Generate(depth);
        var nextGenerator = data.foundGenerator;
        if (nextGenerator == null || depth < 1) return data;

        data.childrenData.Add(nextGenerator.Generate(depth - 1));
        return data;
    }

    [ContextMenu(nameof(TestGenerate))]
    private void TestGenerate()
    {
        const int DEPTH = 2;
        var data = Generate(DEPTH - 1);
        Debug.Log(data);
    }

}