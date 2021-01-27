using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public Transform firePoint1;
    public Transform firePoint2;
    public GameObject bulletPrefab;
    public GameObject player;

    public Animator anim;

    public float bulletForce = 40f;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            anim.SetTrigger("Switch_Ship");
        }
    }

    IEnumerator Shoot()
    {
        Quaternion bulletRotation = Quaternion.identity;

        // Create bullet1 and 2 game objects and put them into variables
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint1.position, bulletRotation);
        GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.position, bulletRotation);

        // Give the bullets physics
        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();

        // Add force to the bullets
        rb1.AddForce(firePoint1.up * bulletForce, ForceMode2D.Impulse);
        rb2.AddForce(firePoint2.up * bulletForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Shoot());
    }
}
