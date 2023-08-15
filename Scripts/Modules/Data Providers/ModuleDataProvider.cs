using System.Collections.Generic;
using UnityEngine;

public class ModuleDataProvider : MonoBehaviour, IModuleDataProvider
{
    [SerializeField] private Transform _rootTransform;
    public Transform RootTransform => _rootTransform;

    [SerializeField] private ConnectionAnchor[] _connectionAnchors;
    public IEnumerable<ConnectionAnchor> ConnectionAnchors => _connectionAnchors;

    [SerializeField] private float _boundsExpandBias = -0.05f;

    private Bounds? _cachedDebugBounds;

    public bool TryGetGenerator(out IProceduralGenerator generator)
    {
        generator = GetComponent<IProceduralGenerator>();
        return generator != null;
    }

    public Bounds GetBounds()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
            return new Bounds();

        var bounds = renderers[0].bounds;
        foreach (var renderer in renderers)
            bounds.Encapsulate(renderer.bounds);

        bounds.Expand(_boundsExpandBias);
        return bounds;
    }

    [ContextMenu(nameof(SearchConnectionAnchors))]
    private void SearchConnectionAnchors() => _connectionAnchors = GetComponentsInChildren<ConnectionAnchor>();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        _cachedDebugBounds = GetBounds();
        Gizmos.DrawWireCube(_cachedDebugBounds.Value.center, _cachedDebugBounds.Value.size);
    }
}