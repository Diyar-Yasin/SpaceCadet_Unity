using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BulletController
//              This script adds upward force to bullets shot by the player as well as
//          disables them and returns them to the player bullet pool after 2 seconds.
//
//          Credits: I got most of this code from a youtube video linked in one of the other
//          scripts.
public class BulletController : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public float bulletForce;

    // PRIVATE
        // VARIABLES
        private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                                                                              // We always start by getting the rigidbody attached to the bullet and then adding the initial
                                                                                                                       //   force upwards.
        rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
    }

    private void OnEnable()
    {
        if (rb != null)                                                                                                // Since bullets are reused, when they get disabled they may lose the force we added on
        {                                                                                                              //   instantiation. So if that is the case we add that.
            rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
        }

        Invoke("Disable", 2f);                                                                                             
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()                                                                                           // This is added extra to the Disable() function to ensure that nothing goes wrong when we disable.
    {
        CancelInvoke();
    }
}
