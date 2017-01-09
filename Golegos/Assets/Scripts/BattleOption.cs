using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Golegos;

namespace Golegos {

    [RequireComponent(typeof(Text))]
    public class BattleOption : MonoBehaviour {

        //List of battle options that derive from this option
        public BattleOption[] derivedOptions;
        private BattleUI battleUI;
        private BattleOption parentOption = null;

        //Indicates whether or not the menu should animate to the left when this option is selected
        public bool increasesDepth = true;
        //Indicates whether this option can do something if selected
        public bool selectable = true;

        //The amount of options available after selecting this option
        private int optionsNum;
        //Keeps track of the current option index
        private int currentIndex = 0;
        //The amount of time until this option hides its derived options
        public float hideTime = .2f;

        void Awake() {
            //Check if the number of derived options is 0
            optionsNum = derivedOptions.Length;
            if (optionsNum == 0 && increasesDepth) {
                Debug.LogError("Can't increase depth without sub-options!");
            }
        }

        void Start() {
            battleUI = BattleUI.BUI;
            if (battleUI == null) {
                Debug.LogError("No BattleUI reference found");
            }
            //Set this option as the parent of all the derived options
            foreach (BattleOption battleOp in derivedOptions) {
                if (battleOp != null) {
                    battleOp.SetParentOption(this);
                }
            }
        }

        //Called when this option is made available (first indicates whether or not
        //this option was the first one to be called, so that it can reset the optionBox's position)
        public void Available(bool first) {
            if (first) {
                Invoke("FirstUpdate", .005f);
            }
        }

        public void FirstUpdate() {
            battleUI.UpdateOptionBox(0);
        }

        //Called when this option gets returned to (so that this option can hide its derived options)
        public void SetToHideChildren() {
            SetChildrenNewEnable(false);
        }

        public void SetChildrenNewEnable(bool newEnable) {
            foreach (BattleOption battleOp in derivedOptions) {
                if (battleOp != null) {
                    battleOp.gameObject.SetActive(newEnable);
                    //If the children are now activated, call the available function
                    if (newEnable) {
                        battleOp.Available(false);
                    }
                }
            }
        }

        public void UpNavigate() {
            if (currentIndex <= 0) {
                currentIndex = optionsNum - 1;
            }
            else {
                currentIndex--;
            }
            battleUI.UpdateOptionBox(currentIndex);
        }

        public void DownNavigate() {
            if (currentIndex < optionsNum - 1) {
                currentIndex++;
            }
            else {
                currentIndex = 0;
            }
            battleUI.UpdateOptionBox(currentIndex);
        }

        //Called when this option is selected
        public BattleOption Select() {
            if (increasesDepth) {
                battleUI.IncreaseDepth();
            }
            battleUI.UpdateOptionBox(0);
            derivedOptions[currentIndex].SetChildrenNewEnable(true);
            return derivedOptions[currentIndex];
        }

        //Called when this option is exited from
        public BattleOption Back() {
            if (parentOption != null) {
                battleUI.DecreaseDepth();
                SetToHideChildren();
                //Invoke("SetToHideChildren", hideTime);

                int newIndex = GetIndexInParent();
                if (newIndex >= 0) {
                    battleUI.UpdateOptionBox(newIndex);
                }
                return parentOption;
            }
            else {
                Debug.Log("No parent");
                return null;
            }
        }
        
        //Set the parent option of this option
        public void SetParentOption(BattleOption newParent) {
            parentOption = newParent;
        }

        //Returns the index of this option, from the parent's perspective
        public int GetIndexInParent() {
            return parentOption.checkChildIndex(this);
        }

        //Returns the index of the requested child from the derivedOptions array
        public int checkChildIndex(BattleOption child) {
            int i = 0;
            foreach (BattleOption battleOption in derivedOptions) {
                if (battleOption == child) {
                    return i;
                }
                i++;
            }
            return -1;
        }
    }
}