using UnityEngine;
using Leopotam.EcsLite;

public class ObjectGeneratorSystem : IEcsRunSystem
{
    private float _nextGenerationTime = 0f;
    private float _generationInterval = 5f;

    public void Run(IEcsSystems systems)
    {
        if (Time.time >= _nextGenerationTime)
        {
            GenerateObject();
            _nextGenerationTime = Time.time + _generationInterval;
        }
    }

    private void GenerateObject()
    {
        // Implement object generation logic
        GameObject newItem = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newItem.transform.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
        newItem.tag = "Item";
        newItem.GetComponent<BoxCollider>().isTrigger = true;
        // newItem.AddComponent<BoxCollider>().isTrigger = true;
    }
}