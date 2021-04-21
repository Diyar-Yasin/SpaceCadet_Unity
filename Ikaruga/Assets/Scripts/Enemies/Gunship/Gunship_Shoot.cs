using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunship_Shoot : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public Animator anim;
        public Transform firePoint1;
        public Transform firePoint2;
        public Transform firePoint3;
        public Transform firePoint4;
        public Transform firePoint5;
        public Transform firePoint6;
        public Transform cannon1;
        public Transform cannon2;
        public Transform garage1;
        public Transform garage2;
        public Transform megaCannon;
        public Transform megaCannonLeft;
        public Transform megaCannonRight;
        
    // PRIVATE
        // CONSTANTS
        private const int goldenAttack = 0;
        private const int gridAttack = 1;
        private const int holeAttack = 2;
        private const int spawnAttack = 3;

        // VARIABLES
        private bool bulletIsOrange;
        private int orangeCounter;
        private int pinkCounter;
        private GameObject laser1;
        private GameObject laser2;

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
        laser1 = gameObject.transform.GetChild(11).gameObject;
        laser2 = gameObject.transform.GetChild(12).gameObject;

        StartCoroutine(Shoot());
    }

    IEnumerator GoldenRatioAttack()
    {
        while (orangeCounter <= 100 && pinkCounter <= 110)
        {
            orangeCounter++;
            pinkCounter++;

            yield return new WaitForSeconds(0.03f);                                                                         // Since Base Enemy begins shooting immediately, we want the bullet pool to have some time to 
                                                                                                                       //   initialize and create its objects before we call on it to grab bullets.
        Quaternion bulletRotation = Quaternion.identity;                                                               //   Not doing this leads to a NullReferenceException because the pooledObjects.Count does not yet 
                                                                                                                       //   exist in PinkEnemyBulletPooler.cs (the orange verison worked for some reason).
        //if (bulletIsOrange)
        //{
            GameObject oBullet = OrangeEnemyBulletPooler.current.GetOrangeEnemyBullet();

            if (oBullet == null)
            {
                //StartCoroutine(GoldenRatioAttack()); // do we want this or just call Shoot();?
                yield break;
            }

            oBullet.transform.position = megaCannon.position;                                                // Set the positon and rotation of the bullet
            oBullet.transform.rotation = bulletRotation;
            oBullet.GetComponent<EnemyBulletController>().SetEnemyType("gunship");                                        // This will tell the bullet controller script how the bullet will move once shot
            oBullet.GetComponent<EnemyBulletController>().SetEnemyCounter(orangeCounter, goldenAttack, 0f);
            oBullet.SetActive(true);                                                                                   // Activate the bullet
        //}
       // else
       // {
            GameObject pBullet = PinkEnemyBulletPooler.current.GetPinkEnemyBullet();

            if (pBullet == null)
            {
                //StartCoroutine(GoldenRatioAttack());
                yield break;
            }

            pBullet.transform.position = megaCannon.position;                                          // Set the positon and rotation of the bullet
            pBullet.transform.rotation = bulletRotation;
            pBullet.GetComponent<EnemyBulletController>().SetEnemyType("gunship");                                        // This will tell the bullet controller script how the bullet will move once shot
            pBullet.GetComponent<EnemyBulletController>().SetEnemyCounter(pinkCounter, goldenAttack, 0f);
            pBullet.SetActive(true);                                                                                   // Activate the bullet
     //   
        }
        orangeCounter = 0;
        pinkCounter = 10;
        yield break;

    }

    IEnumerator GridAttack(Transform firePoint) 
    {
        float waitTime = Random.Range(0.6f, 1.9f);

        while (orangeCounter <= 8)
        {
            orangeCounter++;

            yield return new WaitForSeconds(0.05f);                                                                    // Since Base Enemy begins shooting immediately, we want the bullet pool to have some time to 
                                                                                                                       //   initialize and create its objects before we call on it to grab bullets.
            Quaternion bulletRotation = Quaternion.identity;                                                               //   Not doing this leads to a NullReferenceException because the pooledObjects.Count does not yet 
                                                                                                                       //   exist in PinkEnemyBulletPooler.cs (the orange verison worked for some reason).

            if (bulletIsOrange)
            {
                GameObject oBullet = OrangeEnemyBulletPooler.current.GetOrangeEnemyBullet();

                if (oBullet == null)
                {
                    yield break;
                }

                oBullet.transform.position = firePoint.position;                                                // Set the positon and rotation of the bullet
                oBullet.transform.rotation = bulletRotation;
                oBullet.GetComponent<EnemyBulletController>().SetEnemyType("gunship");                                        // This will tell the bullet controller script how the bullet will move once shot
                oBullet.GetComponent<EnemyBulletController>().SetEnemyCounter(orangeCounter, gridAttack, waitTime);
                oBullet.SetActive(true);                                                                                   // Activate the bullet
            }
            else
            {
                GameObject pBullet = PinkEnemyBulletPooler.current.GetPinkEnemyBullet();

                if (pBullet == null)
                {
                    yield break;
                }

                pBullet.transform.position = firePoint.position;                                          // Set the positon and rotation of the bullet
                pBullet.transform.rotation = bulletRotation;
                pBullet.GetComponent<EnemyBulletController>().SetEnemyType("gunship");                     // This will tell the bullet controller script how the bullet will move once shot
                pBullet.GetComponent<EnemyBulletController>().SetEnemyCounter(pinkCounter, gridAttack, waitTime);
                pBullet.SetActive(true);                                                                   // Activate the bullet
            
            }
        }
        orangeCounter = 0;
        yield break;
    }

    IEnumerator HoleInTheWallAttack()
    {
        anim.SetBool("Charging_Laser", true);
        yield return new WaitForSeconds(.5f);       
        anim.SetBool("Charging_Laser", false);

        laser1.GetComponent<Laser>().controlLasers(true); //Getting the component each time may be very inefficient!
        laser2.GetComponent<Laser>().controlLasers(true);

        while (orangeCounter <= 30)
        {
            orangeCounter++;

            yield return new WaitForSeconds(1f);                                                                    // Since Base Enemy begins shooting immediately, we want the bullet pool to have some time to 
                                                                                                                       //   initialize and create its objects before we call on it to grab bullets.
            Quaternion bulletRotation = Quaternion.identity;                                                               //   Not doing this leads to a NullReferenceException because the pooledObjects.Count does not yet 
                                                                                                                       //   exist in PinkEnemyBulletPooler.cs (the orange verison worked for some reason).
            float spacing = -22.5f;
            const float spacingInterval = 1.5f;
            for (int i = 0; i < 23; i++) { // repeating this 15 times to produce 30 total bullets that simulate a wall of bullets, each one spaced 1.5 apart in the X direction
                GameObject oBullet = OrangeEnemyBulletPooler.current.GetOrangeEnemyBullet();

                if (oBullet == null)
                {
                    laser1.GetComponent<Laser>().controlLasers(false);
                    laser2.GetComponent<Laser>().controlLasers(false);
                    yield break;
                }

                oBullet.transform.position = firePoint1.position + new Vector3(spacing, 0f, 0f);                                                // Set the positon and rotation of the bullet
                oBullet.transform.rotation = bulletRotation;
                oBullet.GetComponent<EnemyBulletController>().SetEnemyType("gunship");                                        // This will tell the bullet controller script how the bullet will move once shot
                oBullet.GetComponent<EnemyBulletController>().SetEnemyCounter(orangeCounter, holeAttack, spacing);
                oBullet.SetActive(true);                                                                         // Activate the bullet
                
                spacing += spacingInterval;

                GameObject pBullet = PinkEnemyBulletPooler.current.GetPinkEnemyBullet();

                if (pBullet == null)
                {
                    laser1.GetComponent<Laser>().controlLasers(false);
                    laser2.GetComponent<Laser>().controlLasers(false);
                yield break;
                }

                pBullet.transform.position = firePoint1.position + new Vector3(spacing, 0f, 0f);                                            // Set the positon and rotation of the bullet
                pBullet.transform.rotation = bulletRotation;
                pBullet.GetComponent<EnemyBulletController>().SetEnemyType("gunship");                     // This will tell the bullet controller script how the bullet will move once shot
                pBullet.GetComponent<EnemyBulletController>().SetEnemyCounter(pinkCounter, holeAttack, 0f);
                pBullet.SetActive(true);

                spacing += spacingInterval;  
            }
        }
        orangeCounter = 0;
        laser1.GetComponent<Laser>().controlLasers(false);
        laser2.GetComponent<Laser>().controlLasers(false);

        yield break;
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

    IEnumerator Shoot()
    {

        for (int i = 0; i < 20; i++)
        {
            //StartCoroutine(GridAttack(firePoint1));
            yield return new WaitForSeconds(1f);
            StartCoroutine(HoleInTheWallAttack());
            yield return new WaitForSeconds(1f);
            //StartCoroutine(GoldenRatioAttack());
            yield return new WaitForSeconds(1f);
            //StartCoroutine(GoldenRatioAttack());
            yield return new WaitForSeconds(4f);
        }
        
        /*
        bulletIsOrange = false;
        StartCoroutine(GridAttack(firePoint6));
        bulletIsOrange = true;
        StartCoroutine(GridAttack(firePoint2));
        yield return new WaitForSeconds(1f);
        bulletIsOrange = false;
        StartCoroutine(GridAttack(firePoint5));
        bulletIsOrange = true;
        StartCoroutine(GridAttack(firePoint3));
        bulletIsOrange = false;
        StartCoroutine(GridAttack(firePoint4));
        yield return new WaitForSeconds(1f);
        StartCoroutine(GoldenRatioAttack());*/
        /*int[] movesLeft = {0, 1, 2, 3};
        int movesLeftLen = 4;
        //int move = movesLeft[Random.Range(0, movesLeftLen)];
        int move = 0;
        while (movesLeftLen > 0) // this loops forever
        {
            switch (move)
            {
                case 0:
                    StartCoroutine(GoldenRatioAttack());
                    //movesLeft = RemoveMove(move, movesLeftLen, movesLeft);
                    //movesLeftLen--;
                    break;
                case 1:
                    // GridAttack
                    movesLeft = RemoveMove(move, movesLeftLen, movesLeft);
                    movesLeftLen--;
                    break;
                case 2:
                    // HoleInTheWallAttack
                    movesLeft = RemoveMove(move, movesLeftLen, movesLeft);
                    movesLeftLen--;
                    break;
                case 3:
                    // EnemySpawnAttack
                    movesLeft = RemoveMove(move, movesLeftLen, movesLeft);
                    movesLeftLen--;
                    break;
            }

            /*if (movesLeftLen == 0) 
            {
                movesLeftLen = 4;
                for (int i = 0; i < movesLeftLen; i++)
                {
                    movesLeft[i] = i;
                }
            }
            
            move = movesLeft[Random.Range(0, movesLeftLen)];
        }*/

        
    }
}
