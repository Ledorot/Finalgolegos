using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Golegos;

namespace Golegos {
    /*
    * Represents a generic option available in battle
    * to be interacted with by the BattleManager
    */
    public abstract class BattleOption : MonoBehaviour {

        //Reference to the battleUI
        protected BattleUI battleUI;
        //Reference to the BattleManager
        protected NewBattleManager battleManager;

        //The option that enabled this option
        protected BattleOption parentOption = null;

        public virtual void Start() {
            battleUI = BattleUI.BUI;
            if (battleUI == null) {
                Debug.LogError("No BattleUI reference found");
            }
            battleManager = NewBattleManager.bm;
            if (battleManager == null) {
                Debug.LogError("No static instance of NewBattleManager found!");
            }
        }

        public abstract void FirstOption();

        public abstract BattleOption Select();
        public abstract BattleOption Back();
        public abstract void UpNavigate();
        public abstract void DownNavigate();
        public abstract BattleOption LeftNavigate();
        public abstract BattleOption RightNavigate();

        //Set the parent option of this option
        public void SetParentOption(BattleOption newParent) {
            parentOption = newParent;
        }

        public BattleOption GetParentOption() {
            return parentOption;
        }
    }
}