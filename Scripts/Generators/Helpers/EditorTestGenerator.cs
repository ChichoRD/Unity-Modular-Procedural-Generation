using UnityEngine;

public class EditorTestGenerator : MonoBehaviour, IProceduralGenerator
{
    [RequireInterface(typeof(IProceduralGenerator))]
    [SerializeField] private Object _generatorObject;
    private IProceduralGenerator Generator => _generatorObject as IProceduralGenerator;

    [SerializeField][Min(0)] private int _depth = 5;

    public IGenerationData Generate(int depth)
    {
        return Generator.Generate(depth);
    }

    [ContextMenu(nameof(TestGeneration))]
    private void TestGeneration()
    {
        Debug.Log(Generate(_depth - 1));
    }
}