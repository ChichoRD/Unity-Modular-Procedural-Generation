public interface IInstancerModifier : IGenerationModifier
{
    bool TryInstantiate(IModuleDataProvider moduleData, in IGenerationData generationData, out IInstanceGenerationData instanceGenerationData);

    bool DestroyOnFailedToInstantiate { get; }
}