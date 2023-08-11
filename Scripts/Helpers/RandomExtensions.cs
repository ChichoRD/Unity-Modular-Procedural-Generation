using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandomExtensions
{
    public static T GetWeightedRandom<T>(T[] objects, float[] weights)
    {
        var totalWeight = weights.Sum();

        var randomWeight = Random.Range(0.0f, totalWeight);
        var acumulatedWeight = 0.0f;
        for (var i = 0; i < objects.Length; i++)
        {
            acumulatedWeight += weights[i];
            if (randomWeight < acumulatedWeight) return objects[i];
        }

        return objects[^1];
    }

    public static T GetWeightedRandom<T>(IEnumerable<T> objects) where T : IRandomWeightable
    {
        var totalWeight = objects.Sum(obj => obj.Weight);

        var randomWeight = Random.Range(0.0f, totalWeight);
        var acumulatedWeight = 0.0f;
        foreach (var obj in objects)
        {
            acumulatedWeight += obj.Weight;
            if (randomWeight < acumulatedWeight) return obj;
        }

        return objects.Last();
    }
}
