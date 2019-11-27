using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    private bool m_WasPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if(m_WasPickedUp)
            return;

        var inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.Pickup(this);
            m_WasPickedUp = true;
        }
    }

    private void OnValidate()
    {
        var mycollider = GetComponent<Collider>();
        mycollider.isTrigger = true;
    }
}