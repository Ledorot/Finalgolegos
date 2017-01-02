using UnityEngine;
using UnityEditor;

namespace Golegos
{
	[CustomPropertyDrawer (typeof(Constraint))]
	public class ConstraintDrawer : PropertyDrawer
	{
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			Rect contentPosition = EditorGUI.PrefixLabel (position, label);
			contentPosition.width *= .75f;
			EditorGUI.PropertyField (contentPosition, property.FindPropertyRelative ("type"), GUIContent.none);
			contentPosition.x += contentPosition.width;
			contentPosition.width /= 3f;
			EditorGUI.PropertyField (contentPosition, property.FindPropertyRelative ("value"), GUIContent.none);
		}
	}
}
