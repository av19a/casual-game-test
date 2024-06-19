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
                    stackComponent.IsHolding = false;
                    item.transform.position = stackComponent.StackPoint.position + new Vector3(Random.Range(-5, 5), 0, 0);
                    item.transform.SetParent(null);
                    item.GetComponent<Collider>().enabled = true;
                    // stackComponent.StackCountText.text = stackComponent.Items.Count.ToString();
                    // Add any additional reset logic here
                }
            }
        }
    }
}