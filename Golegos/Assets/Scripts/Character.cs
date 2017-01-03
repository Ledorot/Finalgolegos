using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour {

    //Character components
    private Rigidbody2D rb;

    //Movement variables
    [SerializeField]
    private float movSpeed = 6f;

    /****************
    *   METHODS
    ****************/

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    //Handles the character's movement
    public void Move(float moveX, float moveY) {

        rb.velocity = new Vector3(moveX * movSpeed, moveY * movSpeed, 0f);
    }
}
