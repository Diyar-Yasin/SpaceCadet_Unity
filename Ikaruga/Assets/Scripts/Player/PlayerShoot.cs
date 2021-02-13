using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerShoot
//              This script handles shooting for the player. It also handles other forms
//          of input such as switching the ship color.
public class PlayerShoot : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
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

    void Update()
    {
        if (Input.GetKeyDown("space"))                                                                                 // On spacebar press we change ship color between pink and orange.
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

        if (isPink)                                                                                                    // If the ship is pink, we shoot pink bullets, otherwise we shoot orange bullets.
        {
            GameObject bullet1 = PinkPlayerBulletPooler.current.GetPinkPlayerBullet();                                 // We get new bullets from the bullet poolers for the player
            
            if (bullet1 == null)                                                                                       // This ensures we don't try to activate nothing and get NullReferenceException
            {
                StartCoroutine(Shoot());
                yield break;
            }

            bullet1.transform.position = firePoint1.position;                                                          // Set the positon and rotation of the bullet
            bullet1.transform.rotation = bulletRotation;
            
            bullet1.SetActive(true);                                                                                   // Activate the bullet

            GameObject bullet2 = PinkPlayerBulletPooler.current.GetPinkPlayerBullet();                                 // We do this two times because the player shoots two bullets at once.

            if (bullet2 == null)
            {
                StartCoroutine(Shoot());
                yield break;
            }

            bullet2.transform.position = firePoint2.position;                                                          // Set the positon and rotation of the bullet
            bullet2.transform.rotation = bulletRotation;

            bullet2.SetActive(true);                                                                                   // Activate the bullet
        }
        else
        {
            GameObject bullet1 = OrangePlayerBulletPooler.current.GetOrangePlayerBullet();
            

            if (bullet1 == null)
            {
                StartCoroutine(Shoot());
                yield break;
            }

            bullet1.transform.position = firePoint1.position;                                                          // Set the positon and rotation of the bullet
            bullet1.transform.rotation = bulletRotation;
            
            bullet1.SetActive(true);                                                                                   // Activate the bullet


            GameObject bullet2 = OrangePlayerBulletPooler.current.GetOrangePlayerBullet();

            if (bullet2 == null)
            {
                StartCoroutine(Shoot());
                yield break;
            }

            bullet2.transform.position = firePoint2.position;                                                          // Set the positon and rotation of the bullet
            bullet2.transform.rotation = bulletRotation;

            bullet2.SetActive(true);                                                                                   // Activate the bullet
        }
        
        yield return new WaitForSeconds(0.1f);                                                                         // Delays each shot by a small amount to simulate machine gun speed shooting
        StartCoroutine(Shoot());
    }
}
