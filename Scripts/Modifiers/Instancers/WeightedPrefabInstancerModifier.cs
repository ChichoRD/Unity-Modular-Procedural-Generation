using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class WeightedPrefabInstancerModifier : MonoBehaviour, IGenerationModifier<IGenerationData, IInstanceGenerationData>
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

    public IInstanceGenerationData Modify(IGenerationData generationData)
    {
        WeightedPrefab selected = _weightedPrefabs.GetWeightedRandom();
        if (selected.PrefabModuleDataProvider == null)
            return new InstanceGenerationData(new GenerationData(generationData.Generator, GenerationStatus.Failed), null, null);

        PrefabInstancerModifier.TryInstantiate(selected.PrefabModuleDataProvider, generationData, out var instanceGenerationData);
        return instanceGenerationData;
    }
}
