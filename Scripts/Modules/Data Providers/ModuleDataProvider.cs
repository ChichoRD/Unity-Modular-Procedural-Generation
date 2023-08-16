using System.Collections.Generic;
using UnityEngine;

public class ModuleDataProvider : MonoBehaviour, IModuleDataProvider
{
    [SerializeField] private Transform _rootTransform;
    public Transform RootTransform => _rootTransform;

    [SerializeField] private ConnectionAnchor[] _connectionAnchors;
    public IEnumerable<ConnectionAnchor> ConnectionAnchors => _connectionAnchors;

    [RequireInterface(typeof(IModuleBoundsProvider))]
    [SerializeField] private Object _boundsProviderObject;
    private IModuleBoundsProvider BoundsProvider => _boundsProviderObject as IModuleBoundsProvider;

    public bool TryGetGenerator(out IProceduralGenerator generator)
    {
        generator = GetComponent<IProceduralGenerator>();
        return generator != null;
    }

    public Bounds GetBounds() => BoundsProvider.GetBounds();

    [ContextMenu(nameof(SearchConnectionAnchors))]
    private void SearchConnectionAnchors() => _connectionAnchors = GetComponentsInChildren<ConnectionAnchor>();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        var debugBounds = GetBounds();
        Gizmos.DrawWireCube(debugBounds.center, debugBounds.size);
    }
}