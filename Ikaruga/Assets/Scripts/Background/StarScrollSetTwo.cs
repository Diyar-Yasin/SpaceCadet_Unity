using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScrollSetTwo : MonoBehaviour
{
    private const int start_height = 283;
    private const int end_height = -83;
    public float speed = 20f;

    void Start()
    {
        Vector3 newPosition = transform.position; // We store the current position
        newPosition.y = start_height; // We set a axis, in this case the y axis
        transform.position = newPosition; // We pass it back
    }
    private void Update()
    {
        // We add +1 to the x axis every frame.
        // Time.deltaTime is the time it took to complete the last frame
        // The result of this is that the object moves one unit on the x axis every second
        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);

        if (transform.position.y <= end_height)
        {
            Vector3 newPosition = transform.position; // We store the current position
            newPosition.y = start_height; // We set a axis, in this case the y axis
            transform.position = newPosition; // We pass it back
        }
    }
}
