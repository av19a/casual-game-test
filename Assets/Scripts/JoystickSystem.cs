using UnityEngine;
using Leopotam.EcsLite;

public class JoystickSystem : IEcsRunSystem
{
    private Joystick _joystick;

    public void Initialize(Joystick joystick)
    {
        _joystick = joystick;
    }

    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var filter = world.Filter<MovementComponent>().End();
        var movementPool = world.GetPool<MovementComponent>();
    
        foreach (var i in filter)
        {
            ref var moveComponent = ref movementPool.Get(i);
            moveComponent.Direction = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            Debug.Log($"Joystick Input: Horizontal = {_joystick.Horizontal}, Vertical = {_joystick.Vertical}");
        }
    }
}