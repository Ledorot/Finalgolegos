using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Golegos;

namespace Golegos {

    /*
    * Represents the option of choosing which character to strike with
    * while in a battle
    */
    public class CharacterOption : MenuOption {

        //Reference to the characters that this option will show
        private List<CharacterStatSet> characters;
        //Whether or not this class lists enemies
        public bool hasEnemies;

        //Store the texts of the different attacks
        private string[] characterTexts;


        public override void Awake() {
            base.Awake();
            characterTexts = new string[optionsNum];
        }

        public override void Start() {
            base.Start();
            SetCharacterList();
            Invoke("SetCharacterList", .05f);
        }

        public void SetCharacterList() {
            if (!hasEnemies) {
                characters = battleManager.GetAllies();
            }
            else {
                characters = battleManager.GetEnemies();
            }
        }

        public override BattleOption Select() {
            if (!hasEnemies) {
                battleManager.SetAllyIndex(currentIndex);
            }
            else {
                battleManager.SetEnemyIndex(currentIndex);
            }
            return base.Select();
        }

        public override void UpNavigate() {
            if (currentIndex <= 0) {
                if (!hasEnemies) {
                    currentIndex = battleManager.GetAllies().Count - 1;
                }
                else {
                    currentIndex = battleManager.GetEnemies().Count - 1;
                }
            }
            else {
                currentIndex--;
            }
            battleUI.UpdateOptionBox(currentIndex);
        }

        public override void DownNavigate() {
            if (!hasEnemies && currentIndex < battleManager.GetAllies().Count - 1 ||
                    hasEnemies && currentIndex < battleManager.GetEnemies().Count - 1) {
                currentIndex++;
            }
            else {
                currentIndex = 0;
            }
            battleUI.UpdateOptionBox(currentIndex);
        }

        public override void SetChildrenNewEnable(bool newEnable) {
            int i;
            string newText;
            if (newEnable) {
                for (i = 0; i < characters.Count; i++) { 
                    newText = characters[i].characterName;
                    characterTexts[i] = newText;
                    if (i <= maxOptions) {
                        //Set the option's text to each of the character's names
                        derivedOptions[i].GetComponent<Text>().text = characterTexts[i];
                    }
                    else {
                        //If there are more options than characters, hide the rest of the options' text
                        derivedOptions[i].GetComponent<Text>().text = "";
                    }
                }
            }
            else {
                for (i = 0; i < characters.Count; i++) {
                    //Hide the text from all the options
                    derivedOptions[i].GetComponent<Text>().text = "";
                }
            }
        }

        public override void SetChildrenEnableAtIndex(int index) {
            int i;
            for (i = 0; i < characters.Count; i++) {
                if (i == index) {
                    derivedOptions[i].GetComponent<Text>().text = characters[i].characterName;
                }
                else {
                    derivedOptions[i].GetComponent<Text>().text = "";
                }
            }
            currentIndex = 0;
        }
    }
}