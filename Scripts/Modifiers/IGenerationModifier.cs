public interface IGenerationModifier
{
    T Modify<T>(T generationData) where T : struct, IGenerationData;
}
