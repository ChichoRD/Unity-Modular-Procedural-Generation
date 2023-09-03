using System.Linq;
using UnityEngine;

public class PrefabInstancerModifier : MonoBehaviour, IGenerationModifier<IGenerationData, IInstanceGenerationData>, IInstancerModifier
{
    [RequireInterface(typeof(IModuleDataProvider), typeof(GameObject))]
    [SerializeField] private Object _prefabModuleDataProvider;
    private IModuleDataProvider PrefabModuleDataProvider => _prefabModuleDataProvider as IModuleDataProvider;

    [SerializeField] private ConnectionAnchor _connectionAnchor;
    [SerializeField] private AnchorLayer _validAnchorLayers;
    [SerializeField] private LayerMask _modulesPhysicsOverlapCheckLayers;
    [SerializeField] private bool _destroyOnFailedToInstantiate = true;
    public bool DestroyOnFailedToInstantiate => _destroyOnFailedToInstantiate;

    public bool TryInstantiate(IModuleDataProvider moduleData, in IGenerationData generationData, out IInstanceGenerationData instanceGenerationData)
    {
        GameObject instanceRoot = Instantiate(moduleData.RootTransform.gameObject);
        instanceRoot.SetActive(false);

        moduleData = instanceRoot.GetComponentInChildren<IModuleDataProvider>();
        IProceduralGenerator instanceGenerator = instanceRoot.GetComponentInChildren<IProceduralGenerator>();

        var connectionAnchors = moduleData.ConnectionAnchors.Where(a => _validAnchorLayers.HasFlag(a.Layer)).OrderBy(_ => Random.value);
        foreach (var connectionAnchor in connectionAnchors)
        {
            Vector3 anchorNormalAxis = Vector3.Cross(_connectionAnchor.Transform.forward, connectionAnchor.Transform.forward);
            float anchorAngleDifference = Vector3.SignedAngle(_connectionAnchor.Transform.forward, connectionAnchor.Transform.forward, anchorNormalAxis);
            instanceRoot.transform.RotateAround(connectionAnchor.Transform.position, anchorNormalAxis, -anchorAngleDifference);

            Vector3 anchoredPosition = _connectionAnchor.ConnectRootWithAnchor(connectionAnchor, instanceRoot.transform.position);
            instanceRoot.transform.position = anchoredPosition;


            Bounds bounds = moduleData.GetBounds();            
            if (!Physics.CheckBox(bounds.center, bounds.extents, instanceRoot.transform.rotation, _modulesPhysicsOverlapCheckLayers))
            {
                instanceRoot.SetActive(true);

                instanceGenerationData = new InstanceGenerationData(generationData, instanceRoot, instanceGenerator);
                return true;
            }
        }

        instanceGenerationData = new InstanceGenerationData(new GenerationData(generationData.Generator, GenerationStatus.Failed), instanceRoot, instanceGenerator);
        return false;
    }

    public IInstanceGenerationData Modify(IGenerationData generationData)
    {
        TryInstantiate(PrefabModuleDataProvider, in generationData, out IInstanceGenerationData instanceGenerationData);
        return instanceGenerationData;
    }
}
