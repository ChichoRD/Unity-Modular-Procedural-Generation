using System;
using UnityEngine;

public class WeightedPrefabInstancerModifier : MonoBehaviour, IGenerationModifier
{
    [Serializable]
    private struct WeightedPrefab : IRandomWeightable
    {
        public GameObject prefab;
        [Min(0.0f)] public float weight;
        public readonly float Weight => weight;
    }

    [SerializeField] private PrefabInstancerModifier _prefabInstancerModifier;
    [SerializeField] private WeightedPrefab[] _weightedPrefabs;

    public GenerationData Modify(GenerationData generationData)
    {
        WeightedPrefab selected = _weightedPrefabs.GetWeightedRandom();
        _prefabInstancerModifier.InstantiateAndPosition(selected.prefab, ref generationData);
        return generationData;
    }
}