using UnityEngine;
using UnityEditor;

namespace Golegos
{
	[CustomPropertyDrawer (typeof(Effect))]
	public class EffectDrawer : PropertyDrawer
	{
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			Rect contentPosition = EditorGUI.PrefixLabel (position, label);
			contentPosition.width *= .25f;
			EditorGUI.PropertyField (contentPosition, property.FindPropertyRelative ("magnitude"), GUIContent.none);
			contentPosition.x += contentPosition.width;
			contentPosition.width *= 3f;
			EditorGUI.PropertyField (contentPosition, property.FindPropertyRelative ("type"), GUIContent.none);
		}
	}
}