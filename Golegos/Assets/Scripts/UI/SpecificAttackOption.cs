using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Golegos;

namespace Golegos {

    [RequireComponent(typeof(Text))]
    public class SpecificAttackOption : MenuOption {

        //A reference to the character
        private MapCharacter character;

        new public virtual void Start() {
            base.Start();
            if ((character = MapCharacter.mapChar) == null) {
                Debug.LogError("No reference to MapCharacter found!");
            }
        }

        new public virtual void UpNavigate() {
            //Change this
            if (currentIndex <= 0) {
                currentIndex = optionsNum - 1;
            }
            else {
                currentIndex--;
            }
            battleUI.UpdateOptionBox(currentIndex);
        }

        new public virtual void DownNavigate() {
            //Change this
            if (currentIndex < optionsNum - 1) {
                currentIndex++;
            }
            else {
                currentIndex = 0;
            }
            battleUI.UpdateOptionBox(currentIndex);
        }

        public override void SetChildrenNewEnable(bool newEnable) {
            ;
        }

        new public virtual BattleOption Select() {
            //NewBattleManager
            return null;
        }

        public override int checkChildIndex(MenuOption child) {
            return -1;
        }
    }
}