using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunship_Destroy : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public Animator anim;

    // PRIVATE
        // CONSTANTS
        private const int gunshipEnemyWorth = 500;

        // VARIABLES
        private int health = 2000; 
        private bool alreadyDead = false;

    void OnTriggerEnter2D(Collider2D other)                                                                            // As soon as a BaseEnemy collides with something, we need to check if it collided with a player
    {                                                                                                                  //   bullet. If it did then we decrease the health of the enemy. Player bullets with color opposite
        if (other.tag == "O_Player_Bullet" || other.tag == "P_Player_Bullet")                                                                            //   to the enemy's color deal double damage.
        {
            health -= 1;
        }

        if (health <= 0 && !alreadyDead)                                                                                               // If the enemy's health drops to or below 0, we want to get rid of the enemy. We killed it!
        {
            alreadyDead = true;

            GameObject scoreText = GameObject.FindGameObjectWithTag("Score");                                          // We add the score worth of killing a base enemy before removing the enemy.
            Score score = scoreText.GetComponent<Score>();
            score.ScoreAdd_DestroyedGunshipEnemy();

            PointsPopup.Create(transform.position, gunshipEnemyWorth);

            StartCoroutine(Death());
            
        }
    }

    IEnumerator Death() 
    {
        anim.SetBool("Death", true);

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
    }
}
