using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DiverEnemyMove
//              This script controls the movement of the Diver Enemy. It works in a similar
//          way to the BaseEnemyMove.cs script with some added changes to distinguish this
//          enemy from the base enemy.
//
//          CREDITS: https://www.youtube.com/watch?v=mKLp-2iseDc&ab_channel=KristerCederlund
//
//          UNDER CONTRUCTION: Angles are not working properly yet
public class DiverEnemyMove : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public float speed;
        public float diveForce;
        public float startWaitTime;
        public Transform[] moveSpots;

    // PRIVATE
        // VARIABLES
        private int currentWaypoint;
        private float waitTime;
        private Rigidbody2D rb;
        private bool isMoving;

    void Start()                                                                                                       // We always start at our first waypoint, 0.
    {
        isMoving = true;
        rb = GetComponent<Rigidbody2D>();
        waitTime = startWaitTime;
        currentWaypoint = 0;
    }

    void Update()                                                                                                      // We constantly move the enemy towards the currentWaypoint by its speed * time
    {
        if (isMoving)
        {
/*            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 targ = player.transform.position;
            targ.z = 0f;

            Vector3 diverPos = transform.position;
            targ.x = targ.x - diverPos.x;
            targ.y = targ.y - diverPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
*/

            transform.position = Vector2.MoveTowards(transform.position,
            moveSpots[currentWaypoint].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[moveSpots.Length - 1].position) < 0.2f)                     // Once the enemy reaches the last waypoint in the array (within 0.2 error range) we will,
            {

                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Vector3 targ = player.transform.position;
                targ.z = 0f;

                Vector3 diverPos = transform.position;
                targ.x = targ.x - diverPos.x;
                targ.y = targ.y - diverPos.y;

                float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

                StartCoroutine(Dive(targ.x, targ.y)); // point object at player location (only get it once, dont update, so it is dodgeable), play animation and add force in that direction 

                //isMoving = false;
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

    IEnumerator Dive(float xTarg, float yTarg)
    {
        

        yield return new WaitForSeconds(1.5f);

        rb.AddForce((new Vector2(xTarg, yTarg)) * diveForce, ForceMode2D.Impulse);
    }
}
