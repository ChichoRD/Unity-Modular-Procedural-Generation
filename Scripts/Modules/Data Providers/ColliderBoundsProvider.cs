using UnityEngine;

public class ColliderBoundsProvider : MonoBehaviour, IModuleBoundsProvider
{
    private Bounds? _cachedBounds;
    [SerializeField] private Collider[] colliders;
    [field: SerializeField] public bool UseCache { get; private set; } = true;
    [field: SerializeField] public float BoundsExpandBias { get; private set; } = -0.05f;

    public Bounds GetBounds()
    {
        if (UseCache && _cachedBounds.HasValue)
            return _cachedBounds.Value;

        if (colliders.Length == 0)
            return new Bounds();

        Bounds bounds = colliders[0].bounds;
        foreach (var collider in colliders)
            bounds.Encapsulate(collider.bounds);

        bounds.Expand(BoundsExpandBias);
        return (_cachedBounds = bounds).Value;
    }
}