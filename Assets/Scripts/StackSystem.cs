using UnityEngine;
using Leopotam.EcsLite;

public class StackSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var filter = world.Filter<StackComponent>().Inc<MovementComponent>().End();
        var stackPool = world.GetPool<StackComponent>();

        foreach (var i in filter)
        {
            ref var stackComponent = ref stackPool.Get(i);
            Collider[] hitColliders = Physics.OverlapSphere(stackComponent.StackPoint.position, 0.5f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Item"))
                {
                    GameObject item = hitCollider.gameObject;
                    item.transform.SetParent(stackComponent.StackPoint);
                    item.transform.localPosition = new Vector3(0, stackComponent.Items.Count * stackComponent.StackHeight, 0);
                    stackComponent.Items.Push(item);
                    item.GetComponent<Collider>().enabled = false;
                }
            }
        }
    }
}