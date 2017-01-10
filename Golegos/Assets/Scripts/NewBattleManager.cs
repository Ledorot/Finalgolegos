using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Golegos.Enums;

namespace Golegos {
    public class NewBattleManager : MonoBehaviour {

        //List of allies in the battle
        public List<CharacterStatSet> allies;
        //List of enemies in the battle
        public List<CharacterStatSet> defenders;
        public DiceManager DiceManager;

        //Index of the currently selected player
        private int playerAttackerIndex = 0;

        // These are currently only here for helping with testing.
        public DiceManager.RollInfo attackerRoll;
        public DiceManager.RollInfo defenderRoll;
        public DiceManager.RollInfo specialRoll;

        // These are all temporary.  The final battles will likely not be showing dice totals as they run.
        public bool updateTotals;
        public Text BattleOutcomeText;
        public Text OffensiveTotalText;
        public Text DefensiveTotalText;

        //A reference to the baseOption
        public BattleOption baseOption;
        //The currently selected option
        private BattleOption currentOption;

        void Awake() {
            currentOption = baseOption;
            if (currentOption == null) {
                Debug.LogError("No BaseOption selected");
            }
        }

        void Start() {
            //Battle();
            currentOption.FirstOption();
        }

        public void Battle() {
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

            DiceManager.Roll();
        }

        public void UpdateDiceTotals(int attackTotal, int defenseTotal) {
            if (!updateTotals)
                return;

            OffensiveTotalText.text = attackTotal.ToString();
            DefensiveTotalText.text = defenseTotal.ToString();
        }

        public void EvaluateBattle(int attack, int defense) {
            int diff = attack - defense;
            if (diff > 0) {
                BattleOutcomeText.text = "Attacker wins!";
            }
            else if (diff == 0) {
                BattleOutcomeText.text = "Draw!";
            }
            else if (diff < 0) {
                BattleOutcomeText.text = "Defender wins!";
            }

            if (OffensiveTotalText != null) {
                OffensiveTotalText.color = Color.red;
            }
            if (DefensiveTotalText != null) {
                DefensiveTotalText.color = Color.blue;
            }

        }

        //Called when the player presses the selection button
        public void Select() {
            currentOption = currentOption.Select();
        }

        //Called when the player presses the back button
        public void Back() {
            BattleOption temp = currentOption.Back();
            if (temp != null) {
                currentOption = temp;
            }
        }

        //Called when the player navigates the battle UI
        public void RightSelection() {
            currentOption.RightNavigate();
        }

        //Called when the player navigates the battle UI
        public void LeftSelection() {
            currentOption.LeftNavigate();
        }

        //Called when the player navigates the battle UI
        public void UpSelection() {
            currentOption.UpNavigate();
        }

        //Called when the player navigates the battle UI
        public void DownSelection() {
            currentOption.DownNavigate();
        }

        public void IncrementCharacterIndex() {
            playerAttackerIndex++;
            if (playerAttackerIndex >= allies.Count) {
                playerAttackerIndex = 0;
            }
        }

        public void DecrementCharacterIndex() {
            playerAttackerIndex--;
            if (playerAttackerIndex < 0) {
                playerAttackerIndex = allies.Count - 1;
            }
        }

        public void PlayerAttack(int attackerIndex, int defenderIndex) {

        }

        public void EnemyAttack(int attackerIndex, int defenderIndex) {

        }
    }
}