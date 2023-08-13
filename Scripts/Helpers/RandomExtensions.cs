using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandomExtensions
{
    public static T GetRandom<T>(this IEnumerable<T> objects) => objects.ElementAtOrDefault(Random.Range(0, objects.Count()));

    public static T GetWeightedRandom<T>(IEnumerable<T> objects, IEnumerable<float> weights)
    {
        var totalWeight = weights.Sum();

        var randomWeight = Random.Range(0.0f, totalWeight);
        var acumulatedWeight = 0.0f;
        for (var i = 0; i < objects.Count(); i++)
        {
            acumulatedWeight += weights.ElementAt(i);
            if (randomWeight < acumulatedWeight) return objects.ElementAt(i);
        }

        return objects.LastOrDefault();
    }

    public static T GetWeightedRandom<T>(this IEnumerable<T> objects) where T : IRandomWeightable
    {
        var totalWeight = objects.Sum(obj => obj.Weight);

        var randomWeight = Random.Range(0.0f, totalWeight);
        var acumulatedWeight = 0.0f;
        foreach (var obj in objects)
        {
            acumulatedWeight += obj.Weight;
            if (randomWeight < acumulatedWeight) return obj;
        }

        return objects.LastOrDefault();
    }
}
