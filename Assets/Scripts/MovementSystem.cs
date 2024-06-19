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

        foreach (var i in filter)
        {
            ref var moveComponent = ref movementPool.Get(i);
            ref var playerComponent = ref playerPool.Get(i);
            ref var stackComponent = ref stackPool.Get(i);

            Vector3 direction = moveComponent.Direction;
            float maxSpeed = 10f; // Adjust the maximum speed as needed
            float targetSpeed = direction.magnitude * maxSpeed; // Speed depends on joystick positioning
            
            // Get the Rigidbody component and set its velocity
            Rigidbody rb = playerComponent.Transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 newPosition = playerComponent.Transform.position + direction * targetSpeed * Time.deltaTime;
                rb.MovePosition(newPosition);
            }
            
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
                Transform playerTransform = playerComponent.Transform.Find("Player");
                if (playerTransform != null)
                {
                    Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                    playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, toRotation, targetSpeed * 100 * Time.deltaTime);
                }
            }
        }
    }
}