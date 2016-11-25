using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Golegos;

public class BattleManager : MonoBehaviour {

    public CharacterStatSet Attacker;
    public CharacterStatSet Defender;
    public DiceManager DiceManager;

    public Text BattleOutcomeText;

    void Awake () {
        DiceManager.BattleManager = this;
        Battle ();
    }

    public void Battle () {
        BattleOutcomeText.text = "";

        DiceManager.OffensiveDiceCount = Attacker.AttackDice;
        DiceManager.DefensiveDiceCount = Defender.DefenseDice;
        DiceManager.Roll ();
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
    }

}
