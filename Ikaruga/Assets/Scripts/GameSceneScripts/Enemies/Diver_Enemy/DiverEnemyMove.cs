using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DiverEnemyMove
//              This script controls the movement of the Diver Enemy. It works in a similar
//          way to the BaseEnemyMove.cs script with some added changes to distinguish this
//          enemy from the base enemy.
//
//          CREDITS: https://www.youtube.com/watch?v=mKLp-2iseDc&ab_channel=KristerCederlund
//          and      https://www.youtube.com/watch?v=4Wh22ynlLyk&ab_channel=PressStart
//
//          UNDER CONTRUCTION: Replace destroy Diver with SetActive(false) after we add enemy
//          pooling.
public class DiverEnemyMove : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public float speed;
        public float diveForce;
        public float startWaitTime;
        

    // PRIVATE
        // CONSTANTS
        private const int piOver2 = 90;

        // VARIABLES
        private Transform player;
        private int currentWaypoint;
        private float waitTime;
        private Rigidbody2D rb;
        private bool isMoving;
        private Animator anim;
        private Transform[] moveSpots;
        private AudioManager audioManager;

    void Start()                                                                                                       // We always start at our first waypoint, 0.
    {
        audioManager = FindObjectOfType<AudioManager>();
        moveSpots = new Transform[1];
        isMoving = true;
        player = GameObject.FindWithTag("Player").transform;
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        waitTime = startWaitTime;
        currentWaypoint = 0;

        GameObject diver1 = GameObject.Find("Diver_1");
        GameObject diver2 = GameObject.Find("Diver_2");

        int moveChoice = Random.Range(0, 2);

        switch (moveChoice)
        {
            case 0:
                moveSpots = new Transform[1];
                moveSpots[0] = diver1.transform.GetChild(0).transform;
                break;
            case 1:
                moveSpots = new Transform[1];
                moveSpots[0] = diver2.transform.GetChild(0).transform;
                break;
            default:
                break;
        }
    }

    void Update()                                                                                                      // We constantly move the enemy towards the currentWaypoint by its speed * time
    {
        if (isMoving)                                                                                                  // As long as we are in the isMoving state (the only other state is diving, where we want to freeze
        {                                                                                                              //   the direction that the Diver will Dive in.
            this.transform.position = Vector2.MoveTowards(this.transform.position,                                               // This block constantly ensures the Diver points at the player (signifying to the player that it is
            moveSpots[currentWaypoint].position, speed * Time.deltaTime);                                              //   targeting them.

            Vector3 direction = player.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + piOver2;                             // Note: We add Pi/2 to the angle to account for the original Diver sprite being oriented backwards.
            rb.rotation = angle;

            if (Vector2.Distance(this.transform.position, moveSpots[moveSpots.Length - 1].position) < 0.2f)                 // Once the enemy reaches the last waypoint in the array (within 0.2 error range) we will stop
            {                                                                                                          //   re-pointing at the player and launch towards the last calculated direction in 1.5 seconds.
                isMoving = false;
                StartCoroutine(Dive(direction.x, direction.y));                                                        // Point object at player location (only get it once, dont update, so it is dodgeable), play 
            }                                                                                                          //   animation and add force in that direction.
            else if (Vector2.Distance(this.transform.position, moveSpots[currentWaypoint].position) < 0.2f)                 // Once the enemy reaches the next waypoint in the array (within 0.2 error range) we wait for the
            {                                                                                                          //   preset waitTime. Then once we have waited the time, we set a new waypoint, reset the waitTime
                if (waitTime <= 0)                                                                                     //   and continue moving to our next waypoint.
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

    IEnumerator Dive(float xTarg, float yTarg)                                                                         // This IEnumerator waits 0.6 seconds, plays the Diving animation and then waits another 0.8 seconds
    {                                                                                                                  //   before launching. Then waits 1 second before removing the object.
        yield return new WaitForSeconds(0.6f);

        anim.SetBool("isDiving", true);
        audioManager.Play("Diver_Alert");

        yield return new WaitForSeconds(0.8f);

        rb.AddForce((new Vector2(xTarg, yTarg)).normalized * diveForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);
    }
}
