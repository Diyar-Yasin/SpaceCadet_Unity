using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BaseEnemyMove
//              Controls the movement of BaseEnemy. It works by using an array of empty gameObjects
//          that act as waypoint markers for the enemy to move to. So an enemy will move from an
//          arbitrary list of waypoints starting at waypoint 1 -> n (where n is some real number).
//
//          Credits: This code was learnt from,
//          https://www.youtube.com/watch?v=8eWbSN2T8TE&ab_channel=Blackthornprod
//
//          UNDER CONSTRUCTION: Here I may add pooling so destroying the enemy will have to change.
public class BaseEnemyMove : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public float speed;
        public float startWaitTime;
        

    // PRIVATE
        // VARIABLES
        private int currentWaypoint;
        private float waitTime;
        private Transform[] moveSpots;

    void Start()                                                                                                       // We always start at our first waypoint, 0.
    {
        
        waitTime = startWaitTime;
        currentWaypoint = 0;

        GameObject base1 = GameObject.Find("Base_1");
        GameObject base2 = GameObject.Find("Base_2");
        GameObject base3 = GameObject.Find("Base_3");
        GameObject base4 = GameObject.Find("Base_4");
        GameObject base5 = GameObject.Find("Base_5");

        int moveChoice = Random.Range(0, 5);

        switch (moveChoice)
        {
            case 0:
                moveSpots = new Transform[2];
                moveSpots[0] = base1.transform.GetChild(0).transform;
                moveSpots[1] = base1.transform.GetChild(1).transform;
                break;
            case 1:
                moveSpots = new Transform[2];
                moveSpots[0] = base2.transform.GetChild(0).transform;
                moveSpots[1] = base2.transform.GetChild(1).transform;
                break;
            case 2:
                moveSpots = new Transform[3];
                moveSpots[0] = base3.transform.GetChild(0).transform;
                moveSpots[1] = base3.transform.GetChild(1).transform;
                moveSpots[2] = base3.transform.GetChild(2).transform;
                break;
            case 3:
                moveSpots = new Transform[2];
                moveSpots[0] = base4.transform.GetChild(0).transform;
                moveSpots[1] = base4.transform.GetChild(1).transform;
                break;
            case 4:
                moveSpots = new Transform[2];
                moveSpots[0] = base5.transform.GetChild(0).transform;
                moveSpots[1] = base5.transform.GetChild(1).transform;
                break;
            default:
                break;
        }
    }

    void Update()                                                                                                      // We constantly move the enemy towards the currentWaypoint by its speed * time
    {
        
        this.transform.position = Vector2.MoveTowards(this.transform.position, 
            moveSpots[currentWaypoint].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[moveSpots.Length - 1].position) < 0.2f)                     // Once the enemy reaches the last waypoint in the array (within 0.2 error range) we destroy the
        {                                                                                                              //   enemy. The final waypoint will always be at the bottom of the screen in this case (since at
            Destroy(this.gameObject);                                                                                       //   that point the player does not care about said enemy).
        }
        else if (Vector2.Distance(transform.position, moveSpots[currentWaypoint].position) < 0.2f)                     // Once the enemy reaches the next waypoint in the array (within 0.2 error range) we wait for the
        {                                                                                                              //   preset waitTime. Then once we have waited the time, we set a new waypoint, reset the waitTime
            if (waitTime <= 0)                                                                                         //   and continue moving to our next waypoint.
            {
                currentWaypoint++;
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
