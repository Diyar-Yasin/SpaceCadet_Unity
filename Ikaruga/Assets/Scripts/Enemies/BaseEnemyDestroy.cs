using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyDestroy : MonoBehaviour
{
    public Animator anim;
    private int health = 40;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(health);

        if (other.tag == "O_Player_Bullet")
        {
            if (gameObject.tag == "O_Base_Enemy")
            {
                // takes less dmg
                health -= 1;
            }
            else
            {
                // takes more dmg
                health -= 2;
            }
        }
        else if (other.tag == "P_Player_Bullet")
        {
            if (gameObject.tag == "O_Base_Enemy")
            {
                // takes more dmg
                health -= 2;
            }
            else
            {
                // take less dmg
                health -= 1;
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
