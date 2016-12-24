using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DiceManager : MonoBehaviour
{
	public static float DieCheckUpdateDelay = 0.1f;

	// The materials we want applied to the different types of dice
	public Material OffensiveMaterial;
	public Material OffensiveSpecialMaterial;
	public Material DefensiveMaterial;
	public Material DefensiveSpecialMaterial;
	public Material SpecialMoveMaterial;

	// The number of each type of dice that should be spawned
	public int OffensiveDiceCount;
	public int OffensiveSpecialDiceCount;
	public int DefensiveDiceCount;
	public int DefensiveSpecialDiceCount;
	public int SpecialDiceCount;

	// The totals
	public int OffensiveTotal;
	public int DefensiveTotal;
	public int SpecialTotal;

	// The actual lists of each type of dice
	List<GameObject> offensiveDice;
	List<GameObject> defensiveDice;
	List<GameObject> specialDice;

	// Tell the script what it should spawn
	public GameObject DiePrefab;

	public Text OffensiveTotalText;
	public Text DefensiveTotalText;

	public BattleManager BattleManager;

	void Awake()
	{
		
	}

	void Start()
	{
	}

	/// <summary>
	/// Helper function to handle spawning in the dice.
	/// </summary>
	/// <param name="diceCount">Dice count.</param>
	/// <param name="dieName">Editor name for each die.</param>
	/// <param name="dieList">The die list to store the new dice in.</param>
	/// <param name="useMaterial">Material for the new dice.</param>
	/// <param name="playerSide">If set to <c>true</c> dice spawn on the player side.</param>
	/// <param name="throwOffset">Offset from -1 to 1 to determine the throw starting from the left, middle or right.</param>
	private void SpawnDice(int diceCount, string dieName, List<GameObject> dieList, Material useMaterial, bool playerSide, float throwOffset)
	{
		// Clamp throwOffset to stay in range.
		throwOffset	= Mathf.Min (Mathf.Max (throwOffset, -1), 1);

		// Set the start position to be used for the rest of the loop.
		Vector3 startPosition = new Vector3(9 * throwOffset, 5, playerSide ? 5 : -5);

		for (int i = 0; i < diceCount; i++) {
			// These positionings are for testing purposes and will need to be modified.
			// NOTE: DICE **CAN** SPAWN INSIDE EACH OTHER WITH THIS CODE.
			GameObject newDie = Instantiate<GameObject> (DiePrefab, startPosition + Random.insideUnitSphere * 2.5f, Random.rotation);

			// Determine the direction vector for the throw from the randomized position.
			// Be aware that the dice are thrown at just above the position of the DiceManager GameObject.
			// This lets the whole table/dice throwing to be position independent.
			Vector3 direction = transform.position + new Vector3 (0, 2.5f, 0) - newDie.transform.localPosition;

			// Rotation axes are a bit strange.  We flip the vector around to make the rotation work correctly.
			Vector3 reorganize = new Vector3 (direction.z, direction.y, -direction.x);

			// Get the rigidbody on this GameObject.
			Rigidbody rb = newDie.GetComponent<Rigidbody> ();

			// Set the velocity directly for the throw.
			// The multiplier should likely be turned into a publicly facing Editor value for ease of adjustment.
			rb.velocity = direction.normalized * 20;

			// Add rotation to the die.
			rb.AddTorque(reorganize.normalized * 250);

			// Fold it nicely under the dice manager in the scene hierarchy
			newDie.transform.parent = transform;

			// Rename the die
			newDie.name = dieName + " " + i;
			// Change its material
			newDie.GetComponent<Die>().SetMaterial(useMaterial);

			// Add it to the correct list.
			dieList.Add (newDie);
		}

	}

	// These functions are for setting up from the BattleManager code.
	public void SetAttackerInfo(int diceCount, int specialDiceCount, bool playerSide, float throwOffset)
	{

	}

	public void SetDefenderInfo(int diceCount, int specialDiceCount, bool playerSide, float throwOffset)
	{

	}

	public void SetSpecialInfo(int diceCount, bool playerSide, float throwOffset)
	{

	}

	public void Roll ()
	{
		Vector3 noMansLand = new Vector3 (-1000, -1000, -1000);
		// Clean things up before we roll the dice
		foreach (Transform child in transform) {
			// These children actually exist for a single frame so to avoid them causing physics collisions for that one frame
			// we are going to move them out into no-man's land, far enough apart to avoid a sudden jump in physics processing for one frame.
			child.position = noMansLand + Random.insideUnitSphere * 100;
			Destroy (child.gameObject);
		}

		OffensiveTotalText.text = "";
		DefensiveTotalText.text = "";

		OffensiveTotal = 0;
		DefensiveTotal = 0;
		SpecialTotal = 0;

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

		if (specialDice == null)
			specialDice = new List<GameObject> ();
		else
			specialDice.Clear ();

		// Spawn dice for each variety.
		SpawnDice (OffensiveDiceCount, "Offensive Die", offensiveDice, OffensiveMaterial, true, .0f);
		SpawnDice (OffensiveSpecialDiceCount, "Offensive Special Die", offensiveDice, OffensiveSpecialMaterial, true, 0.0f);
		SpawnDice (DefensiveDiceCount, "Defensive Die", defensiveDice, DefensiveMaterial, false, -.5f);
		SpawnDice (DefensiveSpecialDiceCount, "Defensive Special Die", defensiveDice, DefensiveSpecialMaterial, false, -.5f);
		SpawnDice (SpecialDiceCount, "Special Die", specialDice, SpecialMoveMaterial, false, 1f);

		// Start trying to read the dice we've created.
		StartCoroutine (CheckForSettledDice ());
	}

	// Helper function to count the dice and avoid having three copies of this code in a row.
	// AllSettled argument gets passed back out, possibly set to false.
	// dieTotal is going to get boxed/unboxed but this won't matter much for how little it's called.
	private bool CountDice(List<GameObject> dieList, bool allSettled, ref int dieTotal)
	{
		// Clear the total.
		dieTotal = 0;

		// Sanity check that we have a list.
		if (dieList != null) {
			foreach (GameObject go in dieList) {
				Die dieCache = go.GetComponent<Die> ();
				dieTotal += dieCache.value;
				if (!dieCache.FullySettled)
					allSettled = false;
			}

		}
		// Return the settled state to be passed on or used to continue the loop.
		return allSettled;
	}

	// Still working on this bit, documentation to come later
	IEnumerator CheckForSettledDice ()
	{

		// Wait for each of the dice to start moving.
		// Bandaid over dice being read as theyre spawned because they have no momentum yet.
		yield return new WaitForSeconds (DieCheckUpdateDelay);

		// Are all the dice done moving? No.
		bool allSettled = false;

		// Just to show the player that calculations are underway
		OffensiveTotalText.color = Color.yellow;
		DefensiveTotalText.color = Color.yellow;

		// so long as not all the dice are settled...
		while (!allSettled) {
			// Say that they are
			allSettled = true;

			// Call helper to count each die total.
			allSettled = CountDice (offensiveDice, allSettled, ref OffensiveTotal);
			allSettled = CountDice (defensiveDice, allSettled, ref DefensiveTotal);
			allSettled = CountDice (specialDice, allSettled, ref SpecialTotal);

			// Set totals to animate while calculating. These aren't necessary
			OffensiveTotalText.text = OffensiveTotal.ToString ();
			DefensiveTotalText.text = DefensiveTotal.ToString ();

			// Do this every so often.
			yield return new WaitForSeconds (DieCheckUpdateDelay);
		}

		// Display proper text colors to indicate final calculations are done
		OffensiveTotalText.color = Color.red;
		DefensiveTotalText.color = Color.blue;

		// Finish up
		this.BattleManager.EvaluateBattle (OffensiveTotal, DefensiveTotal);
	}
}
