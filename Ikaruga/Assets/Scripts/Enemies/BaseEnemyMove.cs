using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyMove : MonoBehaviour
{
    // https://www.youtube.com/watch?v=8eWbSN2T8TE&ab_channel=Blackthornprod

    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int n;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        n = 0;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[n].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[moveSpots.Length - 1].position) < 0.2f)
        {
            Destroy(gameObject);
        }
        else if (Vector2.Distance(transform.position, moveSpots[n].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                n++;
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        
    }
}
