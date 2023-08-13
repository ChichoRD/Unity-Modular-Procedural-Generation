using System.Linq;
using UnityEngine;

public class PrefabInstancerModifier : MonoBehaviour, IGenerationModifier
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private ConnectionAnchor _connectionAnchor;
    [SerializeField] private AnchorLayer _validAnchorLayers;

    public GameObject InstantiateAndPosition(GameObject prefab, ref GenerationData generationData)
    {
        GameObject instance = Instantiate(prefab);
        generationData.foundGenerator = instance.GetComponentInChildren<IProceduralGenerator>();

        var instanceAnchors = instance.GetComponentsInChildren<ConnectionAnchor>().Where(a => _validAnchorLayers.HasFlag(a.Layer));
        ConnectionAnchor selectedInstanceAnchor = instanceAnchors.GetRandom();

        Vector3 anchorNormalAxis = Vector3.Cross(_connectionAnchor.Transform.forward, selectedInstanceAnchor.Transform.forward);
        float anchorAngleDifference = Vector3.SignedAngle(_connectionAnchor.Transform.forward, selectedInstanceAnchor.Transform.forward, anchorNormalAxis);
        if (anchorAngleDifference > 0.01f)
            instance.transform.RotateAround(selectedInstanceAnchor.Transform.position, anchorNormalAxis, -anchorAngleDifference);

        GameObject testA = Instantiate(prefab, instance.transform.position, instance.transform.rotation);
        testA.name = $"testA: {instance.name}";
        testA.SetActive(false);

        Vector3 anchoredPosition = _connectionAnchor.ConnectRootWithAnchor(selectedInstanceAnchor, instance.transform.position);
        instance.transform.position = anchoredPosition;

        return instance;
    }

    public GenerationData Modify(GenerationData generationData)
    {
        InstantiateAndPosition(_prefab, ref generationData);
        return generationData;
    }
}
