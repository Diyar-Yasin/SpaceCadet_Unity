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
        private const float bulletTravelTime = 2f;

        // VARIABLES
        private bool bulletIsOrange;
        private int orangeCounter;
        private int pinkCounter;
        private GameObject laser1;
        private GameObject laser2;
        private AudioManager audioManager;

    void Start()
    {
        bulletIsOrange = true;
        orangeCounter = 0;
        pinkCounter = 10;
        laser1 = gameObject.transform.GetChild(11).gameObject;
        laser2 = gameObject.transform.GetChild(12).gameObject;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Gunship_Spawn");
        StartCoroutine(Shoot());
    }

    IEnumerator GoldenRatioAttack()
    {
        audioManager.Play("Gunship_GoldenRatio");

        while (orangeCounter <= 100 && pinkCounter <= 110)
        {
            orangeCounter++;
            pinkCounter++;

            yield return new WaitForSeconds(0.03f);                                                                         // Since Base Enemy begins shooting immediately, we want the bullet pool to have some time to 
                                                                                                                       //   initialize and create its objects before we call on it to grab bullets.
        Quaternion bulletRotation = Quaternion.identity;                                                               //   Not doing this leads to a NullReferenceException because the pooledObjects.Count does not yet 

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

            GameObject pBullet = PinkEnemyBulletPooler.current.GetPinkEnemyBullet();

            if (pBullet == null)
            {
                yield break;
            }

            pBullet.transform.position = megaCannon.position;                                          // Set the positon and rotation of the bullet
            pBullet.transform.rotation = bulletRotation;
            pBullet.GetComponent<EnemyBulletController>().SetEnemyType("gunship");                                        // This will tell the bullet controller script how the bullet will move once shot
            pBullet.GetComponent<EnemyBulletController>().SetEnemyCounter(pinkCounter, goldenAttack, 0f);
            pBullet.SetActive(true);                                                                                   // Activate the bullet
        }
        orangeCounter = 0;
        pinkCounter = 10;
        yield break;

    }

    IEnumerator GridAttack(Transform firePoint) 
    {
        float waitTime = Random.Range(0.6f, 2.3f);

        orangeCounter = 0;
        audioManager.Play("Gunship_Wall");

        while (orangeCounter <= 8)
        {
            
            orangeCounter++;

            yield return new WaitForSeconds(0.05f);                                                                    // Since Base Enemy begins shooting immediately, we want the bullet pool to have some time to 
                                                                                                                       //   initialize and create its objects before we call on it to grab bullets.
            Quaternion bulletRotation = Quaternion.identity;                                                               //   Not doing this leads to a NullReferenceException because the pooledObjects.Count does not yet 
                                                                                                                       //   exist in PinkEnemyBulletPooler.cs (the orange verison worked for some reason).
            int colorChoose = Random.Range(0, 2); // Ensures we don't have fully absorbable grid lines (of course there is a very small chance that still happens)

            if (colorChoose == 0) 
            {
                bulletIsOrange = true;
            }
            else
            {
                bulletIsOrange = false;
            }

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
        audioManager.Play("Gunship_Laser");
        yield return new WaitForSeconds(1.5f);  
        anim.SetBool("Charging_Laser", true);
        yield return new WaitForSeconds(.5f);       
        anim.SetBool("Charging_Laser", false); 

        laser1.GetComponent<Laser>().controlLasers(true); //Getting the component each time may be very inefficient!
        laser2.GetComponent<Laser>().controlLasers(true);

        orangeCounter = 0; //make sure we always start at 0

        int extraFastWall = Random.Range(1, 5); // we pick, at a random time, between when we shoot the second wall and the second to last wall, to send out an extra
                                                // fast wall without any holes and is a single color such that we force the player to play with the color switching mechanics
        int extraFastWallColor = Random.Range(0, 2); //To pick which color the fast wall will be

        while (orangeCounter <= 7) // We want to produce 7 total walls
        {
            yield return new WaitForSeconds(1f);                                                                    // Since Base Enemy begins shooting immediately, we want the bullet pool to have some time to 
                                                                                                                       //   initialize and create its objects before we call on it to grab bullets.
            Quaternion bulletRotation = Quaternion.identity;                                                               //   Not doing this leads to a NullReferenceException because the pooledObjects.Count does not yet 
                                                                                                                       //   exist in PinkEnemyBulletPooler.cs (the orange verison worked for some reason).
            float spacing = -22.5f;
            const float spacingInterval = 1.5f;
            const float bulletSpeedMultiplier = 1f;

            if (extraFastWall == orangeCounter) // this is the wall which we also add the fast wall!
            {
                audioManager.Play("Gunship_FastWall");
                for (int i = 0; i <= 44; i++) 
                {
                    if (extraFastWallColor == 0) // We will make the wall orange in this case
                    {
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
                        oBullet.GetComponent<EnemyBulletController>().SetEnemyCounter(orangeCounter, holeAttack, bulletSpeedMultiplier * 2f);
                        oBullet.SetActive(true);                                                                         // Activate the bullet
                        
                        spacing += spacingInterval;
                    }
                    else  //we will make the wall pink
                    {
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
                        pBullet.GetComponent<EnemyBulletController>().SetEnemyCounter(pinkCounter, holeAttack, bulletSpeedMultiplier * 2f);
                        pBullet.SetActive(true);

                        spacing += spacingInterval;
                    }
                }

                spacing = -22.5f; //reset our spacing value
            }

            int bulletGap = Random.Range(7, 14); // between 7 -> 14 we want to choose 2/3? bullets to not spawn

            audioManager.Play("Gunship_Wall");

            for (int i = 0; i <= 22; i++) { // repeating this 15 times to produce 30 total bullets that simulate a wall of bullets, each one spaced 1.5 apart in the X direction
                if (i != bulletGap && i != bulletGap + 1) 
                {
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
                    oBullet.GetComponent<EnemyBulletController>().SetEnemyCounter(orangeCounter, holeAttack, bulletSpeedMultiplier);
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
                    pBullet.GetComponent<EnemyBulletController>().SetEnemyCounter(pinkCounter, holeAttack, bulletSpeedMultiplier);
                    pBullet.SetActive(true);

                    spacing += spacingInterval;
                }
                else 
                {
                    spacing += spacingInterval * 2; // so we skip 4 bullets total
                }
            }

            orangeCounter++;
        }
        yield return new WaitForSeconds(bulletTravelTime);
        orangeCounter = 0;
        laser1.GetComponent<Laser>().controlLasers(false);
        laser2.GetComponent<Laser>().controlLasers(false);

        yield break;
    }

    IEnumerator EnemySpawnAttack()  //Note: Calling this attack successively causes too many enemies to appear on the screen, dont do that, the UX is ruined
    {
        int spawnSet = Random.Range(0, 4); // Depending on the random number, a different set of enemies will be spawned for the attack

        anim.SetBool("Spawning_Enemies", true);
        audioManager.Play("Gunship_GarageOpen");
        
        switch (spawnSet)
        {
            case 0: // 4 base enemies + 2 divers
            Instantiate(GameAssets.i.pBaseEnemy, garage1.position, Quaternion.identity);
            Instantiate(GameAssets.i.oDiverEnemy, garage2.position, Quaternion.identity);
            Instantiate(GameAssets.i.oBaseEnemy, garage2.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            Instantiate(GameAssets.i.pDiverEnemy, garage1.position, Quaternion.identity);
            Instantiate(GameAssets.i.oBaseEnemy, garage1.position, Quaternion.identity);
            Instantiate(GameAssets.i.pBaseEnemy, garage2.position, Quaternion.identity);
            break;

            case 1: // 4 divers + 2 base enemies
            Instantiate(GameAssets.i.pBaseEnemy, garage2.position, Quaternion.identity);
            Instantiate(GameAssets.i.pDiverEnemy, garage1.position, Quaternion.identity);
            Instantiate(GameAssets.i.oDiverEnemy, garage2.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            Instantiate(GameAssets.i.oBaseEnemy, garage1.position, Quaternion.identity);
            Instantiate(GameAssets.i.oDiverEnemy, garage1.position, Quaternion.identity);
            Instantiate(GameAssets.i.pDiverEnemy, garage2.position, Quaternion.identity);
            break;

            case 2: // 3 divers + 3 base enemies
            Instantiate(GameAssets.i.oBaseEnemy, garage1.position, Quaternion.identity);
            Instantiate(GameAssets.i.pDiverEnemy, garage1.position, Quaternion.identity);
            Instantiate(GameAssets.i.pBaseEnemy, garage2.position, Quaternion.identity);
            Instantiate(GameAssets.i.oDiverEnemy, garage2.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            Instantiate(GameAssets.i.oDiverEnemy, garage1.position, Quaternion.identity);
            Instantiate(GameAssets.i.pBaseEnemy, garage1.position, Quaternion.identity);
            break;

            case 3: // 6 divers
            Instantiate(GameAssets.i.pDiverEnemy, garage1.position, Quaternion.identity);
            Instantiate(GameAssets.i.oDiverEnemy, garage2.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            Instantiate(GameAssets.i.pDiverEnemy, garage2.position, Quaternion.identity);
            Instantiate(GameAssets.i.oDiverEnemy, garage1.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            Instantiate(GameAssets.i.pDiverEnemy, garage1.position, Quaternion.identity);
            Instantiate(GameAssets.i.oDiverEnemy, garage2.position, Quaternion.identity);
            break;
        }

        anim.SetBool("Spawning_Enemies", false);
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
        const float spawnTime = 3.5f;
        const float goldenRatioTotalTime = 3f;
        const float gridTotalTime = 2.3f;
        const float holeInTheWallTotalTime = 12.5f;
        const float enemySpawnTotalTime = 6f;

        int[] movesLeft = {0, 1, 2, 3};
        int movesLeftLen = 4;
        int move = movesLeft[Random.Range(0, movesLeftLen)];
        yield return new WaitForSeconds(0.1f);
        audioManager.Play("Gunship_Spawn");
        yield return new WaitForSeconds(0.1f);
        audioManager.Play("Gunship_Spawn");
        yield return new WaitForSeconds(spawnTime); // Gives the Gunship time to play its spawning animation
        
        while (movesLeftLen > 0) // this loops forever
        {
            switch (move)
            {
                case 0:
                    StartCoroutine(GoldenRatioAttack());
                    movesLeft = RemoveMove(move, movesLeftLen, movesLeft);
                    movesLeftLen--;
                    yield return new WaitForSeconds(goldenRatioTotalTime);
                    break;
                case 1:

                    switch (movesLeftLen) // this allows me to choose from 4 diff configurations of the grid attack
                    {
                        case 4: //shoots outermost ones and goes towards inside
                        StartCoroutine(GridAttack(firePoint1));
                        StartCoroutine(GridAttack(firePoint6));
                        yield return new WaitForSeconds(gridTotalTime / 2);
                        StartCoroutine(GridAttack(firePoint2));
                        StartCoroutine(GridAttack(firePoint5));
                        yield return new WaitForSeconds(gridTotalTime / 2);
                        StartCoroutine(GridAttack(firePoint3));
                        StartCoroutine(GridAttack(firePoint4));
                        break;

                        case 3: //shoots innermost ones and goes towards outside
                        StartCoroutine(GridAttack(firePoint3));
                        StartCoroutine(GridAttack(firePoint4));
                        yield return new WaitForSeconds(gridTotalTime / 2);
                        StartCoroutine(GridAttack(firePoint2));
                        StartCoroutine(GridAttack(firePoint5));
                        yield return new WaitForSeconds(gridTotalTime / 2);
                        StartCoroutine(GridAttack(firePoint1));
                        StartCoroutine(GridAttack(firePoint6));
                        break;

                        case 2: //shoots left to right
                        StartCoroutine(GridAttack(firePoint1));
                        StartCoroutine(GridAttack(firePoint2));
                        StartCoroutine(GridAttack(firePoint3));
                        StartCoroutine(GridAttack(firePoint4));
                        StartCoroutine(GridAttack(firePoint5));
                        StartCoroutine(GridAttack(firePoint6));
                        break;

                        case 1: //shoots right to left
                        StartCoroutine(GridAttack(firePoint6));
                        StartCoroutine(GridAttack(firePoint5));
                        StartCoroutine(GridAttack(firePoint4));
                        StartCoroutine(GridAttack(firePoint3));
                        StartCoroutine(GridAttack(firePoint2));
                        StartCoroutine(GridAttack(firePoint1));
                        break;
                    }
                    movesLeft = RemoveMove(move, movesLeftLen, movesLeft);
                    movesLeftLen--;
                    yield return new WaitForSeconds(gridTotalTime);
                    break;
                case 2:
                    StartCoroutine(HoleInTheWallAttack());
                    movesLeft = RemoveMove(move, movesLeftLen, movesLeft);
                    movesLeftLen--;
                    yield return new WaitForSeconds(holeInTheWallTotalTime);
                    break;
                case 3:
                    StartCoroutine(EnemySpawnAttack());
                    movesLeft = RemoveMove(move, movesLeftLen, movesLeft);
                    movesLeftLen--;
                    yield return new WaitForSeconds(enemySpawnTotalTime);
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