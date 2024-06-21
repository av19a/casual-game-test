using System.Collections.Generic;
using UnityEngine;

public struct TableComponent
{
    public Transform StackPoint; // The point where items will be stacked
    public Stack<GameObject> Items; // The items currently on the table
    public float StackHeight; // The height difference between stacked items
}