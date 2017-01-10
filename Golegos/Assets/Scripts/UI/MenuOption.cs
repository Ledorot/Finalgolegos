using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Golegos;
using System;

namespace Golegos {

    [RequireComponent(typeof(Text))]
    public class MenuOption : BattleOption {

        //List of battle options that derive from this option
        public MenuOption[] derivedOptions;
        
        //The option that enabled this option
        protected BattleOption parentOption = null;

        //Indicates whether or not the menu should animate to the left when this option is selected
        public bool increasesDepth = true;

        //The amount of options available after selecting this option
        protected int optionsNum;
        //Keeps track of the current option index
        protected int currentIndex = 0;
        //The amount of time until this option hides its derived options
        //public float hideTime = .2f;

        public virtual void Awake() {
            //Check if the number of derived options is 0
            optionsNum = derivedOptions.Length;
            if (optionsNum == 0 && increasesDepth) {
                Debug.LogError("Can't increase depth without sub-options!");
            }
        }

        public override void Start() {
            base.Start();
            //Set this option as the parent of all the derived options
            foreach (MenuOption battleOp in derivedOptions) {
                if (battleOp != null) {
                    battleOp.SetParentOption(this);
                }
            }
        }

        public override void FirstOption() {
            Invoke("FirstUpdate", .005f);
            SetChildrenNewEnable(true);
        }

        public void FirstUpdate() {
            battleUI.UpdateOptionBox(0);
        }

        //Called when this option gets returned to (so that this option can hide its derived options)
        public void SetToHideChildren() {
            SetChildrenNewEnable(false);
        }

        public virtual void SetChildrenNewEnable(bool newEnable) {
            foreach (MenuOption battleOp in derivedOptions) {
                if (battleOp != null) {
                    battleOp.gameObject.SetActive(newEnable);
                }
            }
        }

        public override void UpNavigate() {
            if (currentIndex <= 0) {
                currentIndex = optionsNum - 1;
            }
            else {
                currentIndex--;
            }
            battleUI.UpdateOptionBox(currentIndex);
        }

        public override void DownNavigate() {
            if (currentIndex < optionsNum - 1) {
                currentIndex++;
            }
            else {
                currentIndex = 0;
            }
            battleUI.UpdateOptionBox(currentIndex);
        }

        public override void LeftNavigate() {
            ;
        }

        public override void RightNavigate() {
            ;
        }

        //Called when this option is selected
        public override BattleOption Select() {
            if (increasesDepth) {
                battleUI.IncreaseDepth();
            }
            battleUI.UpdateOptionBox(0);
            derivedOptions[currentIndex].SetChildrenNewEnable(true);
            return derivedOptions[currentIndex];
        }

        //Called when this option is exited from
        public override BattleOption Back() {
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
            MenuOption menuOption = parentOption as MenuOption;
            if (menuOption != null) {
                return menuOption.checkChildIndex(this);
            }
            return -1;
        }

        //Returns the index of the requested child from the derivedOptions array
        public virtual int checkChildIndex(MenuOption child) {
            int i = 0;
            foreach (MenuOption MenuOption in derivedOptions) {
                if (MenuOption == child) {
                    return i;
                }
                i++;
            }
            return -1;
        }
    }
}