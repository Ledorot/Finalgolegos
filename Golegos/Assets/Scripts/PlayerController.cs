using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Golegos;

namespace Golegos {
    [RequireComponent(typeof(MapCharacter))]
    public class PlayerController : MonoBehaviour {

        //Character reference
        private MapCharacter character;

        /******************
        *   METHODS
        ******************/


        private void Awake() {
            character = GetComponent<MapCharacter>();
        }

        private void FixedUpdate() {

            //Player's horizontal movement
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            character.Move(h, v);
        }
    }
}