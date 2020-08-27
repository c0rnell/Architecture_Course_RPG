using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IPointerClickHandler
{
    public event Action<UIInventorySlot> OnSlotClicked; 
    
    [SerializeField]
    private Image m_Image;
    public bool IsEmpty => Item == null;
    public Sprite Icon => m_Image.sprite;
    public IItem Item { get; private set; }


    public void SetItem(IItem inventoryItem)
    {
        Item = inventoryItem;
        m_Image.sprite = Item != null ? Item.Icon : null;
    }

    public void Clear()
    {
        Item = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSlotClicked?.Invoke(this);
    }
}