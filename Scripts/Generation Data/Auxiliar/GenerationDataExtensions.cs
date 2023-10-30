public static class GenerationDataExtensions
{
    public static bool HasInstanceGenerator(this IGenerationData<GeneratedInstanceData> instanceGenerationData) =>
        instanceGenerationData.Data.InstanceGenerator != null;
}