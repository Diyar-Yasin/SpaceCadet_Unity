using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    // The audio for hurt/death events have not yet been implemented
    public AudioSource deathSound;
    public GameObject deathEffect;
    public AudioSource hurtSound;

    private int lives = 5;

    private bool invincible;
    private bool invisible;
    private float invincibilityTime = 0.25f;
    private float totalInvincibilityTime = 0.4f;
    public Renderer rend;
    public Animator anim;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        invincible = false;
        invisible = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!invincible)
        {
            if (other.tag == "O_Enemy_Bullet")
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player"))
                {
                    // We are orange, have been hit by an orange enemy bullet and thus take no damage.
                }
                else
                {
                    // We are pink, have been hit by a pink enemy bullet so we take damage.
                    StartCoroutine(Invincibility());
                }
            }
            else if (other.tag == "P_Enemy_Bullet")
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player"))
                {
                    // We are orange, have been hit by an pink enemy bullet so we take damage.
                    StartCoroutine(Invincibility());
                }
                else
                {
                    // We are pink, have been hit by a pink enemy bullet and thus take no damage.
                }
            }
        }
    }

    IEnumerator Invincibility()
    {
        if (lives > 1)
        {

            invincible = true;
            // hurtSound.Play();

            lives--;

            for (double i = 0; i < totalInvincibilityTime; i += 0.05)
            {
                if (!invisible)
                {
                    // Disappear
                    GetComponent<Renderer>().enabled = false;
                    invisible = true;
                }
                else
                {
                    // Reappear
                    GetComponent<Renderer>().enabled = true;
                    invisible = false;
                }

                yield return new WaitForSeconds(invincibilityTime);
            }

            GetComponent<Renderer>().enabled = true;
            invincible = false;
        }
        else
        {
            Debug.Log("NO LIVES LEFT");
            Destroy(gameObject);
        }
    }
}
