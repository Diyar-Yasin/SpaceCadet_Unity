using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerDamage
//              This script handles the player taking damage, including invincibility and audio
//          effects.
//
//          UNDER CONSTRUCTION: Audio for the hurt/death events have not yet been added. Also, I
//          may decide to handle Game Over in this script. Although it might be beneficial to have
//          a separate one that simultaneously deals with switching game scenes (if the player were
//          to quit to menu). Death (when lives <= 1) has yet to be added as well.
public class PlayerDamage : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public AudioSource deathSound;
        public GameObject deathEffect;
        public AudioSource hurtSound;
        public Renderer rend;
        public Animator anim;
        public int lives = 4;
        public GameObject healthObj;

    // PRIVATE
        // CONSTANTS
        private const float invincibilityTime = 0.25f;
        private const float totalInvincibilityTime = 0.4f;

        // VARIABLES
        private bool invincible;
        private bool invisible;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        invincible = false;
        invisible = false;

    }

    void OnTriggerEnter2D(Collider2D other)                                                                            // Every time the player collides with game objects we need to check if we hit an enemy bullet so
    {                                                                                                                  //   that damage can be applied and the player can be invincible for a couple seconds (and blinks
        if (!invincible)                                                                                               //   for a short time). If the player is invincible, we ignore all collisions.
        {
            if (other.tag == "O_Enemy_Bullet" &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Player"))                                                                         // We have to check what color the bullet is specifically because if the player is orange and is hit
            {                                                                                                          //   by the same colored bullet, then they take no damage.
                StartCoroutine(Invincibility());                                             // We are orange, have been hit by an orange enemy bullet and thus take no damage.

            }
            else if (other.tag == "P_Enemy_Bullet" &&
            anim.GetCurrentAnimatorStateInfo(0).IsName("Player"))
            {
                StartCoroutine(Invincibility());                                             // We are orange, have been hit by an pink enemy bullet so we take damage.

            }
            else if (other.tag == "P_Enemy" || other.tag == "O_Enemy")                                                 // If we touch enemies (regardless of their color or our color) we take damage. This is to
            {                                                                                                          //   discourage sitting inside large enemy models to avoid bullets but also adds realism.
                StartCoroutine(Invincibility());
            }
        }
    }

    IEnumerator Invincibility()
    {

        if (lives > 1)                                                                                                 // As long as we have more lives we can enter our invincible state, otherwise we die.
        {

            invincible = true;
            // hurtSound.Play();

            --lives;
            healthObj.GetComponent<Health>().UpdateHealth(lives);
            

            for (double i = 0; i < totalInvincibilityTime; i += 0.05)                                                  // We continuously enable and disable the renderer for the player while invincible to
            {                                                                                                          //   simulate blinking that helps the player visualize how long they are invincible.
                if (!invisible)                                                                                        // Disappear
                {
                    GetComponent<Renderer>().enabled = false;
                    invisible = true;
                }
                else                                                                                                   // Reappear
                {
                    GetComponent<Renderer>().enabled = true;
                    invisible = false;
                }

                yield return new WaitForSeconds(invincibilityTime);
            }

            GetComponent<Renderer>().enabled = true;                                                                   // To make sure we always end by having the player be visible we set to enabled again
            invincible = false;
            
        }
        else
        {
            Debug.Log("NO LIVES LEFT");
            Destroy(gameObject);
        }
    }
}
