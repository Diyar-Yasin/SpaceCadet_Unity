using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyShoot : MonoBehaviour
{

    public GameObject orangeBulletPrefab;
    public GameObject pinkBulletPrefab;
    private GameObject bulletColor;
    public float bulletForce = 40f;

    private const int leftOfScreen = -10;
    private const int rightOfScreen = 10;

    // Start is called before the first frame update
    void Start()
    {
        // Depending on which color our base enemy is, depends on which color of bullet we will
        //   pass to Shoot(bulletColor).
        if (gameObject.tag == "O_Enemy")
        {
            bulletColor = orangeBulletPrefab;
        }
        else
        {
            bulletColor = pinkBulletPrefab;
        }

        StartCoroutine(Shoot(bulletColor));
    }

    IEnumerator Shoot(GameObject bulletColor)
    {
        Quaternion bulletRotation = Quaternion.identity;

        GameObject bullet = Instantiate(bulletColor, transform.position, bulletRotation);

        // Give the bullets physics
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Add force to the bullets in certain directions depending on if they are to the left of the screen
        //   to the right of the screen or in the middle of the screen.
        if (gameObject.transform.localPosition.x <= leftOfScreen)
        {
            // Shoot the bullets to the right if the ship is left
            rb.AddForce(gameObject.transform.right * bulletForce, ForceMode2D.Impulse);
        }
        else if (gameObject.transform.localPosition.x >= rightOfScreen)
        {
            // Shoot the bullets to the left if the ship is right
            rb.AddForce(-transform.right * bulletForce, ForceMode2D.Impulse);
        }
        else
        {
            // We must be in the middle so left and right
            // In order to do this we will create an extra bullet
            GameObject extraBullet = Instantiate(bulletColor, transform.position, bulletRotation);

            Rigidbody2D extraRb = extraBullet.GetComponent<Rigidbody2D>();

            extraRb.AddForce(-transform.right * bulletForce, ForceMode2D.Impulse);
            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

        }
        
        

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Shoot(bulletColor));
    }
}
