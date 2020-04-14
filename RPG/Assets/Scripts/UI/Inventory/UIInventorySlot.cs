using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
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
}