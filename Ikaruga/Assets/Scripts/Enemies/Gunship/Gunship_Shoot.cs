using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunship_Shoot : MonoBehaviour
{
    // PRIVATE
        // VARIABLES
        private bool bulletIsOrange;
        private int orangeCounter;
        private int pinkCounter;

    void Start()
    {
      /*  if (gameObject.tag == "O_Enemy")                                                                               // Checks the tag of the gameobject to determine if we need orange or pink bullets
        {
            bulletIsOrange = true;
        }
        else
        {
            bulletIsOrange = false;
        }*/
        bulletIsOrange = true;
        orangeCounter = 0;
        pinkCounter = 10;

        Shoot();
    }

    IEnumerator GoldenRatioAttack()
    {
        if (orangeCounter > 100)  // if counter goes up around 280+ range then the force values that are calculated are NaN leading to errors
        {
            orangeCounter = 0;
        } 
        else
        {
            orangeCounter++;
        }

        if (pinkCounter > 110) 
        {
            pinkCounter = 10;
        }
        else 
        {
            pinkCounter++;
        }

        yield return new WaitForSeconds(0.03f);                                                                         // Since Base Enemy begins shooting immediately, we want the bullet pool to have some time to 
                                                                                                                       //   initialize and create its objects before we call on it to grab bullets.
        Quaternion bulletRotation = Quaternion.identity;                                                               //   Not doing this leads to a NullReferenceException because the pooledObjects.Count does not yet 
                                                                                                                       //   exist in PinkEnemyBulletPooler.cs (the orange verison worked for some reason).
        //if (bulletIsOrange)
        //{
            GameObject oBullet = OrangeEnemyBulletPooler.current.GetOrangeEnemyBullet();

            if (oBullet == null)
            {
                StartCoroutine(GoldenRatioAttack()); // do we want this or just call Shoot();?
                yield break;
            }

            oBullet.transform.position = gameObject.transform.position;                                                // Set the positon and rotation of the bullet
            oBullet.transform.rotation = bulletRotation;
            oBullet.GetComponent<EnemyBulletController>().SetEnemyType("gunship");                                        // This will tell the bullet controller script how the bullet will move once shot
            oBullet.GetComponent<EnemyBulletController>().SetEnemyCounter(orangeCounter);
            oBullet.SetActive(true);                                                                                   // Activate the bullet
        //}
       // else
       // {
            GameObject pBullet = PinkEnemyBulletPooler.current.GetPinkEnemyBullet();

            if (pBullet == null)
            {
                StartCoroutine(GoldenRatioAttack());
                yield break;
            }

            pBullet.transform.position = gameObject.transform.position;                                                // Set the positon and rotation of the bullet
            pBullet.transform.rotation = bulletRotation;
            pBullet.GetComponent<EnemyBulletController>().SetEnemyType("gunship");                                        // This will tell the bullet controller script how the bullet will move once shot
            pBullet.GetComponent<EnemyBulletController>().SetEnemyCounter(pinkCounter);
            pBullet.SetActive(true);                                                                                   // Activate the bullet
     //   }
        
        Shoot();
    }

    IEnumerator GridAttack() 
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator HoleInTheWallAttack() 
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator EnemySpawnAttack() 
    {
        yield return new WaitForSeconds(1f);
    }

    int[] RemoveMove(int move, int len, int[] currentMoveSet)
    {
        for (int i = 0; i < len; i++) 
        {
            if (currentMoveSet[i] == move)
            {
                for (int j = i; j < len - 1; j++)
                {
                    currentMoveSet[j] = currentMoveSet[j + 1];
                }
                break;
            }
        }
        return currentMoveSet;
    }

    void Shoot()
    {
        int[] movesLeft = {0, 1, 2, 3};
        int movesLeftLen = 4;
        int move = move = movesLeft[Random.Range(0, movesLeftLen)];

        while (movesLeftLen > 0) // this loops forever
        {
            switch (move)
            {
                case 0:
                    StartCoroutine(GoldenRatioAttack());
                    RemoveMove(move, movesLeftLen, movesLeft);
                    movesLeftLen--;
                    break;
                case 1:
                    // GridAttack
                    RemoveMove(move, movesLeftLen, movesLeft);
                    movesLeftLen--;
                    break;
                case 2:
                    // HoleInTheWallAttack
                    RemoveMove(move, movesLeftLen, movesLeft);
                    movesLeftLen--;
                    break;
                case 3:
                    // EnemySpawnAttack
                    RemoveMove(move, movesLeftLen, movesLeft);
                    movesLeftLen--;
                    break;
            }

            if (movesLeftLen == 0) 
            {
                movesLeftLen = 4;
                for (int i = 0; i < movesLeftLen; i++)
                {
                    movesLeft[i] = i;
                }
            }
            
            move = movesLeft[Random.Range(0, movesLeftLen)];
        }

        
    }
}
