using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BaseEnemyBulletController
//              Bullets fired by Base Enemies can take on 1 of 3 path styles depending on
//          where the bullet is on the game screen. This script controls the path the
//          bullets follow as well as how they get disabled.
//
//          Note: Bullets on the left of the screen go right, bullets on the right of the
//          screen go left, and bullets in the center go forward.
//
//          UNDER CONSTRUCTION: I plan to change the path of middle bullets to
//          something somewhat more interesting in the future.
public class BaseEnemyBulletController : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public float bulletForce;
        private Rigidbody2D rb;

    // PRIVATE
        // CONSTANTS
        private const int leftOfScreen = -10;
        private const int rightOfScreen = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (gameObject.transform.position.x <= leftOfScreen)
        {
            rb.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
        }
        else if (gameObject.transform.position.x >= rightOfScreen)
        {
            rb.AddForce(-Vector2.right * bulletForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(-Vector2.up * bulletForce, ForceMode2D.Impulse);
        }
    }

    private void OnEnable()
    {
        if (rb != null)                                                                                                // When we enable a bullet from the pool, it may be in a different position or have lost its
        {                                                                                                              //   rb.AddForce so we redo what is in the start method to give the bullet direction.
            if (gameObject.transform.position.x <= leftOfScreen)
            {
                rb.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
            }
            else if (gameObject.transform.position.x >= rightOfScreen)
            {
                rb.AddForce(-Vector2.right * bulletForce, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(-Vector2.up * bulletForce, ForceMode2D.Impulse);
            }

            Invoke("Disable", 2f);
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()                                                                                           // This is added to ensure nothing goes wonky when we try to disable a bullet.
    {
        CancelInvoke();
    }


}
