using UnityEngine;
using UnityEditor;
using System.Collections;

// Used to add a Battle button and block it from being used when not in Play mode.
[CustomEditor (typeof (BattleManager))]
public class BattleManagerEditor : Editor {

    public override void OnInspectorGUI () {
        BattleManager manager = target as BattleManager;

        DrawDefaultInspector ();

        if (GUILayout.Button ("Battle")) {
			if (!Application.isPlaying) {
				Debug.Log ("Battle does not work in Editor mode.");
			} else {
				manager.Battle ();
			}
        }
    }

}
