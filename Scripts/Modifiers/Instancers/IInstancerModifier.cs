public interface IInstancerModifier
{
    bool TryInstantiate(IModuleDataProvider moduleData, in IGenerationData generationData, out IInstanceGenerationData instanceGenerationData);

    bool DestroyOnFailedToInstantiate { get; }
}