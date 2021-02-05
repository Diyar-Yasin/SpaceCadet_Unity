using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// StarScrollSetTwo       
//              A basic script that takes a quad that acts as a background with a star texture and
//          moves the object down across the camera and then puts it back up to its original
//          y-position infnitely. This simulates a moving background.
//
//          Note: This is the second version of this script because 2 quads containing stars are
//          needed to ensure that when the quad is reset to its original position the player
//          does not notice because the stars appear to move fluidly.
public class StarScrollSetTwo : MonoBehaviour
{
    // PRIVATE
        // CONSTANTS
        private const int start_height = 283;
        private const int end_height = -83;

    // PUBLIC
        // VARIABLES
        public float speed = 20f;

    void Start()
    {
        Vector3 newPosition = transform.position;                                                                      // First we get the current position of the quad
        newPosition.y = start_height;                                                                                  // We set the y-axis of the quad to a constant based on eyeballing the unity game
        transform.position = newPosition;                                                                              //   scene. 
    }                                                                                                                  // Finally we change the position of the quad to this start position

    private void Update()
    {
        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);                                              // This moves the position of the quad down by the speed * time

        if (transform.position.y <= end_height)                                                                        // If we are under the end height then we want to set the quad back to the start 
        {                                                                                                              //   position.                          
            Vector3 newPosition = transform.position;
            newPosition.y = start_height;
            transform.position = newPosition;
        }
    }
}
