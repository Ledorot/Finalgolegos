using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Golegos;

namespace Golegos {

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