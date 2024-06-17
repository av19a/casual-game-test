using Leopotam.EcsLite;
using UnityEngine;

public class ResetSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var filter = world.Filter<StackComponent>().Inc<ResetComponent>().End();
        var stackPool = world.GetPool<StackComponent>();

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var i in filter)
            {
                ref var stackComponent = ref stackPool.Get(i);
                while (stackComponent.Items.Count > 0)
                {
                    GameObject item = stackComponent.Items.Pop();
                    item.transform.SetParent(null);
                    item.GetComponent<Collider>().enabled = true;
                    // Add any additional reset logic here
                }
            }
        }
    }
}