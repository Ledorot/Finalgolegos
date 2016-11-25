using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor (typeof (DiceManager))]
public class DiceManagerEditor : Editor {

	public override void OnInspectorGUI () {
		DiceManager manager = target as DiceManager;

		DrawDefaultInspector ();

		if (GUILayout.Button ("Roll Dice")) {
			manager.Roll ();
		}
	}
}
