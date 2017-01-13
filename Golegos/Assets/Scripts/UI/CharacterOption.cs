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

        //Store the texts of the different attacks
        private string[] characterTexts;


        public override void Awake() {
            base.Awake();
            characterTexts = new string[optionsNum];
        }

        public override void SetChildrenNewEnable(bool newEnable) {
            int i = 0;
            string newText;
            if (newEnable) {
                while (i < battleManager.battle.allies.Count) {
                    newText = battleManager.battle.allies[i++].characterName;
                    derivedOptions[i - 1].gameObject.SetActive(newEnable);
                    characterTexts[i - 1] = newText;
                    if (i <= maxOptions) {
                        derivedOptions[i - 1].GetComponent<Text>().text = characterTexts[i - 1];
                    }
                    else {
                        derivedOptions[i - 1].GetComponent<Text>().text = "";
                    }
                }
            }
            else {
                while (battleManager.battle.allies[i] != null) {
                    derivedOptions[i].gameObject.SetActive(newEnable);
                }
            }
        }

        public override void UpNavigate() {
            if (currentIndex <= 0) {
                currentIndex = battleManager.battle.allies.Count - 1;
            }
            else {
                currentIndex--;
            }
            battleUI.UpdateOptionBox(currentIndex);
            battleManager.DecrementCharacterIndex();
        }

        public override void DownNavigate() {
            if (currentIndex < battleManager.battle.allies.Count - 1) {
                currentIndex++;
            }
            else {
                currentIndex = 0;
            }
            battleUI.UpdateOptionBox(currentIndex);
            battleManager.IncrementCharacterIndex();
        }
    }
}