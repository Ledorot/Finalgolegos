using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Golegos
{
    public class BattleUI : MonoBehaviour {

        //private Transform optionBox;
        private GameObject optionBox;
        //Static reference to this class, so that the options can manipulate the optionsox and depth of the menu
        public static BattleUI BUI;
        //Reference to the several positions the box can occupy
        public Transform[] boxPositions;
        //The UI element that will be moved
        public RectTransform optionsUI;
        private Animator optionsAnimator;

        //Keeps track of the depth of the options navigating
        private int depth = 0;

        /************************
        *   ANIMATING THE UI
        *************************/

        ////The amount of units used to move the optionsUI
        //public float horizontalUnits;
        ////The amount of time it will take to move the optionsUI
        //public float horizontalTime;
        ////The amount of units used to move the optionsUI per tick
        //private float horizontalTick = 0f;
        ////Indicates whether the optionsUI should be moved
        //private bool moveHorizontal = false;
        ////Indicates whether the optionsUI should invert its movement direction mid-movement
        //private bool reverseHorizontal = false;
        ////Indicates the direction to move the optionsUI to
        //private int horizontalDir = 0;
        ////Keeps track of how much the optionsUI has moved along the same transition
        //private float horizontalCounter = 0f;

        ////The amount of units used to move the attacksUI
        //public float verticalUnits;
        ////The amount of time it will take to move the attacksUI
        //public float verticalTime;
        ////The amount of units used to move the attacksUI per tick
        //private float verticalTick;
        ////Indicates whether the attacksUI should be moved
        //private bool moveVertical = false;
        ////Indicates whether the attacksUI should invert its movement direction mid-movement
        //private bool reverseVertical = false;
        ////Indicates the direction to move the attacksUI to
        //private int verticalDir = 0;
        ////Keeps track of how much the attacksUI has moved along the same transition
        //private float verticalCounter = 0f;



        void Awake() {
            if (BUI == null) {
                BUI = this;
            }
            if (boxPositions.Length < MenuOption.maxOptions) {
                Debug.LogError("Not enough boxPositions in the BattleUI component!");
            }
            if (optionsUI == null) {
                Debug.LogError("No optionsUI selected");
            }
            //Multiply the amout of units by the time and FixedUpdate rate (0.02 sec)
            //so that we get the amout of units to move the optionsUI by per FixedUpdate
            //horizontalTick = horizontalUnits * horizontalTime * 0.02f;
            //verticalTick = verticalUnits * verticalTime * 0.02f;
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

        //void FixedUpdate() {
        //    if (moveHorizontal) {
        //        //Debug.Log(horizontalCounter);
        //        horizontalCounter += horizontalTick * horizontalDir;
        //        Vector2 newPos = optionsUI.localPosition;
        //        //THIS LINE DOESN'T WORK, THE UI DOESN'T MOVE IN THE X AXIS, ONLY IN THE Y AXIS
        //        //optionsUI.localPosition += new Vector3(horizontalTick * horizontalDir, 0f, 0f);
        //        optionsUI.localPosition += new Vector3(4f, 4f, 0f);
        //        //Once the UI has moved the desired amount, stop moving it
        //        if (!reverseHorizontal && horizontalCounter >= horizontalUnits) {
        //            moveHorizontal = false;
        //            horizontalDir = 0;
        //        }
        //        else if (reverseHorizontal && horizontalCounter <= 0f) {
        //            reverseHorizontal = false;
        //            moveHorizontal = false;
        //            horizontalDir = 0;
        //        }
        //    }
        //    if (moveVertical) {

        //    }
        //}
        
        public void UpdateOptionBox(int index) {
            if (optionBox != null && boxPositions.Length > index) {
                optionBox.transform.position = boxPositions[index].position;
            }
            else {
                Debug.Log("Failed moving the optionsUI");
            }
        }

        public void IncreaseDepth() {
            optionsAnimator.SetInteger("Depth", ++depth);
            //horizontalDir = 1;
            //moveHorizontal = true;
        }

        public void DecreaseDepth() {
            optionsAnimator.SetInteger("Depth", --depth);
            //If the optionsBox is already moving, invert its movement
            //if (!(horizontalDir != 1)) {
            //    reverseHorizontal = true;
            //}
            //horizontalDir = -1;
            //moveHorizontal = true;
        }
    }
}