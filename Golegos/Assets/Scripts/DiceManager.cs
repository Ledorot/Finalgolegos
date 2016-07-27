using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DiceManager : MonoBehaviour {

	// The materials we want applied to the different types of dice
	public Material OffensiveMaterial;
	public Material DefensiveMaterial;

	// The number of each type of dice that should be spawned
	public int OffensiveDiceCount;
	public int DefensiveDiceCount;

	public int OffensiveTotal;
	public int DefensiveTotal;

	// The actual lists of each type of dice
	List<GameObject> offensiveDice;
	List<GameObject> defensiveDice;

	// Tell the script what it should spawn
	public GameObject DiePrefab;

	public Text OffensiveTotalText;
	public Text DefensiveTotalText;

	// Perform a roll as soon as the game starts (for testing purposes).
	void Awake () {
		Roll ();
	}
		
	public void Roll () {
		// Clean things up before we roll the dice
		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}

		OffensiveTotalText.text = "";
		DefensiveTotalText.text = "";

		OffensiveTotal = 0;
		DefensiveTotal = 0;

		// Create new arrays to hold each of the types of dice.
		// This accounts for varying number of dice on subsequent rolls
		offensiveDice = new List<GameObject> ();
		defensiveDice = new List<GameObject> ();

		// Go through each of the offensive dice to apply materials and cache references.
		for (int i = 0; i < OffensiveDiceCount; i++) {
			// These positionings are for testing purposes and will need to be modified.
			// NOTE: DICE **CAN** SPAWN INSIDE EACH OTHER WITH THIS CODE.
			GameObject newDie = Instantiate (DiePrefab, Random.insideUnitSphere * 3 + Vector3.up * 5, Random.rotation) as GameObject;
			// Rename the die
			newDie.name = "Offensive Die " + i;
			// Change its material
			newDie.GetComponentInChildren<Renderer> ().material = OffensiveMaterial;
			// Fold it nicely under the dice manager in the scene hierarchy
			newDie.transform.parent = transform;
			offensiveDice.Add (newDie);
		}

		// Go through each of the defensive dice to apply materials and cache references.
		for (int i = 0; i < DefensiveDiceCount; i++) {
			// These positionings are for testing purposes and will need to be modified.
			// NOTE: DICE **CAN** SPAWN INSIDE EACH OTHER WITH THIS CODE.
			GameObject newDie = Instantiate (DiePrefab, Random.insideUnitSphere * 3 + Vector3.up * 5, Random.rotation) as GameObject;
			// Rename the die
			newDie.name = "Defensive Die " + i;
			// Change its material
			newDie.GetComponentInChildren<Renderer> ().material = DefensiveMaterial;
			// Fold it nicely under the dice manager in the scene hierarchy
			newDie.transform.parent = transform;
			defensiveDice.Add (newDie);
		}

		// Start trying to read the dice we've created.
		StartCoroutine (CheckForSettledDice ());
	}


	// Still working on this bit, documentation to come later
	IEnumerator CheckForSettledDice () {
		Debug.Log ("Starting dice read...");
		while (offensiveDice.Count > 0 && defensiveDice.Count > 0) {

			foreach (GameObject go in (new List<GameObject> (offensiveDice))) {
				if (go.GetComponent<Rigidbody> ().IsSleeping ()) {
					OffensiveTotal += go.GetComponent<DieReader> ().Read ();
					OffensiveTotalText.text = OffensiveTotal.ToString ();
					offensiveDice.Remove (go);
					// TESTING - This will cause dice to disappear once being read
					// Destroy (go);
				}
			}

			foreach (GameObject go in (new List<GameObject> (defensiveDice))) {
				if (go.GetComponent<Rigidbody> ().IsSleeping ()) {
					DefensiveTotal += go.GetComponent<DieReader> ().Read ();
					DefensiveTotalText.text = DefensiveTotal.ToString ();
					defensiveDice.Remove (go);
					// TESTING - This will cause dice to disappear once being read
					// Destroy (go);
				}
			}
			yield return new WaitForEndOfFrame ();
		}
		Debug.Log ("All dice read!");
	}
}
