using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private GameObject Gunship;

    void Start()
    {
        Gunship = GameObject.Find("Gunship");
        Gunship.SetActive(false);

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
                //PlayerDie();
                break;
        }
    }

    private IEnumerator LevelZero()
    {
        // Introduce the base enemy as well as its color variant
        EnemySpawn(spawnPt3, true, "base");

        yield return new WaitForSeconds(8f);

        EnemySpawn(spawnPt3, false, "base");

        yield return new WaitForSeconds(16f);

        // Spawn in multiple base enemies of the same color
        EnemySpawn(spawnPt1, true, "base");
        EnemySpawn(spawnPt3, true, "base");
        EnemySpawn(spawnPt5, true, "base");

        yield return new WaitForSeconds(16f);

        // Spawn in multiple base enemies of differing colors
        EnemySpawn(spawnPt1, true, "base");
        EnemySpawn(spawnPt2, false, "base");
        EnemySpawn(spawnPt3, false, "base");
        EnemySpawn(spawnPt4, false, "base");
        EnemySpawn(spawnPt5, true, "base");

        yield return new WaitForSeconds(16f);

        // Spawn in our first diver enemies (3 of them, because they may be killed too quickly before the player is able to see what they do), all the same color
        EnemySpawn(spawnPt1, true, "diver");
        EnemySpawn(spawnPt3, true, "diver");
        EnemySpawn(spawnPt5, true, "diver");

        yield return new WaitForSeconds(8f);

        // Spawn in 3 again but with different colors
        EnemySpawn(spawnPt1, false, "diver");
        EnemySpawn(spawnPt3, true, "diver");
        EnemySpawn(spawnPt5, false, "diver");

        yield return new WaitForSeconds(8f);

        // Spawn in divers and base enemies with a mix of colors
        EnemySpawn(spawnPt1, true, "base");
        EnemySpawn(spawnPt5, false, "base");
        EnemySpawn(spawnPt2, false, "diver");
        EnemySpawn(spawnPt4, true, "diver");

        yield return new WaitForSeconds(16f);

        // Spawn in one last hoarde of all types
        EnemySpawn(spawnPt1, false, "base");
        EnemySpawn(spawnPt2, true, "base");
        EnemySpawn(spawnPt3, false, "base");
        EnemySpawn(spawnPt4, true, "base");
        EnemySpawn(spawnPt5, false, "base");
        EnemySpawn(spawnPt1, true, "diver");
        EnemySpawn(spawnPt3, false, "diver");
        EnemySpawn(spawnPt5, true, "diver");

        yield return new WaitForSeconds(24f);

        // Bonus wave: Extra difficult
        EnemySpawn(spawnPt1, true, "base");
        EnemySpawn(spawnPt2, false, "base");
        EnemySpawn(spawnPt3, false, "base");
        EnemySpawn(spawnPt4, false, "base");
        EnemySpawn(spawnPt5, true, "base");
        
        yield return new WaitForSeconds(2f);

        EnemySpawn(spawnPt1, true, "base");
        EnemySpawn(spawnPt2, false, "base");
        EnemySpawn(spawnPt3, false, "base");
        EnemySpawn(spawnPt4, false, "base");
        EnemySpawn(spawnPt5, true, "base");

        yield return new WaitForSeconds(4f);

        EnemySpawn(spawnPt1, true, "diver");
        EnemySpawn(spawnPt3, false, "diver");
        EnemySpawn(spawnPt5, true, "diver");

        yield return new WaitForSeconds(2f);

        EnemySpawn(spawnPt1, true, "base");
        EnemySpawn(spawnPt2, false, "base");
        EnemySpawn(spawnPt3, false, "base");
        EnemySpawn(spawnPt4, false, "base");
        EnemySpawn(spawnPt5, true, "base");

        yield return new WaitForSeconds(20f);

        // Wait longer before spawning in our boss enemy!
        Gunship.SetActive(true);

    }

    public void GunshipSlain()
    {
        StartCoroutine(Victory());
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
