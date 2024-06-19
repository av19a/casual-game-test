using System.Collections.Generic;
using TMPro;
using UnityEngine;

public struct StackComponent
{
    public Transform StackPoint;
    public float StackHeight;
    public Stack<GameObject> Items;
    public bool IsHolding;
    public TMP_Text StackCountText;
}

public struct SelectableComponent { }

public struct ResetComponent { }