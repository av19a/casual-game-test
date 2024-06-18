using UnityEngine;
using Leopotam.EcsLite;

public class MovementSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var filter = world.Filter<MovementComponent>().Inc<PlayerComponent>().End();
        var movementPool = world.GetPool<MovementComponent>();
        var playerPool = world.GetPool<PlayerComponent>();
        var stackPool = world.GetPool<StackComponent>();
    
        // Debug.Log("Entities with MovementComponent and PlayerComponent: " + filter.GetEntitiesCount());
        
        foreach (var i in filter)
        {
            ref var moveComponent = ref movementPool.Get(i);
            ref var playerComponent = ref playerPool.Get(i);
            ref var stackComponent = ref stackPool.Get(i);

            Vector3 direction = moveComponent.Direction;
            float maxSpeed = 5f; // Adjust the maximum speed as needed
            float targetSpeed = direction.magnitude * maxSpeed; // Speed depends on joystick positioning
            playerComponent.Transform.Translate(direction * targetSpeed * Time.deltaTime);
            
            // Set the "Speed" parameter in the Animator
            Animator animator = playerComponent.Transform.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.SetBool("InHand", stackComponent.IsHolding);

                animator.SetFloat("Speed", targetSpeed);
            }

            // Rotate the object in the direction of motion
            if (direction != Vector3.zero) // Avoid LookRotation error when direction is zero
            {
                Transform faceTransform = playerComponent.Transform.Find("Player");
                if (faceTransform != null)
                {
                    Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                    faceTransform.rotation = Quaternion.RotateTowards(faceTransform.rotation, toRotation, targetSpeed * 100 * Time.deltaTime);
                }
                else
                {
                    Debug.LogError("Face object not found");
                }
            }
            
            // Debug.Log($"Player Movement: Direction = {direction}, Position = {playerComponent.Transform.position}");
        }
    }
}