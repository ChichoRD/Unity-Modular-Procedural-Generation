using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class WeightedPrefabInstancerModifier : MonoBehaviour, IInstancerModifier
{
    [Serializable]
    private struct WeightedPrefab : IRandomWeightable
    {
        [RequireInterface(typeof(IModuleDataProvider), typeof(GameObject))]
        [SerializeField] private Object _prefabModuleDataProvider;
        public readonly IModuleDataProvider PrefabModuleDataProvider => _prefabModuleDataProvider as IModuleDataProvider;

        [SerializeField] [Min(0.0f)] private float weight;
        public readonly float Weight => weight;
    }

    [RequireInterface(typeof(IInstancerModifier))]
    [SerializeField] private Object _prefabInstancerModifierObject;
    private IInstancerModifier PrefabInstancerModifier => _prefabInstancerModifierObject as IInstancerModifier;
    public bool DestroyOnFailedToInstantiate => PrefabInstancerModifier.DestroyOnFailedToInstantiate;

    [SerializeField] private WeightedPrefab[] _weightedPrefabs;

    public bool TryInstantiate(IModuleDataProvider moduleData, in IGenerationData generationData, out IInstanceGenerationData instanceGenerationData)
    {
        return PrefabInstancerModifier.TryInstantiate(moduleData, generationData, out instanceGenerationData);
    }

    public IGenerationData Modify(IGenerationData generationData)
    {
        WeightedPrefab selected = _weightedPrefabs.GetWeightedRandom();
        if (!TryInstantiate(selected.PrefabModuleDataProvider, generationData, out var instanceGenerationData)
            && DestroyOnFailedToInstantiate)
            Destroy(instanceGenerationData.InstanceRoot);

        return instanceGenerationData;
    }
}
