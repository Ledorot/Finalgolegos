using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour {

    //Character reference
    private Character character;

    /******************
    *   METHODS
    ******************/


    private void Awake() {
        character = GetComponent<Character>();
    }

    private void FixedUpdate() {

        //Player's horizontal movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        character.Move(h, v);
    }
}