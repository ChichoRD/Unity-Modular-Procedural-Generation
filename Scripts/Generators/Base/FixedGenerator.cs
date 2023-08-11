using UnityEngine;

public class FixedGenerator : MonoBehaviour, IProceduralGenerator
{
    [RequireInterface(typeof(IGenerationModifier))]
    [SerializeField] private Object _generationModifierObject;
    private IGenerationModifier GenerationModifier => _generationModifierObject as IGenerationModifier;

    public GenerationData Generate(int depth)
    {
        GenerationData data = new GenerationData(this);
        return GenerationModifier == null ? data : GenerationModifier.Modify(data);
    }
}
