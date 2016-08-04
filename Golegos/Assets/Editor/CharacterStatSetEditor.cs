using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (CharacterStatSet))]
public class CharacterStatSetEditor : Editor {

	public override void OnInspectorGUI () {
		DrawDefaultInspector ();
	}
}
