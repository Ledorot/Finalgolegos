using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Golegos;

namespace Golegos {

    public class CharacterOption : BattleOption {

        public MenuOption menuOp;
        public NewBattleManager bm;
        //A reference to the character
        private MapCharacter character = null;

        void Awake() {
            if (menuOp == null) {
                Debug.LogError("No menuOp available!");
            }
            if (bm == null) {
                Debug.LogError("No BattleManager selected!");
            }
        }

        public override void Start() {
            base.Start();
            if ((character = MapCharacter.mapChar) == null) {
                Debug.LogError("No reference to MapCharacter found!");
            }
        }

        public override void FirstOption() {
            //Update the character selection
        }

        public override BattleOption Select() {
            //Change the selection from the character to the menu
            menuOp.SetChildrenNewEnable(true);
            return menuOp;
        }

        public override BattleOption Back() {
            //Change the selection from the menu to the character
            return null;
        }

        public override void UpNavigate() {
            ;
        }

        public override void DownNavigate() {
            ;
        }

        public override void LeftNavigate() {
            //Change the position of the character selection
        }

        public override void RightNavigate() {
            //Change the position of the character selection
        }
    }
}