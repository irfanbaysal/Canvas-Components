using System;
using Core.Scripts.UI;
using UnityEditor;
using UnityEngine;

namespace BasicSetup.Scripts.PropertyAttributes.Editor
{
    [CustomPropertyDrawer(typeof(RangeWithDecimalAttribute))]
    public class RangeWithDecimalDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // First get the attribute since it contains the range for the slider
            RangeWithDecimalAttribute range = attribute as RangeWithDecimalAttribute;

            property.floatValue = (float)Math.Round(property.floatValue, range.Digit);
            
            // Now draw the property as a Slider or an IntSlider based on whether it's a float or integer.
            if (property.propertyType == SerializedPropertyType.Float)
            {
                EditorGUI.Slider(position, property, range.Min, range.Max, label);
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                if (range != null)
                    EditorGUI.IntSlider(position, property, Convert.ToInt32(range.Max), Convert.ToInt32(range.Max),
                        label);
            }
            else
                EditorGUI.LabelField(position, label.text, "Use Range with float or int.");
            
        }
    }
}