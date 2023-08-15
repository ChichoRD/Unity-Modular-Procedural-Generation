using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class WeightedPrefabInstancerModifier : MonoBehaviour, IInstancerModifier
{
    [Serializable]
    private struct WeightedPrefab : IRandomWeightable
    {
        [RequireInterface(typeof(IModuleDataProvider), typeof(GameObject))]
        [SerializeField] public Object _prefabModuleDataProvider;
        public readonly IModuleDataProvider PrefabModuleDataProvider => _prefabModuleDataProvider as IModuleDataProvider;

        [Min(0.0f)] public float weight;
        public readonly float Weight => weight;
    }

    [RequireInterface(typeof(IInstancerModifier))]
    [SerializeField] private Object _prefabInstancerModifierObject;
    private IInstancerModifier PrefabInstancerModifier => _prefabInstancerModifierObject as IInstancerModifier;

    [SerializeField] private WeightedPrefab[] _weightedPrefabs;

    public T Modify<T>(T generationData) where T : struct, IGenerationData
    {
        WeightedPrefab selected = _weightedPrefabs.GetWeightedRandom();
        if (!TryInstantiate(selected.PrefabModuleDataProvider, ref generationData))
        {
            Destroy(instance);
        }

        return generationData;
    }

    public bool TryInstantiate(IModuleDataProvider moduleData, ref InstanceGenerationData generationData)
    {
        return PrefabInstancerModifier.TryInstantiate(moduleData, ref generationData);
    }
}
