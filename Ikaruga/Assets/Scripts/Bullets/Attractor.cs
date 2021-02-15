using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attractor
//				This script adds gravitational attraction to bullets in the direction of the
//			player when the bullets are both within a certain range as well as the same color.
//
//			Credits: https://www.youtube.com/watch?v=Ouu3D_VHx9o&ab_channel=Brackeys
public class Attractor : MonoBehaviour
{
	// PUBLIC
		// VARIABLES
		public Rigidbody2D rb;

	// PRIVATE
		// CONSTANTS
		const float G = 66.74f;																	
		const float absorptionRange = 2.5f;

	void FixedUpdate()
	{
		Attract();
	}

	void Attract()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

		Vector3 direction = rb.position - playerRb.position;
		float distance = direction.magnitude;

		if (distance <= absorptionRange)                                                                               // We only want the bullets to be "absorbed" when they are close to the player
			return;

		PlayerShoot playerShoot = player.GetComponent<PlayerShoot>();
		
		if (playerShoot.isPink && this.tag == "O_Enemy_Bullet")														   // If the player and bullet are opposite colors, we should not attract the bullet to player
			return;

		if (!playerShoot.isPink && this.tag == "P_Enemy_Bullet")
			return;

		float forceMagnitude = G * (rb.mass * playerRb.mass) / Mathf.Pow(distance, 2);
		Vector3 force = -direction.normalized * forceMagnitude;

		this.rb.AddForce(force);																					   // We only add this force of gravitation to the bullet.
	}

}