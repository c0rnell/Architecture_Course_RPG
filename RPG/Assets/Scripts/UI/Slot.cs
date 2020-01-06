using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Item m_Item;
    [SerializeField]
    private Image m_Icon;
    public bool IsEmpty => m_Item == null;

    public void SetItem(Item item)
    {
        m_Item = item;
        m_Icon.sprite = item.Icon;
    }
}