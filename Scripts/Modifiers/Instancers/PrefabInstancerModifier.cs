using UnityEngine;

public class PrefabInstancerModifier : MonoBehaviour, IGenerationModifier
{
    [SerializeField] private GameObject _prefab;

    public GenerationData Modify(GenerationData generationData)
    {
        var instance = Instantiate(_prefab, transform.position, transform.rotation);
        generationData.foundGenerator = instance.GetComponentInChildren<IProceduralGenerator>();
        return generationData;
    }
}
