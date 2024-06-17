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
    
        // Debug.Log("Entities with MovementComponent and PlayerComponent: " + filter.GetEntitiesCount());
        
        foreach (var i in filter)
        {
            ref var moveComponent = ref movementPool.Get(i);
            ref var playerComponent = ref playerPool.Get(i);

            Vector3 direction = moveComponent.Direction;
            float speed = 5f; // Adjust the speed as needed
            playerComponent.Transform.Translate(direction * speed * Time.deltaTime);

            // Rotate the "Face" child object in the direction of motion
            if (direction != Vector3.zero) // Avoid LookRotation error when direction is zero
            {
                Transform faceTransform = playerComponent.Transform.Find("Player");
                if (faceTransform != null)
                {
                    Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                    faceTransform.rotation = Quaternion.RotateTowards(faceTransform.rotation, toRotation, speed * 100 * Time.deltaTime);
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