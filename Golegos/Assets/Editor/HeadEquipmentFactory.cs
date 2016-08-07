using UnityEngine;
using UnityEditor;
using System.Collections;

public class HeadEquipmentFactory : Editor {
    [MenuItem ("Assets/Create/Golegos/Equipment/Head")]
    public static void CreateStatSet () {
        HeadEquipment he = ScriptableObject.CreateInstance<HeadEquipment> ();
        AssetDatabase.CreateAsset (he, "Assets/Equipment/Head/Head.asset");
        AssetDatabase.SaveAssets ();

        EditorUtility.FocusProjectWindow ();

        Selection.activeObject = he;
    }
}
