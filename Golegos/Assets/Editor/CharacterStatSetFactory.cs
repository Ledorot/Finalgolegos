using UnityEngine;
using UnityEditor;
using System.Collections;

public class CharacterStatSetFactory {
	[MenuItem ("Assets/Create/Character Stat Set")]
	public static void CreateStatSet () {
		CharacterStatSet statSet = ScriptableObject.CreateInstance<CharacterStatSet> ();
		AssetDatabase.CreateAsset (statSet, "Assets/Data/CharacterStatSets/Character Stat Set.asset");
		AssetDatabase.SaveAssets ();

		EditorUtility.FocusProjectWindow ();

		Selection.activeObject = statSet;
	}
}
