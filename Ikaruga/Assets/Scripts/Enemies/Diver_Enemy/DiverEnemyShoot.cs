using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DiverEnemyShoot
//              This program shoots bullets from diver enemies. It will shoot the appropriate
//          colored bullet depending on the ships color.
//
//          UNDER CONSTRUCTION: I might be able to merge this with BaseEnemyShoot into one EnemyShoot
public class DiverEnemyShoot : MonoBehaviour
{
    // PRIVATE
        // VARIABLES
        private bool bulletIsOrange;
        private Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();

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
        yield return new WaitForSeconds(0.2f);                                                                         // We want the bullet pooler to have some time to initialize and create its objects before we call 
                                                                                                                       //   on it to grab bullets. Not doing this leads to a NullReferenceException because the
        Quaternion bulletRotation = Quaternion.identity;                                                               //   the pooledObjects.Count does not yet exist in PinkEnemyBulletPooler.cs (the orange verison 
                                                                                                                       //   worked for some reason).

        while (anim.GetCurrentAnimatorStateInfo(0).IsName("O_Diver_Enemy_Dive") ||                                        // We only want to shoot bullets while we are diving across the map.
            anim.GetCurrentAnimatorStateInfo(0).IsName("P_Diver_Enemy_Dive"))               
        {
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
                oBullet.GetComponent<EnemyBulletController>().SetEnemyType("diver");                                       // This will tell the bullet controller script how the bullet will move once shot

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

                pBullet.GetComponent<EnemyBulletController>().SetEnemyType("diver");                                       // This will tell the bullet controller script how the bullet will move once shot
                pBullet.SetActive(true);                                                                                   // Activate the bullet

            }
        }

        StartCoroutine(Shoot());
    }
}
