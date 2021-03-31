using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameManager
//              This script controlls the spawning of enemies, controlling when enemies spawn,
//          where they spawn, as well as how many of them spawn.
public class GameManager : MonoBehaviour
{
    // PUBLIC
        // CONSTANTS

        // VARIABLES
        public Transform spawnPt1;
        public Transform spawnPt2;
        public Transform spawnPt3;
        public Transform spawnPt4;
        public Transform spawnPt5;

    // PRIVATE
        // CONSTANTS
        
        // VARIABLES
        private int currentLevel;

    void Start()
    {
        currentLevel = 0;
        LevelSelect();
    }

    private void EnemySpawn(Transform spawnPt, bool isPink, string type)
    {
        Vector3 position = spawnPt.position;

        switch (type)
        {
            case "base":
                if (isPink)
                {
                    Instantiate(GameAssets.i.pBaseEnemy, position, Quaternion.identity);
                }
                else
                {
                    Instantiate(GameAssets.i.oBaseEnemy, position, Quaternion.identity);
                }
                break;
            case "diver":
                if (isPink)
                {
                    Instantiate(GameAssets.i.pDiverEnemy, position, Quaternion.identity);
                }
                else
                {
                    Instantiate(GameAssets.i.oDiverEnemy, position, Quaternion.identity);
                }
                break;
            default:
                Debug.Log("INVALID ENEMY TYPE INPUT");
                break;
        }
    }

    private void LevelSelect()
    {
        switch (currentLevel)
        {
            case 0:
                StartCoroutine(LevelZero());
                break;

            default:
                PlayerDie();
                break;
        }
    }

    private IEnumerator LevelZero()
    {
        EnemySpawn(spawnPt3, true, "base");

        yield return new WaitForSeconds(1f);

        EnemySpawn(spawnPt2, false, "base");
        EnemySpawn(spawnPt4, true, "base");

        yield return new WaitForSeconds(1.5f);

        EnemySpawn(spawnPt3, true, "base");
        EnemySpawn(spawnPt2, false, "base");
        EnemySpawn(spawnPt4, true, "base");

        yield return new WaitForSeconds(0.8f);

        EnemySpawn(spawnPt1, true, "diver");
        EnemySpawn(spawnPt5, false, "diver");

        yield return new WaitForSeconds(3f);

        currentLevel++;

        LevelSelect();
    }

    private void PlayerDie()
    {
        Debug.Log("YOU DIED");
    }
}
