using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Golegos.Enums;

namespace Golegos {
    public class NewBattleManager : MonoBehaviour {

        //public reference to the battle manager
        public static NewBattleManager bm;
        //List of allies in the battle
        public List<CharacterStatSet> allies;
        //List of enemies in the battle
        public List<CharacterStatSet> defenders;
        public DiceManager DiceManager;
        //Index of the currently selected player
        public int playerAttackerIndex = 0;
        private BattleAction battleAction = BattleAction.Attack;
        public BattleUI battleUI;

        // These are currently only here for helping with testing.
        public DiceManager.RollInfo attackerRoll;
        public DiceManager.RollInfo defenderRoll;
        public DiceManager.RollInfo specialRoll;

        // These are all temporary.  The final battles will likely not be showing dice totals as they run.
        public bool updateTotals;
        public Text BattleOutcomeText;
        public Text OffensiveTotalText;
        public Text DefensiveTotalText;

        void Awake() {
            if (bm == null) {
                bm = this;
            }
            //DiceManager.BattleManager = this;
            if (battleUI == null) {
                Debug.LogError("No BattleUI script selected in BattleManager!");
            }
        }

        void Start() {
            //Battle();
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

            switch (battleAction) {
                case BattleAction.Selecting:
                    battleAction = BattleAction.Attack;
                    break;

                case BattleAction.Attack:
                    battleAction = BattleAction.NormalAttack;
                    break;

                case BattleAction.NormalAttack:
                    //Display player attacks
                    break;

                case BattleAction.SpecialAttack:
                    //Display player special attacks
                    break;

                case BattleAction.Stats:
                    //Display player stats
                    break;

                case BattleAction.QuickSave:
                    //Quicksave the game
                    break;

                case BattleAction.Opponent:
                    //Display opponent stats
                    break;
            }
        }

        //Called when the player presses the back button
        public void Back() {

            switch (battleAction) {

                case BattleAction.Attack:
                    battleAction = BattleAction.Selecting;
                    break;

                case BattleAction.NormalAttack:
                    battleAction = BattleAction.Attack;
                    break;

                case BattleAction.SpecialAttack:
                    battleAction = BattleAction.Attack;
                    break;

                case BattleAction.Stats:
                    battleAction = BattleAction.Selecting;
                    break;

                case BattleAction.QuickSave:
                    battleAction = BattleAction.Selecting;
                    break;

                case BattleAction.Opponent:
                    battleAction = BattleAction.Selecting;
                    break;
            }
        }

        //Called when the player navigates the menu
        public void RightSelection() {

            //There is only horizontal navigation if the player is selecting a character
            if (battleAction == BattleAction.Selecting) {
                playerAttackerIndex++;
                if (playerAttackerIndex == allies.Count) {
                    playerAttackerIndex = 0;
                }
                UpdateSelectedCharacter();
            }
        }

        //Called when the player navigates the menu
        public void LeftSelection() {

            if (battleAction == BattleAction.Selecting) {
                playerAttackerIndex--;
                if (playerAttackerIndex < 0) {
                    playerAttackerIndex = allies.Count - 1;
                }
                UpdateSelectedCharacter();
            }
        }

        //Called when the player navigates the menu
        public void UpSelection() {

            //There is only horizontal navigation if the player isn't selecting a character
            if (battleAction != BattleAction.Selecting) {
                switch (battleAction) {

                    case BattleAction.Attack:
                        battleAction = BattleAction.Opponent;
                        break;

                    case BattleAction.NormalAttack:
                        battleAction = BattleAction.SpecialAttack;
                        break;

                    case BattleAction.SpecialAttack:
                        battleAction = BattleAction.NormalAttack;
                        break;

                    case BattleAction.Stats:
                        battleAction = BattleAction.Attack;
                        break;

                    case BattleAction.QuickSave:
                        battleAction = BattleAction.Stats;
                        break;

                    case BattleAction.Opponent:
                        battleAction = BattleAction.QuickSave;
                        break;
                }
                UpdateMenuOption();
            }
        }

        //Called when the player navigates the menu
        public void DownSelection() {

            //If the player is selecting a character, there is no vertical navigation
            if (battleAction != BattleAction.Selecting) {
                switch (battleAction) {

                    case BattleAction.Attack:
                        battleAction = BattleAction.Stats;
                        break;

                    case BattleAction.NormalAttack:
                        battleAction = BattleAction.SpecialAttack;
                        break;

                    case BattleAction.SpecialAttack:
                        battleAction = BattleAction.NormalAttack;
                        break;

                    case BattleAction.Stats:
                        battleAction = BattleAction.QuickSave;
                        break;

                    case BattleAction.QuickSave:
                        battleAction = BattleAction.Opponent;
                        break;

                    case BattleAction.Opponent:
                        battleAction = BattleAction.Attack;
                        break;
                }
                UpdateMenuOption();
            }
        }

        private void UpdateMenuOption() {

            switch (battleAction) {

                case BattleAction.Attack:
                    battleUI.NavigateOption(0);
                    break;

                case BattleAction.NormalAttack:
                    battleUI.NavigateOption(0);
                    break;

                case BattleAction.SpecialAttack:
                    battleUI.NavigateOption(1);
                    break;

                case BattleAction.Stats:
                    battleUI.NavigateOption(1);
                    break;

                case BattleAction.QuickSave:
                    battleUI.NavigateOption(2);
                    break;

                case BattleAction.Opponent:
                    battleUI.NavigateOption(3);
                    break;
            }
        }

        private void UpdateSelectedCharacter() {
            battleUI.NavigateCharacter(playerAttackerIndex);
        }

        public void PlayerAttack(int attackerIndex, int defenderIndex) {

        }

        public void EnemyAttack(int attackerIndex, int defenderIndex) {

        }
    }
}