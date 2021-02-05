using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public Transform firePoint1;
    public Transform firePoint2;

    public GameObject orangeBulletPrefab;
    public GameObject pinkBulletPrefab;

    public float bulletForce = 20f;

    public GameObject player;
    public Animator anim;
    public bool isPink = false;


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

            if (isPink)
            {
                isPink = false;
            }
            else
            {
                isPink = true;
            }
        }
    }

    IEnumerator Shoot()
    {
        Quaternion bulletRotation = Quaternion.identity;

        if (isPink)
        {
            GameObject bullet1 = PinkPlayerBulletPooler.current.GetPinkPlayerBullet();
            
            if (bullet1 == null)
            {
                StartCoroutine(Shoot());
                yield break;
            }

            // Set the positon and rotation of the bullet
            bullet1.transform.position = firePoint1.position;
            bullet1.transform.rotation = bulletRotation;
            
            // Activate the bullet
            bullet1.SetActive(true);
            

            GameObject bullet2 = PinkPlayerBulletPooler.current.GetPinkPlayerBullet();

            if (bullet2 == null)
            {
                StartCoroutine(Shoot());
                yield break;
            }

            // Set the positon and rotation of the bullet
            bullet2.transform.position = firePoint2.position;
            bullet2.transform.rotation = bulletRotation;

            // Activate the bullet
            bullet2.SetActive(true);
        }
        else
        {
            GameObject bullet1 = OrangePlayerBulletPooler.current.GetOrangePlayerBullet();
            

            if (bullet1 == null)
            {
                StartCoroutine(Shoot());
                yield break;
            }

            // Set the positon and rotation of the bullet
            bullet1.transform.position = firePoint1.position;
            bullet1.transform.rotation = bulletRotation;
            

            // Activate the bullet
            bullet1.SetActive(true);
            

            GameObject bullet2 = OrangePlayerBulletPooler.current.GetOrangePlayerBullet();

            if (bullet2 == null)
            {
                StartCoroutine(Shoot());
                yield break;
            }

            // Set the positon and rotation of the bullet
            bullet2.transform.position = firePoint2.position;
            bullet2.transform.rotation = bulletRotation;

            // Activate the bullet
            bullet2.SetActive(true);
        }
        
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Shoot());
    }
}
