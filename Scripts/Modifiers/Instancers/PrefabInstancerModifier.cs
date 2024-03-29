﻿using System.Linq;
using UnityEngine;

public class PrefabInstancerModifier : MonoBehaviour, IGenerationModifier<IGenerationData, IGenerationData<GeneratedInstanceData>>, IInstancerModifier
{
    [RequireInterface(typeof(IModuleDataProvider), typeof(GameObject))]
    [SerializeField] private Object _prefabModuleDataProvider;
    private IModuleDataProvider PrefabModuleDataProvider => _prefabModuleDataProvider as IModuleDataProvider;

    [SerializeField] private ConnectionAnchor _connectionAnchor;
    [SerializeField] private AnchorLayer _validAnchorLayers;
    [SerializeField] private LayerMask _modulesPhysicsOverlapCheckLayers;
    [SerializeField] private bool _destroyOnFailedToInstantiate = true;
    public bool DestroyOnFailedToInstantiate => _destroyOnFailedToInstantiate;

    public bool TryInstantiate(IModuleDataProvider moduleData, in IGenerationData generationData, out IGenerationData<GeneratedInstanceData> instanceGenerationData)
    {
        GameObject instanceRoot = Instantiate(moduleData.RootTransform.gameObject);
        instanceRoot.SetActive(false);

        moduleData = instanceRoot.GetComponentInChildren<IModuleDataProvider>();
        moduleData.TryGetGenerator(out IProceduralGenerator instanceGenerator);

        var connectionAnchors = moduleData.ConnectionAnchors.Where(a => _validAnchorLayers.HasFlag(a.Layer)).OrderBy(_ => Random.value);
        foreach (var connectionAnchor in connectionAnchors)
        {
            Quaternion anchorTargetRotation = _connectionAnchor.Transform.rotation;
            Quaternion rotationCorrection = anchorTargetRotation * Quaternion.Inverse(connectionAnchor.Transform.localRotation);
            instanceRoot.transform.rotation = rotationCorrection;

            Vector3 anchoredPosition = _connectionAnchor.ConnectRootWithAnchor(connectionAnchor, instanceRoot.transform.position);
            instanceRoot.transform.position = anchoredPosition;

            Bounds bounds = moduleData.GetBounds();            
            if (!Physics.CheckBox(bounds.center, bounds.extents, instanceRoot.transform.rotation, _modulesPhysicsOverlapCheckLayers))
            {
                instanceRoot.SetActive(true);

                instanceGenerationData = new InstanceGenerationData(
                    new GenerationData(generationData.Generator, GenerationStatus.Success),
                    new GeneratedInstanceData(instanceRoot, instanceGenerator));
                return true;
            }
        }

        instanceGenerationData = new InstanceGenerationData(
            new GenerationData(generationData.Generator, GenerationStatus.Failed),
            new GeneratedInstanceData(instanceRoot, instanceGenerator));
        return false;
    }

    public IGenerationData<GeneratedInstanceData> Modify(IGenerationData generationData)
    {
        TryInstantiate(PrefabModuleDataProvider, in generationData, out IGenerationData<GeneratedInstanceData> instanceGenerationData);
        return instanceGenerationData;
    }
}
