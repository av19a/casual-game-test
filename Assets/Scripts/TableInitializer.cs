using UnityEngine;
using Leopotam.EcsLite;
using System.Collections.Generic;

public class TableInitializer : MonoBehaviour
{
    public Transform stackPoint;
    // public Transform playerTransform; // Reference to the player's transform
    public float stackHeight = 1f;
    
    public void Initialize(EcsWorld world)
    {
        var playerEntity = world.NewEntity();
        // var movePool = world.GetPool<MovementComponent>();
        var stackPool = world.GetPool<StackComponent>();
        // var selectablePool = world.GetPool<SelectableComponent>();
        // var resetPool = world.GetPool<ResetComponent>();
        // var playerPool = world.GetPool<PlayerComponent>();
        var tablePool = world.GetPool<TableComponent>();
    
        // ref var moveComponent = ref movePool.Add(playerEntity);
        // ref var stackComponent = ref stackPool.Add(playerEntity);
        // stackComponent.StackPoint = stackPoint;
        // stackComponent.StackHeight = stackHeight;
        // stackComponent.Items = new Stack<GameObject>();
        
        ref var tableComponent = ref tablePool.Add(playerEntity);
        tableComponent.StackPoint = stackPoint;
        tableComponent.StackHeight = stackHeight;
        tableComponent.Items = new Stack<GameObject>();
    
        // selectablePool.Add(playerEntity);
        // resetPool.Add(playerEntity);
        // ref var playerComponent = ref playerPool.Add(playerEntity);
        // playerComponent.Transform = playerTransform;
    }
}
