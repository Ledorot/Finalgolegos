using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (HeadEquipment))]
public class HeadEquipmentEditor : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector ();
    }
}
