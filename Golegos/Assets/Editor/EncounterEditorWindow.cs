using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Golegos;
using Golegos.Enums;

public class EncounterEditorWindow : EditorWindow
{
	private Encounter enc;
	private List<bool> characterFadeGroups;
	private Vector2 scrollPosition;

	private GUIContent contentObject;

	private GUIStyle deleteCharacterButtonStyle;

	private bool reinitialize;
	private EquipmentValidation invalidEQ;

	[MenuItem ("Window/Golegos/Encounter Editor %#e")]
	static void  Init ()
	{
		EditorWindow.GetWindow (typeof(EncounterEditorWindow));
	}

	protected void Awake()
	{
		contentObject = new GUIContent ();
		characterFadeGroups = new List<bool> ();
		characterFadeGroups.Add (false);
		contentObject = new GUIContent ();

		reinitialize = true;

		invalidEQ = new EquipmentValidation ();

	}

	void OnEnable ()
	{
		Debug.Log ("OnEnable");
		titleContent.text = "Encounter Editor";
		if (enc != null)
			SetEncounter (enc);
		scrollPosition = Vector2.zero;

		reinitialize = true;
	}

	protected void OnGUI ()
	{
		// Styles apparently can't be copied or otherwise set up outside of OnGUI so these have to be done here.
		if (reinitialize) {
			// Set up the style reused for the character deletion button.
			deleteCharacterButtonStyle = new GUIStyle (GUI.skin.button);
			deleteCharacterButtonStyle.fixedWidth = 20;
			deleteCharacterButtonStyle.alignment = TextAnchor.MiddleRight;
			reinitialize = false;
		}

		scrollPosition = EditorGUILayout.BeginScrollView (scrollPosition);

		EditorGUILayout.BeginHorizontal ();
		EditorGUI.BeginChangeCheck ();
		enc = EditorGUILayout.ObjectField ("Obj: ", enc, typeof(Encounter), false) as Encounter;
		if (EditorGUI.EndChangeCheck ()) {
			SetEncounter (enc);
		}
		EditorGUILayout.EndHorizontal ();

		// If we have an encounter let's draw it all out.
		if (enc != null) {
			enc.coinReward = EditorGUILayout.IntField ("Coins", enc.coinReward);
			enc.itemReward = EditorGUILayout.ObjectField ("Item", enc.itemReward, typeof(EquipmentItem), false) as EquipmentItem;

			int loopCounter = 0;
			bool endLoop = false;

			// Check to make sure we have our list setup and that we have at least one enemy object in the list.
			// Ideally I'd rather this be in the constructor of Encounter but I feel if I do that it will just cause
			// the Character object to be thrown out when there is actually a valid asset loaded in.
			if (enc.enemies == null) {
				enc.enemies = new List<Character> ();
			}
			if (enc.enemies.Count == 0)
				enc.enemies.Add (new Character ());

			while (endLoop == false) {
				Character ch = enc.enemies [loopCounter];

				string name = (ch.stats == null) ? "Empty Character" : ch.stats.name;
				EditorGUILayout.BeginHorizontal ();
				EditorGUI.BeginChangeCheck ();
				characterFadeGroups [loopCounter] = EditorGUILayout.Foldout (characterFadeGroups [loopCounter], name);
				if (EditorGUI.EndChangeCheck ()) {
					//characterFadeGroups[loopCounter] = characterFoldouts[loopCounter] ? 1 : 0;
					AnimatorTransitionInfo info;
				}
				GUILayout.Button ("X", deleteCharacterButtonStyle);
				EditorGUILayout.EndHorizontal ();


				if (EditorGUILayout.BeginFadeGroup (characterFadeGroups [loopCounter] ? 1 : 0)) {
					EditorGUI.indentLevel++;

					EditorGUI.BeginChangeCheck ();
					ch.stats = EditorGUILayout.ObjectField ("Stats", ch.stats, typeof(CharacterStatSet), false) as CharacterStatSet;
					if (EditorGUI.EndChangeCheck ())
					{
						if (ch.stats != null) {
							ch.gear.CopyFrom (ch.stats.defaultEquipment);
						} else {
							ch.gear.Clear ();
						}
					}

					if (ch.stats != null) {

						for (int i = 0; i < 7; i++) {
							EquipmentSlot slot;
							string label;
							switch (i) {
							case 0:
								slot = ch.gear.hand;
								label = "Hand";
								break;
							case 1:
								slot = ch.gear.torso;
								label = "Torso";
								break;
							case 2:
								slot = ch.gear.neck;
								label = "Neck";
								break;
							case 3:
								slot = ch.gear.head;
								label = "Head";
								break;
							case 4:
								slot = ch.gear.back;
								label = "Back";
								break;
							case 5:
								slot = ch.gear.legs;
								label = "Legs";
								break;
							case 6:
								slot = ch.gear.offHand;
								label = "OffHand";
								break;
							default:
								slot = ch.gear.offHand;
								label = "Head";
								break;
							}

							slot.item = EditorGUILayout.ObjectField (label, slot.item, typeof(EquipmentItem), false)as EquipmentItem;
						}
					}
					EditorGUI.indentLevel--;
				}
				EditorGUILayout.EndFadeGroup ();
				loopCounter++;
				if (loopCounter >= enc.enemies.Count)
					endLoop = true;
			}
		}
		EditorGUILayout.EndScrollView ();
	}

	public void SetEncounter (Encounter newEncounter)
	{
		enc = newEncounter;

		if (enc != null) {
			// Get the current enemy count.  A fresh encounter has no list yet so check for it.
			int enemyCount = 0;
			if (enc.enemies == null) {
				enc.enemies = new List<Character> ();
			}
			enemyCount = enc.enemies.Count;

			int fadesToAdd = enemyCount - characterFadeGroups.Count;

			for (int i = 0; i < fadesToAdd; i++)
				characterFadeGroups.Add (false);
		}
	}

	public void ValidateEncounter()
	{

	}

	public void ValidateCharacter(Equipment defaultEquipment, Equipment currentEquipment)
	{
		if (defaultEquipment == null || currentEquipment == null)
			return;

		invalidEQ.Reset ();

	}

	public class EquipmentValidation
	{
		public bool anyInvalid;
		public bool[] invalidSlot;
		public bool[] changedGear;

		public EquipmentValidation()
		{
			anyInvalid = false;
			int gearSize = (int)EquipmentType.Count;
			invalidSlot = new bool[gearSize];
			changedGear = new bool[gearSize];
		}

		public void Reset()
		{
			for (int i = 0; i < invalidSlot.Length; i++) {
				invalidSlot [i] = false;
				changedGear [i] = false;
			}
		}
	}
}