using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Golegos;

public class BattleManager : MonoBehaviour {

    public CharacterStatSet Attacker;
    public CharacterStatSet Defender;
    public DiceManager DiceManager;

	// These are currently only here for helping with testing.
	public DiceManager.RollInfo attackerRoll;
	public DiceManager.RollInfo defenderRoll;
	public DiceManager.RollInfo specialRoll;

	// These are all temporary.  The final battles will likely not be showing dice totals as they run.
	public bool updateTotals;
    public Text BattleOutcomeText;
	public Text OffensiveTotalText;
	public Text DefensiveTotalText;

    void Awake () {
        DiceManager.BattleManager = this;
    }

	void Start() {
		Battle ();
	}

    public void Battle () {
        BattleOutcomeText.text = "";

		// These three lines are in to help with testing until the rest of the battle system is in.
		DiceManager.SetAttackerInfo(attackerRoll.regularDice, attackerRoll.specialDice, attackerRoll.playerSide, attackerRoll.throwOffset);
		DiceManager.SetDefenderInfo(defenderRoll.regularDice, defenderRoll.specialDice, defenderRoll.playerSide, defenderRoll.throwOffset);
		DiceManager.SetSpecialInfo(specialRoll.regularDice, specialRoll.playerSide, specialRoll.throwOffset);

		if (OffensiveTotalText != null) {
			OffensiveTotalText.color = Color.yellow;
			OffensiveTotalText.text = "0";
		}
		if (DefensiveTotalText != null) {
			DefensiveTotalText.color = Color.yellow;
			DefensiveTotalText.text = "0";
		}

        DiceManager.Roll ();
    }

	public void UpdateDiceTotals(int attackTotal, int defenseTotal)
	{
		if (!updateTotals)
			return;

		OffensiveTotalText.text = attackTotal.ToString ();
		DefensiveTotalText.text = defenseTotal.ToString ();
	}

    public void EvaluateBattle (int attack, int defense) {
        int diff = attack - defense;
        if (diff > 0) {
            BattleOutcomeText.text = "Attacker wins!";
        } else if (diff == 0) {
            BattleOutcomeText.text = "Draw!";
        } else if (diff < 0) {
            BattleOutcomeText.text = "Defender wins!";
        }

		if (OffensiveTotalText != null) {
			OffensiveTotalText.color = Color.red;
		}
		if (DefensiveTotalText != null) {
			DefensiveTotalText.color = Color.blue;
		}

    }
}
