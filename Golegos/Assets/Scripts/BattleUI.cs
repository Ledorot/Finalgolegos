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

        [SerializeField]
        private float optionWidth;
        [SerializeField]
        private float optionHeight;

        void Awake() {
            if (boxPositions.Length < 4) {
                Debug.LogError("Not enough boxPositions in the BattleUI component!");
            }
        }

        void Start() {
            optionBox = GameObject.FindGameObjectWithTag("OptionBox");
            if (optionBox == null) {
                Debug.Log("Couldn't find OptionBox");
            }
            /*
            Image image = optionBox.GetComponent<Image>();
            if (image == null) {
                Debug.LogError("The optionBox transform has no Image component!");
            }
            else {
                float width = image.sprite.bounds.size.x;
                float height = image.sprite.bounds.size.y;

                //Scale the option box according to the specified width and height
                if (width != optionWidth) {
                    Vector3 theScale = optionBox.localScale;
                    theScale.x *= (optionWidth / width);
                    Debug.Log("" + optionWidth / width);
                    //optionBox.localScale = theScale;
                }
                if (height != optionHeight) {
                    Vector3 theScale = optionBox.localScale;
                    theScale.y *= (optionHeight / height);
                    //optionBox.localScale = theScale;
                }
                optionBox.transform.position = boxPositions[1].position;
            }
            */
        }

        //Receives the index of the vertical position we are navigating to
        public void NavigateOption(int index) {
            optionBox.transform.position = boxPositions[index].position;
        }

        //Receives the index of the character that we are navigating to
        public void NavigateCharacter(int index) {
            //How many characters will there be in the screen
        }
    }
}