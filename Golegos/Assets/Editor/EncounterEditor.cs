using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace Golegos
{
	[CustomEditor (typeof(Encounter))]
	public class EncounterEditor : Editor
	{
		public override void OnInspectorGUI ()
		{
			Encounter enc = target as Encounter;
			DrawDefaultInspector ();

			if (GUILayout.Button ("Open in editor")) {
				EncounterEditorWindow window = EditorWindow.GetWindow (typeof(EncounterEditorWindow)) as EncounterEditorWindow;
				window.SetEncounter (enc);
			}
		}
	}
}