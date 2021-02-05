using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BaseEnemyShoot
//              
public class BaseEnemyShoot : MonoBehaviour
{
    private bool bulletIsOrange;

    // Start is called before the first frame update
    void Start()
    {
        // Depending on which color our base enemy is, depends on which color of bullet we will
        //   pass to Shoot(bulletColor).
        if (gameObject.tag == "O_Enemy")
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
        // Since Base Enemy begins shooting immediately, we want the bullet pool to have some time to initialize and create its objects before we call on it to grab bullets.
        //   Not doing this leads to a NullReferenceException because the pooledObjects.Count does not yet exist in PinkEnemyBulletPooler.cs (the orange verison worked for some reason).
        yield return new WaitForSeconds(0.5f);

        Quaternion bulletRotation = Quaternion.identity;

        if (bulletIsOrange)
        {
            GameObject oBullet = OrangeEnemyBulletPooler.current.GetOrangeEnemyBullet();

            if (oBullet == null)
            {
                StartCoroutine(Shoot());
                yield break;
            }

            // Set the positon and rotation of the bullet
            oBullet.transform.position = gameObject.transform.position;
            oBullet.transform.rotation = bulletRotation;

            // Activate the bullet
            oBullet.SetActive(true);
        }
        else
        {
            GameObject pBullet = PinkEnemyBulletPooler.current.GetPinkEnemyBullet();

            if (pBullet == null)
            {
                StartCoroutine(Shoot());
                yield break;
            }

            // Set the positon and rotation of the bullet
            pBullet.transform.position = gameObject.transform.position;
            pBullet.transform.rotation = bulletRotation;

            // Activate the bullet
            pBullet.SetActive(true);
        }
        
        StartCoroutine(Shoot());
    }
}
