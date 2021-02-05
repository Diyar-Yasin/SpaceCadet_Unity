using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BulletCollisions
//              Determines what happens to bullets that collide with enemies or the player.
//          This script is currently attached to all bullet types, orange/pink, and
//          enemy/player.
//
//          Credits: The code that implements gravitational forced was found here,
//          https://answers.unity.com/questions/596202/planetary-point-gravity-2d.html
//          
//          UNDER CONSTRUCTION: The gravitational pull that pulls enemy bullets which have
//          the same color as the player ship is still being implemented.
public class BulletCollisions : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public float maxGravity = 80.0f;

    void OnTriggerEnter2D(Collider2D other)                                                                            // As soon as a bullet hits something, we need to check what hit what. More detail below.
    {
        if (gameObject.tag == "O_Enemy_Bullet" || gameObject.tag == "P_Enemy_Bullet")                                  // If the object itself is an enemy bullet then we check if it hit the player, if it did
        {                                                                                                              //   then we need to check what the state of the player is. If the enemy bullet hits a
            if (other.tag == "Player")                                                                                 //   player and both objects have the same color, we want the player to "absorb" the bullets
            {                                                                                                          //   and take no damage. Otherwise the player needs to take damage and the bullet needs to be
                if (other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player"))                    //   deactivated.
                {
                    if (gameObject.tag == "O_Enemy_Bullet")
                    {
                        float dist = Vector3.Distance(gameObject.transform.position, transform.position);
                        Vector3 v = gameObject.transform.position - transform.position;
                        gameObject.GetComponent<Rigidbody2D>().AddForce(v.normalized * (1.0f - dist) * maxGravity);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (gameObject.tag == "P_Enemy_Bullet")
                    {
                        float dist = Vector3.Distance(gameObject.transform.position, transform.position);
                        Vector3 v = gameObject.transform.position - transform.position;
                        gameObject.GetComponent<Rigidbody2D>().AddForce(v.normalized * (1.0f - dist) * maxGravity);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
        else if (gameObject.tag == "P_Player_Bullet" || gameObject.tag == "O_Player_Bullet")                           // If the object itself is a player's bullet, then we check if it hit an enemy. If it did,
        {                                                                                                              //   regardless of color, we deactivate the player bullet.
            if (other.tag == "P_Enemy" || other.tag == "O_Enemy")
            {
                gameObject.SetActive(false);
            }
        }
    }
}
