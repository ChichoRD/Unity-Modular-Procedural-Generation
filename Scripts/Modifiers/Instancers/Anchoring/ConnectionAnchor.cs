using UnityEngine;

public class ConnectionAnchor : MonoBehaviour
{
    [field: SerializeField] public Transform Transform { get; private set; }
    [field: SerializeField] public AnchorLayer Layer { get; private set; } = AnchorLayer.InAnchorLayer0;

    public Vector3 ConnectRootWithAnchor(ConnectionAnchor rootAnchor, Vector3 rootWorldPosition)
    {
        Vector3 rootFromAnchor = rootAnchor.Transform.InverseTransformPoint(rootWorldPosition);
        return Transform.TransformPoint(rootFromAnchor);
    }
}
