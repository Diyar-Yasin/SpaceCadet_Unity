using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BaseEnemyShoot
//              This program shoots bullets from base enemies. It will shoot the appropriate
//          colored bullet depending on the ships color.
public class BaseEnemyShoot : MonoBehaviour
{
    // PRIVATE
        // VARIABLES
        private bool bulletIsOrange;

    void Start()
    {
        if (gameObject.tag == "O_Enemy")                                                                               // Checks the tag of the gameobject to determine if we need orange or pink bullets
        {
            bulletIsOrange = true;
        }
        else
        {
            bulletIsOrange = false;
        }

        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.5f);                                                                         // Since Base Enemy begins shooting immediately, we want the bullet pool to have some time to 
                                                                                                                       //   initialize and create its objects before we call on it to grab bullets.
        Quaternion bulletRotation = Quaternion.identity;                                                               //   Not doing this leads to a NullReferenceException because the pooledObjects.Count does not yet 
                                                                                                                       //   exist in PinkEnemyBulletPooler.cs (the orange verison worked for some reason).
        if (bulletIsOrange)
        {
            GameObject oBullet = OrangeEnemyBulletPooler.current.GetOrangeEnemyBullet();

            if (oBullet == null)
            {
                StartCoroutine(Shoot());
                yield break;
            }

            oBullet.transform.position = gameObject.transform.position;                                                // Set the positon and rotation of the bullet
            oBullet.transform.rotation = bulletRotation;                                                               

            oBullet.SetActive(true);                                                                                   // Activate the bullet
        }
        else
        {
            GameObject pBullet = PinkEnemyBulletPooler.current.GetPinkEnemyBullet();

            if (pBullet == null)
            {
                StartCoroutine(Shoot());
                yield break;
            }

            pBullet.transform.position = gameObject.transform.position;                                                // Set the positon and rotation of the bullet
            pBullet.transform.rotation = bulletRotation;

            pBullet.SetActive(true);                                                                                   // Activate the bullet
        }
        
        StartCoroutine(Shoot());
    }
}