using UnityEngine;
using Leopotam.EcsLite;
using System.Collections.Generic;
using TMPro;

public class PlayerInitializer : MonoBehaviour
{
    public Transform stackPoint;
    public Transform playerTransform; // Reference to the player's transform
    public float stackHeight = 1f;
    public TMP_Text stackCountText;
    
    public void Initialize(EcsWorld world)
    {
        var playerEntity = world.NewEntity();
        var movePool = world.GetPool<MovementComponent>();
        var stackPool = world.GetPool<StackComponent>();
        var selectablePool = world.GetPool<SelectableComponent>();
        var resetPool = world.GetPool<ResetComponent>();
        var playerPool = world.GetPool<PlayerComponent>();
    
        ref var moveComponent = ref movePool.Add(playerEntity);
        ref var stackComponent = ref stackPool.Add(playerEntity);
        stackComponent.StackPoint = stackPoint;
        stackComponent.StackHeight = stackHeight;
        stackComponent.Items = new Stack<GameObject>();
        stackComponent.StackCountText = stackCountText;
    
        selectablePool.Add(playerEntity);
        resetPool.Add(playerEntity);
        ref var playerComponent = ref playerPool.Add(playerEntity);
        playerComponent.Transform = playerTransform;
    }
}