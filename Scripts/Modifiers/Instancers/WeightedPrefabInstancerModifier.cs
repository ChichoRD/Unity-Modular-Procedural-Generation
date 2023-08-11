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

    [SerializeField] private WeightedPrefab[] _weightedPrefabs;

    public GenerationData Modify(GenerationData generationData)
    {
        WeightedPrefab selected = RandomExtensions.GetWeightedRandom(_weightedPrefabs);
        var instance = Instantiate(selected.prefab, transform.position, transform.rotation);
        generationData.foundGenerator = instance.GetComponentInChildren<IProceduralGenerator>();
        return generationData;
    }
}