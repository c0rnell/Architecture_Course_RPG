using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItemHolder : MonoBehaviour
{
    [SerializeField]
    private Transform m_ItemTransform;

    [SerializeField]
    private float _rottationSpeed;

    private Item _item;

    public void TakeItem(Item item)
    {
        _item = item;                
        _item.transform.SetParent(m_ItemTransform);
        _item.transform.localPosition = Vector3.zero;
        _item.transform.localRotation = Quaternion.identity;
        _item.gameObject.SetActive(true);
        _item.OnPickedUp += HandleItemPickup;
    }

    private void HandleItemPickup()
    {
        LootSystem.AddToPool(this);
    }

    void Update()
    {
        float amount = Time.deltaTime * _rottationSpeed;
        m_ItemTransform.Rotate(0, amount, 0);
    }
}
