using System.Linq;
using UnityEngine;

public class PrefabInstancerModifier : MonoBehaviour, IInstancerModifier
{
    [RequireInterface(typeof(IModuleDataProvider), typeof(GameObject))]
    [SerializeField] private Object _prefabModuleDataProvider;
    private IModuleDataProvider PrefabModuleDataProvider => _prefabModuleDataProvider as IModuleDataProvider;

    [SerializeField] private ConnectionAnchor _connectionAnchor;
    [SerializeField] private AnchorLayer _validAnchorLayers;
    [SerializeField] private LayerMask _modulesPhysicsOverlapCheckLayers;
    [SerializeField] private bool _destroyOnFailedToInstantiate = true;

    public bool TryInstantiate(IModuleDataProvider moduleData, ref InstanceGenerationData generationData)
    {
        GameObject instanceRoot = null;
        generationData.InstanceRoot = instanceRoot = Instantiate(moduleData.RootTransform.gameObject);
        moduleData = instanceRoot.GetComponentInChildren<IModuleDataProvider>();
        generationData.InstanceGenerator = instanceRoot.GetComponentInChildren<IProceduralGenerator>();
        //moduleData.TryGetGenerator(out generationData.foundGenerator);

        var connectionAnchors = moduleData.ConnectionAnchors.Where(a => _validAnchorLayers.HasFlag(a.Layer)).OrderBy(_ => Random.value);
        foreach (var connectionAnchor in connectionAnchors)
        {
            Vector3 anchorNormalAxis = Vector3.Cross(_connectionAnchor.Transform.forward, connectionAnchor.Transform.forward);
            float anchorAngleDifference = Vector3.SignedAngle(_connectionAnchor.Transform.forward, connectionAnchor.Transform.forward, anchorNormalAxis);
            instanceRoot.transform.RotateAround(connectionAnchor.Transform.position, anchorNormalAxis, -anchorAngleDifference);

            Vector3 anchoredPosition = _connectionAnchor.ConnectRootWithAnchor(connectionAnchor, instanceRoot.transform.position);
            instanceRoot.transform.position = anchoredPosition;

            instanceRoot.SetActive(false);

            Bounds bounds = moduleData.GetBounds();            
            if (!Physics.CheckBox(bounds.center, bounds.extents, instanceRoot.transform.rotation, _modulesPhysicsOverlapCheckLayers))
            {
                instanceRoot.SetActive(true);
                return true;
            }
        }

        generationData.InstanceGenerator = null;
        return false;
    }

    public T Modify<T>(T generationData) where T : struct, IGenerationData
    {
        if (!TryInstantiate(PrefabModuleDataProvider, ref generationData))
            Destroy(generationData.InstanceRoot);

        return generationData;
    }
}
