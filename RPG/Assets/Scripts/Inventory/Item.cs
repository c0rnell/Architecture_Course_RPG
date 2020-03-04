using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    public UseAction[] Actions => m_Actions;

    public event Action OnPickedUp;

    public CrosshairDefinition CrosshairDefinition => m_CrosshairDefinition;
    public Sprite Icon => m_Icon;
    public bool WasPickedUp { get; set; }

    [SerializeField] private Sprite m_Icon;
    
    [SerializeField]
    private CrosshairDefinition m_CrosshairDefinition;
    
    [SerializeField] 
    private UseAction[] m_Actions = new UseAction[0];

    private bool m_WasPickedUp;
    private void OnTriggerEnter(Collider other)
    {
        if(WasPickedUp)
            return;

        var inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.Pickup(this);
            OnPickedUp?.Invoke();
        }
    }

    private void OnValidate()
    {
        var mycollider = GetComponent<Collider>();
        if(mycollider.isTrigger == false)
            mycollider.isTrigger = true;
    }
}

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Item item = target as Item;

        DrawIcon(item);
        
        DrawCroshair(item);

        DrawActions(item);

        //base.OnInspectorGUI();
    }

    private void DrawIcon(Item item)
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("Icon", GUILayout.Width(120));

            if (item.Icon != null)
                GUILayout.Box(item.Icon.texture, GUILayout.Width(60), GUILayout.Height(60));
            else
                EditorGUILayout.HelpBox("Icon not set", MessageType.Warning);

            using (var prop = serializedObject.FindProperty("m_Icon"))
            {
                var sprite = (Sprite) EditorGUILayout.ObjectField(item.Icon, typeof(Sprite), false);
                prop.objectReferenceValue = sprite;
                serializedObject.ApplyModifiedProperties();
            }
        }
    }

    private void DrawCroshair(Item item)
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("Croshair", GUILayout.Width(120));

            if (item.CrosshairDefinition?.Sprite != null)
                GUILayout.Box(item.CrosshairDefinition.Sprite.texture, GUILayout.Width(60), GUILayout.Height(60));
            else
                EditorGUILayout.HelpBox("Croshair not set", MessageType.Warning);

            using (var prop = serializedObject.FindProperty("m_CrosshairDefinition"))
            {
                var definition =
                    (CrosshairDefinition) EditorGUILayout.ObjectField(item.CrosshairDefinition, typeof(CrosshairDefinition),
                        false);
                prop.objectReferenceValue = definition;
                serializedObject.ApplyModifiedProperties();
            }
        }
    }

    private void DrawActions(Item item)
    {
        using (var actionsProperty = serializedObject.FindProperty("m_Actions"))
        {
            for (int i = 0; i < actionsProperty.arraySize; i++)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("x", GUILayout.Width(20)))
                    {
                        actionsProperty.DeleteArrayElementAtIndex(i);
                        serializedObject.ApplyModifiedProperties();
                        break;
                    }

                    var action = actionsProperty.GetArrayElementAtIndex(i);
                    if (action != null)
                    {
                        var usemodeProperty = action.FindPropertyRelative("UseMode");
                        var targetComponentProperty = action.FindPropertyRelative("TargetComponent");

                        usemodeProperty.enumValueIndex = (int) (UseMode) EditorGUILayout.EnumPopup(
                            (UseMode) usemodeProperty.enumValueIndex, GUILayout.Width(80));

                        EditorGUILayout.PropertyField(targetComponentProperty, GUIContent.none, false);

                        serializedObject.ApplyModifiedProperties();
                    }
                }
            }

            if (GUILayout.Button("Auto Assign Actions"))
            {
                List<ItemComponent> assignedItems = new List<ItemComponent>();

                for (int i = 0; i < actionsProperty.arraySize; i++)
                {
                    var action = actionsProperty.GetArrayElementAtIndex(i);
                    if (action != null)
                    {
                        var targetComponentProperty = action.FindPropertyRelative("TargetComponent");
                        var assignedItemComponent = targetComponentProperty.objectReferenceValue as ItemComponent;
                        assignedItems.Add(assignedItemComponent);
                    }
                }

                foreach (var itemComponent in item.GetComponentsInChildren<ItemComponent>())
                {
                    if (assignedItems.Contains(itemComponent))
                        continue;
                    
                    actionsProperty.InsertArrayElementAtIndex(actionsProperty.arraySize);
                    var action = actionsProperty.GetArrayElementAtIndex(actionsProperty.arraySize - 1);
                    var targetComponentProperty = action.FindPropertyRelative("TargetComponent");
                    targetComponentProperty.objectReferenceValue = itemComponent;
                    serializedObject.ApplyModifiedProperties();
                }
            }
        }
    }
}