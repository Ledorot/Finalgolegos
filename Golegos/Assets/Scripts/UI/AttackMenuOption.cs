using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Golegos;

namespace Golegos {

    /*
    * Represents an option whose children are attacks whose text depends on
    * the character's attacks
    */
    [RequireComponent(typeof(Text))]
    public class AttackMenuOption : MenuOption {

        //A reference to the attacksUI (this class will animate it)
        public Transform attacksUI;
        private Animator attacksAnimator;

        //Store the texts of the different attacks
        private string[] attackTexts;

        //Is this class representing normal or special attacks
        public bool isSpecial = false;
        //The current height of the attacks
        private int height = 0;

        public override void Awake() {
            base.Awake();
            if (attacksUI == null) {
                Debug.LogError("No attacksUI found!");
            }
            attacksAnimator = attacksUI.GetComponent<Animator>();
            if (attacksAnimator == null) {
                Debug.LogError("AttacksUI doesn't have an Animator component!");
            }
            attackTexts = new string[optionsNum];
        }

        //Changes the different attack's visibility
        //public void RearrangeChildrenVisibility(int index) {
        //    if (index >= derivedOptions.Length || index < 0) {
        //        return;
        //    }
        //    derivedOptions[index - maxOptions].gameObject.SetActive(false);
        //    if (index - 1 >= derivedOptions.Length) {
        //        derivedOptions[index - 1].gameObject.SetActive(false);
        //    }
        //    derivedOptions[index].gameObject.SetActive(true);
        //    if (index < derivedOptions.Length - 1) {
        //        derivedOptions[index + 1].gameObject.SetActive(false);
        //    }
        //}

        public override BattleOption Select() {
            battleManager.SetPlayerAttack(currentIndex);
            return base.Select();
        }

        //Called when this option is exited from
        public override BattleOption Back() {
            height = 0;
            attacksAnimator.SetInteger("Height", height);
            for (int i = maxOptions - 1; i > optionsNum; i++) {
                derivedOptions[i].GetComponent<Text>().text = "";
            }
            return base.Back();
        }

        public override void UpNavigate() {
            if (currentIndex > 0) {
                currentIndex--;
                if (height > 0) {
                    attacksAnimator.SetInteger("Height", --height);
                    derivedOptions[currentIndex + 1].GetComponent<Text>().text = "";
                    derivedOptions[currentIndex - maxOptions + 1].GetComponent<Text>().text = attackTexts[currentIndex - maxOptions + 1];
                }
                else {
                    battleUI.UpdateOptionBox(currentIndex);
                }
            }
        }

        public override void DownNavigate() {
            if (!isSpecial && currentIndex < battleManager.GetSelectedPlayer().Attacks.Count - 1 ||
                isSpecial && currentIndex < battleManager.GetSelectedPlayer().SpecialAttacks.Count - 1) {

                if (currentIndex < optionsNum - 1) {
                    currentIndex++;
                    if (currentIndex >= maxOptions) {
                        attacksAnimator.SetInteger("Height", ++height);
                        derivedOptions[currentIndex - maxOptions].GetComponent<Text>().text = "";
                        derivedOptions[currentIndex].GetComponent<Text>().text = attackTexts[currentIndex];
                    }
                    else {
                        battleUI.UpdateOptionBox(currentIndex);
                    }
                }
            }
        }

        public override void SetChildrenNewEnable(bool newEnable) {
            int i = 0;
            string newText;
            if (newEnable) {
                while (battleManager.GetSelectedPlayer() != null &&
                    (newText = battleManager.GetSelectedPlayer().GetAttackText(i++, isSpecial)) != null) {

                    attackTexts[i - 1] = newText;
                    if (i <= maxOptions) {
                        //Show the text of the different attacks
                        derivedOptions[i - 1].GetComponent<Text>().text = attackTexts[i - 1];
                    }
                    else {
                        //If there are more options than there are attacks, hide this text
                        derivedOptions[i - 1].GetComponent<Text>().text = "";
                    }
                }
            }
            else {
                while (battleManager.GetSelectedPlayer().GetAttackText(i++, isSpecial) != null) {
                    //Hide the text of all the attacks
                    derivedOptions[i - 1].GetComponent<Text>().text = "";
                }
                currentIndex = 0;
            }
        }
    }
}