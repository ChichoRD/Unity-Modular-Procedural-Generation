using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class InitializationGenerator : MonoBehaviour, IProceduralGenerator
{
    [Serializable]
    private struct WeightedGeneratorPoolEntry : IRandomWeightable
    {
        [RequireInterface(typeof(IProceduralGenerator))]
        [SerializeField]
        private Object _generator;
        public readonly IProceduralGenerator Generator => _generator as IProceduralGenerator;

        [SerializeField]
        [Min(0.0f)]
        private float _weight;
        public readonly float Weight => _weight;
    }

    [SerializeField]
    private bool _generateOnStart = true;

    [SerializeField]
    [Min(0)]
    private int _depth = 0;

    [SerializeField]
    private WeightedGeneratorPoolEntry[] _weightedGenerators;
    private IGenerationData _generationData = new GenerationData(default, GenerationStatus.Failed);

    // TODO - Maybe segregate
    public IGenerationData Generate(int depth) => _generationData;
    private void Start() => _ = !_generateOnStart || ((_generationData = _weightedGenerators.GetRandom().Generator.Generate(_depth)) != null);
}