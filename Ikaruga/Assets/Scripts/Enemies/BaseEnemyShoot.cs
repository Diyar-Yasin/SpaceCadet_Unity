using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyShoot : MonoBehaviour
{

    public GameObject orangeBulletPrefab;
    public GameObject pinkBulletPrefab;
    private GameObject bulletColor;

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

        if (GameObject.ReferenceEquals(bulletColor, orangeBulletPrefab))
        {
            GameObject bullet = OrangeEnemyBulletPooler.current.GetOrangeEnemyBullet();

            if (bullet == null)
            {
                StartCoroutine(Shoot(bulletColor));
                yield break;
            }

            // Set the positon and rotation of the bullet
            bullet.transform.position = gameObject.transform.position;
            bullet.transform.rotation = bulletRotation;

            // Activate the bullet
            bullet.SetActive(true);
        }
        else
        {
            GameObject bullet = PinkEnemyBulletPooler.current.GetPinkEnemyBullet();

            if (bullet == null)
            {
                StartCoroutine(Shoot(bulletColor));
                yield break;
            }

            // Set the positon and rotation of the bullet
            bullet.transform.position = gameObject.transform.position;
            bullet.transform.rotation = bulletRotation;

            // Activate the bullet
            bullet.SetActive(true);
        }
        
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Shoot(bulletColor));
    }
}
