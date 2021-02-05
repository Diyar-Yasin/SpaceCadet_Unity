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
        public Transform[] moveSpots;

    // PRIVATE
        // VARIABLES
        private int currentWaypoint;
        private float waitTime;

    void Start()                                                                                                       // We always start at our first waypoint, 0.
    {
        waitTime = startWaitTime;
        currentWaypoint = 0;
    }

    void Update()                                                                                                      // We constantly move the enemy towards the currentWaypoint by its speed * time
    {
        transform.position = Vector2.MoveTowards(transform.position, 
            moveSpots[currentWaypoint].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[moveSpots.Length - 1].position) < 0.2f)                     // Once the enemy reaches the last waypoint in the array (within 0.2 error range) we destroy the
        {                                                                                                              //   enemy. The final waypoint will always be at the bottom of the screen in this case (since at
            Destroy(gameObject);                                                                                       //   that point the player does not care about said enemy).
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
