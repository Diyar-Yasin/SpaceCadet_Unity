using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyBulletController : MonoBehaviour
{
    public float bulletForce;
    private Rigidbody2D rb;

    private const int leftOfScreen = -10;
    private const int rightOfScreen = 10;

    // Start is called before the first frame update
    void Start()
    {
        // Get the bullet's Rigidbody
        rb = GetComponent<Rigidbody2D>();

        // Add force to the bullets in certain directions depending on if they are to the left of the screen
        //   to the right of the screen or in the middle of the screen.
        if (gameObject.transform.position.x <= leftOfScreen)
        {
            // Shoot the bullets to the right if the ship is left
            rb.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);

        }
        else if (gameObject.transform.position.x >= rightOfScreen)
        {
            // Shoot the bullets to the left if the ship is right
            rb.AddForce(-Vector2.right * bulletForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(-Vector2.up * bulletForce, ForceMode2D.Impulse);

        }
       /* else
        {
            // We must be in the middle so left and right
            // In order to do this we will create an extra bullet
            GameObject extraBullet = Instantiate(bulletColor, transform.position, bulletRotation);

            Rigidbody2D extraRb = extraBullet.GetComponent<Rigidbody2D>();

            extraRb.AddForce(-transform.right * bulletForce, ForceMode2D.Impulse);
            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

        }*/
    }

    private void OnEnable()
    {
        if (rb != null)
        {
            if (gameObject.transform.position.x <= leftOfScreen)
            {
                // Shoot the bullets to the right if the ship is left
                rb.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);

            }
            else if (gameObject.transform.position.x >= rightOfScreen)
            {
                // Shoot the bullets to the left if the ship is right
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

    private void OnDisable()
    {
        CancelInvoke();
    }


}
