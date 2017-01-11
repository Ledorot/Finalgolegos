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

        public virtual void Start() {
            battleUI = BattleUI.BUI;
            if (battleUI == null) {
                Debug.LogError("No BattleUI reference found");
            }
        }

        public abstract void FirstOption();

        public abstract BattleOption Select();
        public abstract BattleOption Back();

        public abstract void UpNavigate();
        public abstract void DownNavigate();
        public abstract void LeftNavigate();
        public abstract void RightNavigate();
    }
}