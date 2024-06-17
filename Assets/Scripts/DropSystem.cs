using Leopotam.EcsLite;
using UnityEngine;

public class DropSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var filter = world.Filter<StackComponent>().Inc<ResetComponent>().End();
        var stackPool = world.GetPool<StackComponent>();

        if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (var i in filter)
            {
                ref var stackComponent = ref stackPool.Get(i);
                if (stackComponent.Items.Count > 0)
                {
                    GameObject item = stackComponent.Items.Pop();
                    item.transform.position = stackComponent.StackPoint.position + new Vector3(3, 0, 0);
                    item.transform.SetParent(null);
                    item.GetComponent<Collider>().enabled = true;
                    // Add any additional drop logic here
                }
            }
        }
    }
}
