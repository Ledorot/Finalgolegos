using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Golegos;

namespace Golegos {

    /*
    * Represents an attack option, which you navigate through differently
    * (given that there might be more attacks than the amount of options in 
    * the vertical layout) and also triggers an attack function on the BattleManager
    */
    [RequireComponent(typeof(Text))]
    public class SpecificAttackOption : MenuOption {

        public new void SetChildrenNewEnable(bool newEnable) {
            ;
        }

        public override BattleOption Select() {
            //NewBattleManager
            return null;
        }

        public override BattleOption RightNavigate() {
            return null;
        }

        public override int checkChildIndex(MenuOption child) {
            return -1;
        }
    }
}