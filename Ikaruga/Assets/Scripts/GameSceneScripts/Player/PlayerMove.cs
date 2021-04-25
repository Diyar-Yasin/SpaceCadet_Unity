using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerMove
//              This script controls the movement of the player.
public class PlayerMove : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public float speed = 10f;
        public Rigidbody2D rb;

    // PRIVATE
        // VARIABLES
        Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");                                                                   // Input is handled here
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);                                         // Movement is handled here 
    }
}