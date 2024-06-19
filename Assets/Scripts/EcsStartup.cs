using UnityEngine;
using Leopotam.EcsLite;

public class EcsStartup : MonoBehaviour
{
    EcsWorld _world;
    EcsSystems _systems;
    
    public Joystick joystick;
    public PlayerInitializer playerInitializer; // Reference to the PlayerInitializer script
    public TableInitializer tableInitializer; // Reference to the PlayerInitializer script
    public GameObject prefab;
    
    void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        
        // Initialize the player
        playerInitializer.Initialize(_world);
        tableInitializer.Initialize(_world);
    
        var joystickSystem = new JoystickSystem();
        joystickSystem.Initialize(joystick);

        var objectGeneratorSystem = new ObjectGeneratorSystem();
        objectGeneratorSystem.prefab = prefab;
    
        var movementSystem = new MovementSystem();
    
        _systems
            .Add(joystickSystem)
            .Add(new StackSystem())
            .Add(new TableSystem())
            .Add(new SelectionSystem())
            .Add(new ResetSystem())
            .Add(new DropSystem())
            .Add(objectGeneratorSystem)
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