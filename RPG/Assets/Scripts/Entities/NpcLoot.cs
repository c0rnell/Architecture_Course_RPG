using System;
using StateMachines;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Inventory))]
    public class NpcLoot : MonoBehaviour
    {
        [SerializeField] private Item[] m_itemPrefabs;
        
        private Inventory m_Inventory;
        private EntityStateMachine m_EntityStateMachine;

        private void Awake()
        {
            m_Inventory = GetComponent<Inventory>();
        }

        private void Start()
        {
            m_EntityStateMachine = GetComponent<EntityStateMachine>();
            m_EntityStateMachine.OnEntityStateChanged += HandleEntityStateChanged;
                
            foreach (Item itemPrefab in m_itemPrefabs)
            {
                var itemInstance = Instantiate(itemPrefab);
                m_Inventory.Pickup(itemInstance);
            }
        }

        private void HandleEntityStateChanged(IState state)
        {
            Debug.Log($"HandleEntityStateChanged({state.GetType()})");
            if (state is Dead)
                DropLoot();
        }

        private void DropLoot()
        {
            foreach (Item item in m_Inventory.Items)
            {
               LootSystem.Drop(item, transform);
            }
            
            m_Inventory.Items.Clear();
        }
    }
}