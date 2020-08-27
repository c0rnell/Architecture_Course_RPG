using System;
using UnityEngine;

public class UISelectionCursor : MonoBehaviour
{
    private UIInventoryPanel m_InventoryPanel;
    public bool IconVisible { get; set; }

    private void Awake() => m_InventoryPanel = FindObjectOfType<UIInventoryPanel>();

    private void OnEnable() => m_InventoryPanel.OnSelectionChanged += HandleSelectionChange;
    
    private void OnDisable() => m_InventoryPanel.OnSelectionChanged -= HandleSelectionChange;

    private void HandleSelectionChange() => IconVisible = m_InventoryPanel.Selected.IsEmpty == false;

}