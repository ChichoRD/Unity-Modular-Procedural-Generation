public static class GenerationDataExtensions
{
    public static bool HasInstanceGenerator(this IInstanceGenerationData instanceGenerationData) => instanceGenerationData.InstanceGenerator != null;
}