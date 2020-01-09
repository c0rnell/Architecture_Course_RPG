using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField]
    [FormerlySerializedAs("m_Icon")]
    private Image m_IconImage;

    [SerializeField]
    private TMP_Text m_text;
    public bool IsEmpty => Item == null;
    public Item Item { get; private set; }
    public Image IconImage => m_IconImage;

    public void SetItem(Item item)
    {
        Item = item;
        m_IconImage.sprite = item.Icon;
    }

    private void OnValidate()
    {
        m_text = GetComponentInChildren<TMP_Text>();
        int hotkeyNumber = transform.GetSiblingIndex() + 1;
        m_text.SetText(hotkeyNumber.ToString());
        gameObject.name = "Slot " + hotkeyNumber;
    }
}