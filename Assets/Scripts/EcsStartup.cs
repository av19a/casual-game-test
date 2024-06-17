using UnityEngine;
using Leopotam.EcsLite;

public class EcsStartup : MonoBehaviour
{
    EcsWorld _world;
    EcsSystems _systems;
    
    public Joystick joystick;
    public PlayerInitializer playerInitializer; // Reference to the PlayerInitializer script

    void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        
        // Initialize the player
        playerInitializer.Initialize(_world);
    
        var joystickSystem = new JoystickSystem();
        joystickSystem.Initialize(joystick);
    
        var movementSystem = new MovementSystem();
    
        _systems
            .Add(joystickSystem)
            .Add(new StackSystem())
            .Add(new SelectionSystem())
            .Add(new ResetSystem())
            .Add(new DropSystem())
            .Add(new ObjectGeneratorSystem())
            .Add(movementSystem) // Add the MovementSystem
            .Init();
    }

    void Update()
    {
        _systems.Run();
    }
    
    void OnDestroy()
    {
        _systems.Destroy();
        _world.Destroy();
    }
}