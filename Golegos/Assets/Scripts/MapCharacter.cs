using UnityEngine;
using System.Collections;
using Golegos;

namespace Golegos {

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class MapCharacter : MonoBehaviour {

        //Character components

        private Rigidbody2D rb;
        public NewBattleManager battleManager;
        public Character character;
        public static MapCharacter mapChar;

        //Movement variables

        [SerializeField]
        private float movSpeed = 6f;

        //Control variables

        private bool inMenu = true;
        //If the player is holding down a key, consider the input only once
        private bool considerMov = true;

        /****************
        *   METHODS
        ****************/

        void Awake() {
            if (mapChar == null) {
                mapChar = this;
            }
            rb = GetComponent<Rigidbody2D>();
            if (character == null) {
                Debug.LogError("No Character specified!");
            }
        }

        void Start() {
            if (battleManager == null) {
                Debug.LogError("No BattleManager instance assigned");
            }
        }

        //Handles the character's movement
        public void Move(float moveX, float moveY) {
            //2D map navigation controls
            if (!inMenu) {
                rb.velocity = new Vector3(moveX * movSpeed, moveY * movSpeed, 0f);
            }
            //Battle menu controls
            else {
                if (considerMov) {
                    considerMov = false;
                    if (moveX > 0) {
                        battleManager.RightSelection();
                    }
                    else if (moveX < 0) {
                        battleManager.LeftSelection();
                    }
                    else if (moveY > 0) {
                        battleManager.UpSelection();
                    }
                    else if (moveY < 0) {
                        battleManager.DownSelection();
                    }
                    //If the player isn't pressing anything, continue considering movement
                    else {
                        considerMov = true;
                    }
                }
                //When the player stops holding down the keys, stop ignoring input
                else {
                    if (!considerMov && moveX == 0 && moveY == 0) {
                        considerMov = true;
                    }
                }
            }
        }

        public void Select() {
            battleManager.Select();
        }

        public void Back() {
            battleManager.Back();
        }

        
    }
}