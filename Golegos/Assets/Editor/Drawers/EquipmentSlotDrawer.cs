using UnityEngine;
using UnityEditor;

namespace Golegos
{
	// Custom drawer so we can shrink the size and completely block the "type" property from showing up.
	[CustomPropertyDrawer (typeof(EquipmentSlot))]
	public class EquipmentSlotDrawer : PropertyDrawer
	{
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			// Draw the label for the EquipmentSlot.
			Rect contentPosition = EditorGUI.PrefixLabel (position, label);

			// Just more random Unity BS to make things appear properly.
			// Trying to use the label property on PropertyField causes it to indent and shove the CheckBox behind the ObjectField.
			// Leaving indentLevel causes things to be even more indented then they already are.
			label.text = "On";
			EditorGUI.indentLevel = 0;
			EditorGUI.LabelField(contentPosition, "On");

			// Adjust for the LabelField we just put down.
			contentPosition.x += 20;
			contentPosition.width -= 20;

			// Split the remaining part into 1/5th Toggle and 4/5th ObjectField.
			contentPosition.width *= .2f;
			EditorGUI.PropertyField (contentPosition, property.FindPropertyRelative ("enabled"), GUIContent.none);
			contentPosition.x += contentPosition.width;
			contentPosition.width *= 4;
			EditorGUI.PropertyField (contentPosition, property.FindPropertyRelative ("item"), GUIContent.none);
		}
	}
}
