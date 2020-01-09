using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    public UseAction[] Actions => m_Actions;

    public CrosshairDefinition CrosshairDefinition => m_CrosshairDefinition;
    public Sprite Icon => m_Icon;

    [SerializeField] private Sprite m_Icon;
    
    [SerializeField]
    private CrosshairDefinition m_CrosshairDefinition;
    
    [SerializeField] 
    private UseAction[] m_Actions = new UseAction[0];

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
        if(mycollider.isTrigger == false)
            mycollider.isTrigger = true;
    }
}