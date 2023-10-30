using UnityEngine;

public class FixedGenerator : MonoBehaviour, IProceduralGenerator
{
    [RequireInterface(typeof(IGenerationModifier<IGenerationData, IGenerationData>))]
    [SerializeField] private Object _generationModifierObject;
    private IGenerationModifier<IGenerationData, IGenerationData> GenerationModifier => _generationModifierObject as IGenerationModifier<IGenerationData, IGenerationData>;

    public IGenerationData Generate(int depth)
    {
        GenerationData data = new GenerationData(this, GenerationStatus.Failed);
        return GenerationModifier == null ? data : GenerationModifier.Modify(data);
    }
}
