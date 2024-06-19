using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.UI;

public class DropSystem : IEcsRunSystem, IEcsInitSystem
{
    private Button _dropButton;
    private bool _dropRequested;
    
    public void Initialize(Button dropButton)
    {
        _dropButton = dropButton;
        _dropButton.onClick.AddListener(OnResetButtonClick);
    }

    public void Init(IEcsSystems systems)
    {
        // Initialization logic
    }
    
    public void Run(IEcsSystems systems)
    {
        if (_dropRequested)
        {
            _dropRequested = false;
            
            var world = systems.GetWorld();
            var filter = world.Filter<StackComponent>().Inc<ResetComponent>().End();
            var stackPool = world.GetPool<StackComponent>();
            
            foreach (var i in filter)
            {
                ref var stackComponent = ref stackPool.Get(i);
                if (stackComponent.Items.Count > 0)
                {
                    GameObject item = stackComponent.Items.Pop();
                    item.transform.position = stackComponent.StackPoint.position + new Vector3(Random.Range(-5, 5), -1, 0);
                    item.transform.SetParent(null);
                    item.GetComponent<Collider>().enabled = true;
                    stackComponent.StackCountText.text = stackComponent.Items.Count.ToString();
                }
                
                if (stackComponent.Items.Count == 0)
                {
                    stackComponent.IsHolding = false;
                }
            }
        }
    }
    
    private void OnResetButtonClick()
    {
        _dropRequested = true;
    }
}
