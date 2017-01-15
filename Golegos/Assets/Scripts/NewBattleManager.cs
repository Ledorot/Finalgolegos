using UnityEngine;
using UnityEngine.UI;

namespace Golegos {
    public class NewBattleManager : MonoBehaviour {

        //The battle to be executed by this manager
        public Battle battle;
        //A reference to the DiceManager
        public DiceManager diceManager;
        //A static reference to this BattleManager
        public static NewBattleManager bm;

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

        //Ally spawn points
        public Transform[] allySpawnPoints;
        //Enemy spawn points
        public Transform[] enemySpawnPoints;
        //A reference to the baseOption
        public BattleOption baseOption;
        //The currently selected option
        private BattleOption currentOption;

        void Awake() {
            currentOption = baseOption;
            if (currentOption == null) {
                Debug.LogError("No BaseOption selected");
            }
            if (bm == null) {
                bm = this;
            }
            if (allySpawnPoints.Length == 0) {
                Debug.LogError("No ally spawn points specified!");
            }
            if (enemySpawnPoints.Length == 0) {
                Debug.LogError("No enemy spawn points specified!");
            }
        }

        void Start() {
            //Battle();
            currentOption.FirstOption();
            SpawnCharacters();

        }

        public void Battle() {
            BattleOutcomeText.text = "";

            // These three lines are in to help with testing until the rest of the battle system is in.
            diceManager.SetAttackerInfo(attackerRoll.regularDice, attackerRoll.specialDice, attackerRoll.playerSide, attackerRoll.throwOffset);
            diceManager.SetDefenderInfo(defenderRoll.regularDice, defenderRoll.specialDice, defenderRoll.playerSide, defenderRoll.throwOffset);
            diceManager.SetSpecialInfo(specialRoll.regularDice, specialRoll.playerSide, specialRoll.throwOffset);

            if (OffensiveTotalText != null) {
                OffensiveTotalText.color = Color.yellow;
                OffensiveTotalText.text = "0";
            }
            if (DefensiveTotalText != null) {
                DefensiveTotalText.color = Color.yellow;
                DefensiveTotalText.text = "0";
            }

            diceManager.Roll();
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

        private void SpawnCharacters() {
            int i = 0;
            if (battle.allies.Count > 0) {
                while (battle.allies[i] != null && battle.allies[i].battleSprite != null) {
                    /*RectTransform sprite = */
                    Instantiate(battle.allies[i].battleSprite, allySpawnPoints[i++].position, allySpawnPoints[i].rotation, allySpawnPoints[i]);
                    //sprite.parent
                }
            }
            i = 0;
            if (battle.enemies.Count > 0) {
                while (battle.enemies[i] != null && battle.enemies[i].battleSprite != null) {
                    Instantiate(battle.enemies[i].battleSprite, enemySpawnPoints[i++]);
                }
            }
        }

        //Called when the player presses the selection button
        public void Select() {
            BattleOption temp = currentOption.Select();
            if (temp != null) {
                currentOption = temp;
            }
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
            BattleOption temp = currentOption.RightNavigate();
            if (temp != null) {
                currentOption = temp;
            }
        }

        //Called when the player navigates the battle UI
        public void LeftSelection() {
            BattleOption temp = currentOption.LeftNavigate();
            if (temp != null) {
                currentOption = temp;
            }
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
            if (playerAttackerIndex >= battle.allies.Count) {
                playerAttackerIndex = 0;
            }
        }

        public void DecrementCharacterIndex() {
            playerAttackerIndex--;
            if (playerAttackerIndex < 0) {
                playerAttackerIndex = battle.allies.Count - 1;
            }
        }

        public CharacterStatSet GetSelectedPlayer() {
            if (battle.allies.Count > playerAttackerIndex) {
                return battle.allies[playerAttackerIndex];
            }
            return null;
        }

        public void PlayerAttack(int attackerIndex, int defenderIndex) {

        }

        public void EnemyAttack(int attackerIndex, int defenderIndex) {

        }
    }
}