using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (BattleManager))]
public class BattleManagerEditor : Editor {

    public override void OnInspectorGUI () {
        BattleManager manager = target as BattleManager;

        DrawDefaultInspector ();

        if (GUILayout.Button ("Battle")) {
            manager.Battle ();
        }
    }

}
