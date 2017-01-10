using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Golegos;

namespace Golegos {

    [RequireComponent(typeof(Text))]
    public class AttackMenuOption : MenuOption {

        //A reference to the character
        private MapCharacter character = null;

        //How many attacks may there be
        public int numAttacks;
        //Is this class representing normal or special attacks
        public bool isSpecial = false;

        public override void Start() {
            base.Start();
            if ((character = MapCharacter.mapChar) == null) {
                Debug.LogError("No reference to MapCharacter found!");
            }
        }

        public override void SetChildrenNewEnable(bool newEnable) {
            int i = 0;
            string newText;
            while ((newText = character.GetAttackText(i++, isSpecial)) != null && i < numAttacks) {
                derivedOptions[i - 1].GetComponent<Text>().text = newText;
                derivedOptions[i - 1].gameObject.SetActive(newEnable);
            }
        }
    }
}