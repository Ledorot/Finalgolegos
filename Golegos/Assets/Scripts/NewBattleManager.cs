using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Golegos;

namespace Golegos {
    public class NewBattleManager : MonoBehaviour {

        //The battle to be executed by this manager
        public Battle battle;
        //A reference to the DiceManager
        public DiceManager diceManager;
        //A static reference to this BattleManager
        public static NewBattleManager bm;
        //Reference to the character
        private MapCharacter mapCharacter;

        //Index of the currently selected player
        private int playerIndex = 0;
        //Index of the currently selected attack
        private int attackIndex = 0;
        //Index of the currently selected enemy
        private int enemyIndex = 0;
        //Current list of allies
        private List<CharacterStatSet> alliesList;
        //Current list of enemies
        private List<CharacterStatSet> enemiesList;

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
        public MenuOption baseOption;
        //The currently selected option
        private MenuOption currentOption;

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
            if (battle == null) {
                Debug.LogError("No Battle specified!");
            }
        }

        void Start() {
            //Battle();
            mapCharacter = MapCharacter.mapChar;
            if (mapCharacter == null) {
                Debug.Log("No MapCharacter instance found!");
            }
            NewBattle();
            SpawnCharacters();
            Invoke("SetFirstOption", .1f);

        }

        //Used to give the other options a chance to use Start() properly (without null references)
        public void SetFirstOption() {
            currentOption.FirstOption();
        }

        public void NewBattle() {
            alliesList = mapCharacter.allies;
            enemiesList = battle.enemies;
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
            int max = alliesList.Count;
            //Spawn allies
            for (i = 0; i < max; i++) {
                if (alliesList[i].battleSprite != null) {
                    /*RectTransform sprite = */
                    Instantiate(alliesList[i].battleSprite, allySpawnPoints[i].position, allySpawnPoints[i].rotation, allySpawnPoints[i]);
                    //sprite.parent
                }
            }
            i = 0;
            max = enemiesList.Count;
            //Spawn enemies
            for (i = 0; i < max; i++) {
                if (enemiesList[i].battleSprite != null) {
                    /*RectTransform sprite = */
                    Instantiate(enemiesList[i].battleSprite, enemySpawnPoints[i].position, enemySpawnPoints[i].rotation, enemySpawnPoints[i]);
                    //sprite.parent
                }
            }
        }

        //Called when the player presses the selection button
        public void Select() {
            MenuOption temp = currentOption.Select() as MenuOption;
            if (temp != null) {
                MenuOption menuOp = currentOption.GetParentOption() as MenuOption;
                if (currentOption.GetParentOption() != null && menuOp != null) {
                    menuOp.SetChildrenNewEnable(false);
                }
                currentOption = temp;
            }
        }

        //Called when the player presses the back button
        public void Back() {
            MenuOption temp = currentOption.Back() as MenuOption;
            if (temp != null) {
                currentOption = temp;
                MenuOption menuOp = currentOption.GetParentOption() as MenuOption;
                if (menuOp != null) {
                    Debug.Log("Parent: " + menuOp.name + "\tChild: " + currentOption);
                    Debug.Log(currentOption.GetIndexInParent());
                    menuOp.SetChildrenEnableAtIndex(currentOption.GetIndexInParent());
                }
            }
        }

        //Called when the player navigates the battle UI
        public void RightSelection() {
            Select();
        }

        //Called when the player navigates the battle UI
        public void LeftSelection() {
            Back();
        }

        //Called when the player navigates the battle UI
        public void UpSelection() {
            currentOption.UpNavigate();
        }

        //Called when the player navigates the battle UI
        public void DownSelection() {
            currentOption.DownNavigate();
        }

        public void SetAllyIndex(int index) {
            if (playerIndex < alliesList.Count) {
                playerIndex = index;
            }
            else {
                Debug.LogError("Specified character doesn't exist!");
            }
        }

        public void SetEnemyIndex(int index) {
            if (enemyIndex < enemiesList.Count) {
                enemyIndex = index;
            }
            else {
                Debug.LogError("Specified character doesn't exist!");
            }
        }

        public List<CharacterStatSet> GetAllies() {
            return alliesList;
        }

        public List<CharacterStatSet> GetEnemies() {
            return enemiesList;
        }

        public CharacterStatSet GetSelectedPlayer() {
            if (alliesList.Count > playerIndex) {
                return alliesList[playerIndex];
            }
            return null;
        }

        //Sets the index of the attack to use by the player
        public void SetPlayerAttack(int index) {
            attackIndex = index;
        }

        public void PlayerAttack(int attackerIndex, int defenderIndex) {

        }

        public void EnemyAttack(int attackerIndex, int defenderIndex) {

        }
    }
}