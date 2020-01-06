using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class InventoryUse : MonoBehaviour
{
    private Inventory m_Inventory;

    private void Awake()
    {
        m_Inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        if(m_Inventory.ActiveItem == null || m_Inventory.ActiveItem.Actions == null)
            return;

        foreach (var useAction in m_Inventory.ActiveItem.Actions)
        {
            if(useAction.TargetComponent.CanUse && WasPressed(useAction.UseMode))
                useAction.TargetComponent.Use();
        }
    }

    private bool WasPressed(UseMode useActionUseMode)
    {
        switch (useActionUseMode)
        {
            case UseMode.LeftClick:
                return Input.GetMouseButtonDown(0);
            case UseMode.RightClick:
                return Input.GetMouseButtonDown(1);
            default:
                return false;
        }
    }
}