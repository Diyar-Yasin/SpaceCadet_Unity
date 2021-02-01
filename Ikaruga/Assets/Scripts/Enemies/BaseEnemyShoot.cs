using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyShoot : MonoBehaviour
{

    public GameObject orangeBulletPrefab;

    public float bulletForce = 40f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        Quaternion bulletRotation = Quaternion.identity;

        GameObject bullet = Instantiate(orangeBulletPrefab, transform.position, bulletRotation);

        // Give the bullets physics
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Add force to the bullets
        rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Shoot());
    }
}
