using UnityEngine;
using Leopotam.EcsLite;

public class TableSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var tableFilter = world.Filter<TableComponent>().End();
        var playerFilter = world.Filter<StackComponent>().Inc<PlayerComponent>().End();
        var tablePool = world.GetPool<TableComponent>();
        var stackPool = world.GetPool<StackComponent>();

        foreach (var tableEntity in tableFilter)
        {
            ref var tableComponent = ref tablePool.Get(tableEntity);
            Collider[] hitColliders = Physics.OverlapSphere(tableComponent.StackPoint.position, 1f);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    foreach (var playerEntity in playerFilter)
                    {
                        ref var stackComponent = ref stackPool.Get(playerEntity);
                        while (stackComponent.Items.Count > 0)
                        {
                            GameObject item = stackComponent.Items.Pop();
                            item.transform.SetParent(tableComponent.StackPoint);
                            item.transform.localPosition = new Vector3(0, tableComponent.Items.Count * tableComponent.StackHeight, 0);
                            tableComponent.Items.Push(item);
                            item.GetComponent<Collider>().enabled = true;
                            stackComponent.StackCountText.text = stackComponent.Items.Count.ToString();
                        }
                    }
                }
            }
        }
    }
}