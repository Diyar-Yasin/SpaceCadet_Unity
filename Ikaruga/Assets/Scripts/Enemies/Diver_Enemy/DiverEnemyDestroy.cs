using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DiverEnemyDestroy
//              This program handles the health and destruction of the DiverEnemy.
//          
//          UNDER CONSTRUCTION: I may add a pool system here so that I can reuse enemies
//          as opposed to constantly having to instantiate and destroy enemies.
public class DiverEnemyDestroy : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public Animator anim;

    // PRIVATE
        // VARIABLES
        private int health = 10;

    void OnTriggerEnter2D(Collider2D other)                                                                            // As soon as a DiverEnemy collides with something, we need to check if it collided with a player
    {                                                                                                                  //   bullet. If it did then we decrease the health of the enemy. Player bullets with color opposite
        if (other.tag == "O_Player_Bullet")                                                                            //   to the enemy's color deal double damage.
        {
            if (gameObject.tag == "O_Enemy")
            {
                health -= 1;
            }
            else
            {
                health -= 2;
            }
        }
        else if (other.tag == "P_Player_Bullet")
        {
            if (gameObject.tag == "O_Enemy")
            {
                health -= 2;
            }
            else
            {
                health -= 1;
            }
        }

        if (health <= 0)                                                                                               // If the enemy's health drops to or below 0, we want to get rid of the enemy. We killed it!
        {
            GameObject scoreText = GameObject.FindGameObjectWithTag("Score");                                          // We add the score worth of killing a diver enemy before removing the enemy.
            Score score = scoreText.GetComponent<Score>();
            score.ScoreAdd_DestroyedDiverEnemy();

            Destroy(gameObject);
        }
    }
}
