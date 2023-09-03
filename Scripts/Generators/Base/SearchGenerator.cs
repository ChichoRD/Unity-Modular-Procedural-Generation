using UnityEngine;

public class SearchGenerator : MonoBehaviour, IProceduralGenerator
{
    [RequireInterface(typeof(IGenerationModifier<IGenerationData, IInstanceGenerationData>))]
    [SerializeField] private Object _generatorModifierObject;
    private IGenerationModifier<IGenerationData, IInstanceGenerationData> GeneratorModifier => _generatorModifierObject as IGenerationModifier<IGenerationData, IInstanceGenerationData>;

    public IGenerationData Generate(int depth)
    {
        IInstanceGenerationData data = new InstanceGenerationData(new GenerationData(this), null, this);
        data = GeneratorModifier?.Modify(data) ?? data;

        if (depth < 1
            || data is not { Status: GenerationStatus.Success }
            || !data.HasInstanceGenerator()) 
            return data;

        IBranchingGenerationData branchingData = new BranchingGenerationData(data);
        branchingData.ChildrenData.Add(data.InstanceGenerator.Generate(depth - 1));
        return branchingData;
    }
}