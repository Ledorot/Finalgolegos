using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Golegos
{
    public class BattleUI : MonoBehaviour {

        //private Transform optionBox;
        private GameObject optionBox;
        public Transform[] boxPositions;
        public Transform optionsUI;
        private Animator optionsAnimator;

        //Static reference to this class, so that the options can manipulate the optionsox and depth of the menu
        public static BattleUI BUI;

        //Keeps track of the depth of the options navigating
        private int depth = 0;
        //Stores last indexes of selected options
        private int lastCharacterIndex = 0;
        

        void Awake() {

            if (BUI == null) {
                BUI = this;
            }
            if (boxPositions.Length < 4) {
                Debug.LogError("Not enough boxPositions in the BattleUI component!");
            }
            if (optionsUI == null) {
                Debug.LogError("No optionsUI selected");
            }
            else {
                optionsAnimator = optionsUI.GetComponent<Animator>();
                if (optionsAnimator == null) {
                    Debug.LogError("optionsUI has no Animator component");
                }
            }
        }

        void Start() {
            optionBox = GameObject.FindGameObjectWithTag("OptionBox");
            if (optionBox == null) {
                Debug.Log("Couldn't find OptionBox");
            }
            else {
                optionBox.transform.position = boxPositions[0].position;
            }
        }
        
        public void UpdateOptionBox(int index) {
            if (optionBox != null && boxPositions[index] != null) {
                optionBox.transform.position = boxPositions[index].position;
            }
            else {
                Debug.Log("Failed attempt");
            }
        }

        public void IncreaseDepth() {
            optionsAnimator.SetInteger("Depth", ++depth);
        }

        public void DecreaseDepth() {
            optionsAnimator.SetInteger("Depth", --depth);
        }
    }
}