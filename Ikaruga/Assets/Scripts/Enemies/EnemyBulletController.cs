using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyBulletController
//              This script controls the bullet patterns of all basic enemy types. It contains
//          a public function that allows us to set the type of bullet upon it being shot
//          by another script. All of the different types of movement based on if the enemy
//          is a base/diver/etc. are in distinct functions here.
//
//          Note: 
//          BASE ENEMY
//              Bullets fired by Base Enemies can take on 1 of 3 path styles depending on
//          where the bullet is on the game screen. Bullets on the left of the screen go right, 
//          bullets on the right of the screen go left, and bullets in the center go forward.
//
//          DIVER ENEMY
//          When the Diver dives across the screen, it shoots out a spread of very slow,
//          barely moving bullets that last longer than the average bullet, causing
//          temporary blockage for the player.
//
//          UNDER CONSTRUCTION: I plan to change the path of middle bullets to
//          something somewhat more interesting in the future.
//          I will also implement new functions for future enemies that are added to the game.
public class EnemyBulletController : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public float bulletForce;
        
    // PRIVATE
        // CONSTANTS
        private const int leftOfScreen = -10;
        private const int rightOfScreen = 10;
        private const float diverActiveTime = 4f;
        private const float baseActiveTime = 2f;

        // VARIABLES
        private Rigidbody2D rb;
        private string enemyType;
        private int counter;
        private int attack;
        private float waitTime;

    void Start()                                                                                                       // At the start we always give a bullet a type and then call the script for said enemy type.
    {
        rb = GetComponent<Rigidbody2D>();

        if (enemyType == null)
        {
            enemyType = "base";
        }

        switch (enemyType)
        {
            case "base":
                BaseController();
                break;

            case "diver":
                DiverController();
                break;
            case "gunship":
                GunshipController();
                break;
        }
    }

    public void SetEnemyType(string type)                                                                              // This function is used by functions such as DiverEnemyShoot and BaseEnemyShoot in order to
    {                                                                                                                  //   allow for creating bullets on a different script and object.
        enemyType = type;
    }

    public void SetEnemyCounter(int i, int j, float k)
    {
        counter = i;
        attack = j;
        waitTime = k;
    }

    private void BaseController()                                                                                      // Controls the movement of base enemy bullets as well as how long they last
    {
        if (gameObject.transform.position.x <= leftOfScreen)
        {
            rb.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
        }
        else if (gameObject.transform.position.x >= rightOfScreen)
        {
            rb.AddForce(-Vector2.right * bulletForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(-Vector2.up * bulletForce, ForceMode2D.Impulse);
        }

        Invoke("Disable", diverActiveTime);
    }

    private void DiverController()                                                                                     // Controls the movement of diver enemy bullets as well as how long they last
    {
        rb.AddForce(Vector2.up, ForceMode2D.Impulse);

        Invoke("Disable", diverActiveTime);
    }

    private void ChangeBulletPath()
    {
        if (gameObject.transform.position.x <= leftOfScreen)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
        }
        else if (gameObject.transform.position.x >= rightOfScreen)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(-Vector2.right * bulletForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
        }
    }
    private void GunshipController() {
        //const float halfPI = 180f;
        //const double phi = 1.61803398874989484820458683436;
        //float angle = halfPI;
        rb.velocity = Vector3.zero;

        switch (attack) 
        {
            case 0:
                const float golden = 0.30635f;
                float golden_t = golden * counter;
                
                float bulDirX = transform.position.x + Mathf.Exp(golden_t) * Mathf.Cos(counter);                                                                // Mathf.Sin(((angle + halfPI * counter) * Mathf.PI) / halfPI);
                float bulDirY = transform.position.y + Mathf.Exp(golden_t) * Mathf.Sin(counter);                                                                      // Mathf.Cos(((angle + halfPI * counter) * Mathf.PI) / halfPI);    
                Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                Vector2 bulDir = (bulMoveVector - transform.position).normalized; 

                rb.AddForce(bulDir * bulletForce, ForceMode2D.Impulse); 
                break;
            case 1:
                rb.AddForce(-Vector2.up * bulletForce, ForceMode2D.Impulse);
                Invoke("ChangeBulletPath", waitTime);
                break;
            case 2:
                rb.AddForce(-Vector2.up * bulletForce, ForceMode2D.Impulse);
                break;
            case 3:
                break;
        }
        Invoke("Disable", diverActiveTime);
    }

    private void OnEnable()                                                                                            // Each time we re-activate a bullet, we need to reset its type (as we might use a bullet for a base
    {                                                                                                                  //   enemy that gets destroyed and then called to be used for a diver enemy).
        if (rb != null) 
        {
            if (enemyType == null)
            {
                enemyType = "base";
            }

            switch (enemyType)
            {
                case "base":
                    BaseController();
                    break;

                case "diver":
                    DiverController();
                    break;
                
                case "gunship":
                    GunshipController();
                    break;
            }
        }                                                                                            
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()                                                                                           // This is added to ensure nothing goes wonky when we try to disable a bullet.
    {
        CancelInvoke();
    }
}
