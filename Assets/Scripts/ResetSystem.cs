using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.UI;

public class ResetSystem : IEcsRunSystem, IEcsInitSystem
{
    private Button _resetButton;
    private bool _resetRequested;
    
    public void Initialize(Button resetButton)
    {
        _resetButton = resetButton;
        _resetButton.onClick.AddListener(OnResetButtonClick);
    }

    public void Init(IEcsSystems systems)
    {
        // Initialization logic
    }
    
    public void Run(IEcsSystems systems)
    {
        if (_resetRequested)
        {
            _resetRequested = false;
            
            var world = systems.GetWorld();
            var filter = world.Filter<StackComponent>().Inc<ResetComponent>().End();
            var stackPool = world.GetPool<StackComponent>();
            
            foreach (var i in filter)
            {
                ref var stackComponent = ref stackPool.Get(i);
                while (stackComponent.Items.Count > 0)
                {
                    GameObject item = stackComponent.Items.Pop();
                    stackComponent.IsHolding = false;
                    item.transform.position = stackComponent.StackPoint.position + new Vector3(Random.Range(-5, 5), -1, 0);
                    item.transform.SetParent(null);
                    item.GetComponent<Collider>().enabled = true;
                    stackComponent.StackCountText.text = stackComponent.Items.Count.ToString();
                }
            }
        }
    }
    
    private void OnResetButtonClick()
    {
        _resetRequested = true;
    }
}