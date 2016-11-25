using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DiceManager : MonoBehaviour
{

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

	public BattleManager BattleManager;

	void Start()
	{
	}

	public void Roll ()
	{
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
		if (offensiveDice == null)
			offensiveDice = new List<GameObject> ();
		else
			offensiveDice.Clear ();

		if (defensiveDice == null)
			defensiveDice = new List<GameObject> ();
		else
			defensiveDice.Clear ();

		// Go through each of the offensive dice to apply materials and cache references.
		for (int i = 0; i < OffensiveDiceCount; i++) {
			// These positionings are for testing purposes and will need to be modified.
			// NOTE: DICE **CAN** SPAWN INSIDE EACH OTHER WITH THIS CODE.
			GameObject newDie = Instantiate (DiePrefab, Random.insideUnitSphere * 5 + Vector3.up * 5, Random.rotation) as GameObject;
			// Rename the die
			newDie.name = "Offensive Die " + i;
			// Change its material
			newDie.GetComponent<Die>().SetMaterial(OffensiveMaterial);
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
			newDie.GetComponent<Die>().SetMaterial(DefensiveMaterial);
			// Fold it nicely under the dice manager in the scene hierarchy
			newDie.transform.parent = transform;
			defensiveDice.Add (newDie);
		}

		// Start trying to read the dice we've created.
		StartCoroutine (CheckForSettledDice ());
	}


	// Still working on this bit, documentation to come later
	IEnumerator CheckForSettledDice ()
	{

		// Wait for each of the dice to start moving.
		// Bandaid over dice being read as theyre spawned because they have no momentum yet.
		yield return new WaitForSeconds (0.1f);

		// Are all the dice done moving? No.
		bool allSettled = false;

		// Just to show the player that calculations are underway
		OffensiveTotalText.color = Color.yellow;
		DefensiveTotalText.color = Color.yellow;

		// so long as not all the dice are settled...
		while (!allSettled) {
			// Say that they are
			allSettled = true;

			// Set totals to 0
			OffensiveTotal = 0;
			DefensiveTotal = 0;

			// For each of hte offensive dice...
			foreach (GameObject go in offensiveDice) {
				int dieValue = go.GetComponent<Die> ().value;
				if (dieValue == 0) {
					allSettled = false;
				} else {
					OffensiveTotal += dieValue;
				}
			}

			// Do the same for the defense
			foreach (GameObject go in defensiveDice) {
				int dieValue = go.GetComponent<Die> ().value;
				if (dieValue == 0) {
					allSettled = false;
				} else {
					DefensiveTotal += dieValue;
				}
			}

			// Set totals to animate while calculating. These aren't necessary
			OffensiveTotalText.text = OffensiveTotal.ToString ();
			DefensiveTotalText.text = DefensiveTotal.ToString ();

			// Do this 10 times a second. Could be tweaked.
			// TODO: Remove magic number
			yield return new WaitForSeconds (0.1f);
		}

		// Display proper text colors to indicate final calculations are done
		OffensiveTotalText.color = Color.red;
		DefensiveTotalText.color = Color.blue;

		// Finish up
		// Debug.Log ("All dice read!");

		this.BattleManager.EvaluateBattle (OffensiveTotal, DefensiveTotal);
	}
}
