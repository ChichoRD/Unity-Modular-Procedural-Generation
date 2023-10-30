public interface IInstancerModifier
{
    bool TryInstantiate(IModuleDataProvider moduleData, in IGenerationData generationData, out IGenerationData<GeneratedInstanceData> instanceGenerationData);

    bool DestroyOnFailedToInstantiate { get; }
}