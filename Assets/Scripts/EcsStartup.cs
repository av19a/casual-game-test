using UnityEngine;
using Leopotam.EcsLite;
using UnityEngine.UI;

public class EcsStartup : MonoBehaviour
{
    EcsWorld _world;
    EcsSystems _systems;
    
    public Joystick joystick;
    public PlayerInitializer playerInitializer; // Reference to the PlayerInitializer script
    public TableInitializer tableInitializer; // Reference to the PlayerInitializer script
    public GameObject prefab;
    public Button dropButton; // Reference to the drop button
    public Button resetButton; // Reference to the reset button
    
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
        
        var dropSystem = new DropSystem();
        dropSystem.Initialize(dropButton);
        
        var resetSystem = new ResetSystem();
        resetSystem.Initialize(resetButton);
    
        var movementSystem = new MovementSystem();
    
        _systems
            .Add(joystickSystem)
            .Add(new StackSystem())
            .Add(new TableSystem())
            .Add(new SelectionSystem())
            .Add(resetSystem)
            .Add(dropSystem)
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