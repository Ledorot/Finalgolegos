using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

// Used to add a roll button and to block it's usage outside of Play mode.
[CustomEditor (typeof (DiceManager))]
public class DiceManagerEditor : Editor {

	public override void OnInspectorGUI () {
		DiceManager manager = target as DiceManager;

		DrawDefaultInspector ();

		if (GUILayout.Button ("Roll Dice")) {
			if (!Application.isPlaying) {
				Debug.Log ("Roll dice does not work in Editor mode.");
			} else {
				manager.Roll ();
			}
		}
	}
}
