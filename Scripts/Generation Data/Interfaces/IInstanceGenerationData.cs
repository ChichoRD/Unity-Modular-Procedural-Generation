using UnityEngine;

public interface IInstanceGenerationData : IGenerationData
{
    public GameObject InstanceRoot { get; }
    public IProceduralGenerator InstanceGenerator { get; }
}