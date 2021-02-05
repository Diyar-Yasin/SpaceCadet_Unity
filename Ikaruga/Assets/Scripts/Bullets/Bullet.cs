using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxGravity = 80.0f;
    // This C# script name could be improved by adding more detail
    // The gravitational code was found here: https://answers.unity.com/questions/596202/planetary-point-gravity-2d.html

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag == "O_Enemy_Bullet" || gameObject.tag == "P_Enemy_Bullet")
        {
            if (other.tag == "Player")
            {
                if (other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player"))
                {
                    // SHIP IS ORANGE
                    
                    if (gameObject.tag == "O_Enemy_Bullet")
                    {
                        // BULLET IS ORANGE, SO ABSORB IT

                        float dist = Vector3.Distance(gameObject.transform.position, transform.position);
                        Vector3 v = gameObject.transform.position - transform.position;
                        gameObject.GetComponent<Rigidbody2D>().AddForce(v.normalized * (1.0f - dist) * maxGravity);
                    }
                    else
                    {
                        // BULLET IS PINK, SO SET INACTIVE BULLET AND TAKE DAMAGE
                        gameObject.SetActive(false);
                    }
                }
                else
                {
                    // SHIP IS PINK

                    if (gameObject.tag == "P_Enemy_Bullet")
                    {
                        // BULLET IS PINK, SO ABSORB IT
                        float dist = Vector3.Distance(gameObject.transform.position, transform.position);
                        Vector3 v = gameObject.transform.position - transform.position;
                        gameObject.GetComponent<Rigidbody2D>().AddForce(v.normalized * (1.0f - dist) * maxGravity);
                    }
                    else
                    {
                        // BULLET IS ORANGE, SO SET INACTIVE BULLET AND TAKE DAMAGE
                        gameObject.SetActive(false);
                    }
                }
            }
        }
        else if (gameObject.tag == "P_Player_Bullet" || gameObject.tag == "O_Player_Bullet")
        {
            // If the player's bullets hit the enemy we want to set those bullets to inactive
            if (other.tag == "P_Enemy" || other.tag == "O_Enemy")
            {
                gameObject.SetActive(false);
            }
        }
    }
}
