using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Die))]
public class DieEditor : Editor {

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();

		if (GUILayout.Button ("Read Now")) {
			(target as Die).ReadNow ();
		}
	}
}
