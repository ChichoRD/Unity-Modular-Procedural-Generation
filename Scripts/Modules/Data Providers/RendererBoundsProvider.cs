using UnityEngine;

public class RendererBoundsProvider : MonoBehaviour, IModuleBoundsProvider
{
    private Bounds? _cachedBounds;
    [field: SerializeField] public bool UseCache { get; private set; } = true;
    [field: SerializeField] public float BoundsExpandBias { get; private set; } = -0.05f;

    public Bounds GetBounds()
    {
        if (UseCache && _cachedBounds.HasValue)
            return _cachedBounds.Value;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
            return new Bounds();

        Bounds bounds = renderers[0].bounds;
        foreach (var renderer in renderers)
            bounds.Encapsulate(renderer.bounds);

        bounds.Expand(BoundsExpandBias);
        return (_cachedBounds = bounds).Value;
    }
}
