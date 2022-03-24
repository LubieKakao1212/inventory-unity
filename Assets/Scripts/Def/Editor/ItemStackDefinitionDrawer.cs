using UnityEditor;
using UnityEngine;

namespace Inventory.Definition.Editor
{
    [CustomPropertyDrawer(typeof(ItemStackDefinition))]
    public class ItemStackDefinitionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (var prop = new EditorGUI.PropertyScope(position, label, property))
            {
                SerializedProperty amountProperty = property.FindPropertyRelative("amount");
                SerializedProperty itemProperty = property.FindPropertyRelative("item");
                
                Rect pos = EditorGUI.PrefixLabel(position, label);

                float splitRatio = 0.6f;

                float splitPoint = pos.width * splitRatio;
                float reverseSplitPoint = pos.width * (1f - splitRatio);

                Rect itemPos = new Rect(pos.x, pos.y, splitPoint, pos.height);
                Rect amountPos = new Rect(pos.x + splitPoint, pos.y, reverseSplitPoint, pos.height);

                int amount = EditorGUI.IntField(amountPos, amountProperty.intValue);
                amountProperty.intValue = Mathf.Max(0, amount);

                itemProperty.objectReferenceValue = EditorGUI.ObjectField(itemPos, itemProperty.objectReferenceValue, typeof(ItemDefinition), false);
            }
        }
    }
}