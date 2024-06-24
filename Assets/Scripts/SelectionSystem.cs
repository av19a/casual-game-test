using Leopotam.EcsLite;
using UnityEngine;

public class SelectionSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var filter = world.Filter<StackComponent>().Inc<SelectableComponent>().End();
        var stackPool = world.GetPool<StackComponent>();

        if (Input.GetKeyDown(KeyCode.S))
        {
            foreach (var i in filter)
            {
                ref var stackComponent = ref stackPool.Get(i);
            }
        }
    }
}