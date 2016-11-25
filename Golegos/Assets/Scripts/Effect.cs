using UnityEngine;
using System.Collections.Generic;
using Golegos.Enums;
using System;
using UnityEditor;

namespace Golegos
{
	[Serializable]
	public class Effect
	{
		public EffectType type;
		public Int32 magnitude;

		public Effect(EffectType type, Int32 magnitude)
		{
			this.magnitude = magnitude;
			this.type = type;
		}
	}

	#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(Effect))]
	public class EffectDrawer : PropertyDrawer
	{
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
			Rect contentPosition = EditorGUI.PrefixLabel(position, label);
			contentPosition.width *= .25f;
			EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("magnitude"), GUIContent.none);
			contentPosition.x += contentPosition.width;
			contentPosition.width *= 3f;
			EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("type"), GUIContent.none);
		}
	}
	#endif
}
