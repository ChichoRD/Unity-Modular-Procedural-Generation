using System.Collections.Generic;
using UnityEngine;

public class SearchGenerator : MonoBehaviour, IProceduralGenerator
{
    [RequireInterface(typeof(IGenerationModifier<IGenerationData, IGenerationData<GeneratedInstanceData>>))]
    [SerializeField] private Object _generatorModifierObject;
    private IGenerationModifier<IGenerationData, IGenerationData<GeneratedInstanceData>> GeneratorModifier => _generatorModifierObject as IGenerationModifier<IGenerationData, IGenerationData<GeneratedInstanceData>>;

    public IGenerationData Generate(int depth)
    {
        IGenerationData data = new GenerationData(this, GenerationStatus.Failed);
        IGenerationData<GeneratedInstanceData>  instanceData = GeneratorModifier?.Modify(data)
                                                               ?? new InstanceGenerationData(data, default);

        if (depth < 1
            || instanceData is not { Status: GenerationStatus.Success }
            || !instanceData.HasInstanceGenerator()) 
            return instanceData;

        IGenerationData<GeneratedBranchData> branchingData = new BranchingGenerationData(
            instanceData,
            new GeneratedBranchData(new List<IGenerationData>()));

        branchingData.Data.ChildrenData.Add(instanceData.Data.InstanceGenerator.Generate(depth - 1));
        return branchingData;
    }
}