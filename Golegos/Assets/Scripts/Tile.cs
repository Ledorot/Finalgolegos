using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour {

    public Tile(Vector3 pos) {
        transform.position = pos;
    }

    public bool trigger = false;
    private SpriteRenderer sprite;

}