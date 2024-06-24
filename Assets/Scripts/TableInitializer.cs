using UnityEngine;
using Leopotam.EcsLite;
using System.Collections.Generic;

public class TableInitializer : MonoBehaviour
{
    public Transform stackPoint;
    public float stackHeight = 1f;
    
    public void Initialize(EcsWorld world)
    {
        var playerEntity = world.NewEntity();
        var stackPool = world.GetPool<StackComponent>();
        var tablePool = world.GetPool<TableComponent>();
        
        ref var tableComponent = ref tablePool.Add(playerEntity);
        tableComponent.StackPoint = stackPoint;
        tableComponent.StackHeight = stackHeight;
        tableComponent.Items = new Stack<GameObject>();
    }
}
