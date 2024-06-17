using UnityEngine;
using System.Collections.Generic;

public class ItemStack : MonoBehaviour
{
    public Transform stackPoint;  // The point where items are stacked
    public float stackHeight = 0.5f;  // Height of each stacked item

    private Stack<GameObject> items = new Stack<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            StackItem(other.gameObject);
        }
    }

    void StackItem(GameObject item)
    {
        item.transform.SetParent(stackPoint);
        item.transform.localPosition = new Vector3(0, items.Count * stackHeight, 0);
        items.Push(item);

        // Optionally, disable the item's collider to prevent further collisions
        item.GetComponent<Collider>().enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UnstackItem();
        }
    }

    void UnstackItem()
    {
        if (items.Count > 0)
        {
            GameObject item = items.Pop();
            item.transform.SetParent(null);
            // Enable the collider if it was disabled
            item.GetComponent<Collider>().enabled = true;
            item.transform.position = transform.position + transform.forward * 2f;  // Drop item in front of player
            // Implement further logic for dropped item (e.g., physics)
        }
    }
}