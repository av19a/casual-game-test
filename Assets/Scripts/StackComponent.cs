using System.Collections.Generic;
using UnityEngine;

public struct StackComponent
{
    public Transform StackPoint;
    public float StackHeight;
    public Stack<GameObject> Items;
    public bool IsHolding;
}

public struct SelectableComponent { }

public struct ResetComponent { }