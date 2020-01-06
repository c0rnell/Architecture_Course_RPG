using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    [SerializeField]
    private Image m_CrosshairImage;
    
    [SerializeField]
    private Sprite m_InvalidSprite;

    private Inventory m_Inventory;

    private void OnValidate()
    {
        m_CrosshairImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        m_Inventory = FindObjectOfType<Inventory>();

        m_Inventory.ActiveItemChange += HandleActiveItemChanged;

        if(m_Inventory.ActiveItem != null)
            HandleActiveItemChanged(m_Inventory.ActiveItem);
        else
            m_CrosshairImage.sprite = m_InvalidSprite;
    }

    private void HandleActiveItemChanged(Item item)
    {
        if (item != null && item.CrosshairDefinition != null)
            m_CrosshairImage.sprite = item.CrosshairDefinition.Sprite;
        else
            m_CrosshairImage.sprite = m_InvalidSprite;
    }
}