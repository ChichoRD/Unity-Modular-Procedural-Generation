using UnityEngine;

public class PassGenerator : MonoBehaviour, IProceduralGenerator
{
    [RequireInterface(typeof(IGenerationModifier<IGenerationData, IGenerationData>))]
    [SerializeField] private Object _generatorModifierObject;
    private IGenerationModifier<IGenerationData, IGenerationData> GeneratorModifier => _generatorModifierObject as IGenerationModifier<IGenerationData, IGenerationData>;

    [RequireInterface(typeof(IProceduralGenerator))]
    [SerializeField] private Object _nextGeneratorObject;
    private IProceduralGenerator NextGenerator => _nextGeneratorObject as IProceduralGenerator;

    public IGenerationData Generate(int depth)
    {
        IGenerationData data = new GenerationData(this);
        data = GeneratorModifier?.Modify(data) ?? data;

        if (depth < 1
            || data.Status != GenerationStatus.Success)
            return data;

        IBranchingGenerationData branchingData = new BranchingGenerationData(data);
        branchingData.ChildrenData.Add(NextGenerator.Generate(depth - 1));
        return data;
    }
}