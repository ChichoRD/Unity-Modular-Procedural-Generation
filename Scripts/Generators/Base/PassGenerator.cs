using UnityEngine;

public class PassGenerator : MonoBehaviour, IProceduralGenerator
{
    [RequireInterface(typeof(IGenerationModifier<IGenerationData, IGenerationData<GeneratedBranchData>>))]
    [SerializeField] private Object _generatorModifierObject;
    private IGenerationModifier<IGenerationData, IGenerationData<GeneratedBranchData>> GeneratorModifier => _generatorModifierObject as IGenerationModifier<IGenerationData, IGenerationData<GeneratedBranchData>>;

    [RequireInterface(typeof(IProceduralGenerator))]
    [SerializeField] private Object _nextGeneratorObject;
    private IProceduralGenerator NextGenerator => _nextGeneratorObject as IProceduralGenerator;

    public IGenerationData Generate(int depth)
    {
        IGenerationData data = new GenerationData(this, GenerationStatus.Failed);
        IGenerationData<GeneratedBranchData> branchData = GeneratorModifier?.Modify(data)
                                                          ?? new BranchingGenerationData(data, default);

        if (depth < 1
            || branchData is not { Status: GenerationStatus.Success })
            return branchData;

        branchData.Data.ChildrenData.Add(NextGenerator.Generate(depth - 1));
        return data;
    }
}