using UnityEngine;

public interface IInstancerModifier : IGenerationModifier
{
    bool TryInstantiate(IModuleDataProvider moduleData, ref InstanceGenerationData generationData);
}