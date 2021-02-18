using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Score
//              This script keeps a running total of the score of the player and displays
//          it on the screen. It contains functions that add a predetermined amount of score
//          based on what is done by the player. For example, if a player destroys a base enemy
//          and earns 10 points, then the enemy destroy script would call a function in here that
//          would add 10 points to score.
//
//          Credits: https://www.youtube.com/watch?v=TAGZxRMloyU&ab_channel=Brackeys
//
//          UNDER CONSTRUCTION: As I add more enemies I will frequently have to add new functions.
public class Score : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public TextMesh scoreText;

    // PRIVATE
        // CONSTANTS
        private const int bulletWorth = 1;
        private const int baseEnemyWorth = 50;
        private const int diverEnemyWorth = 75;


    public void ScoreAdd_AbsorbedBullet()
    {
        scoreText.text = (int.Parse(scoreText.text) + bulletWorth).ToString();
    }

    public void ScoreAdd_DestroyedBaseEnemy()
    {
        scoreText.text = (int.Parse(scoreText.text) + baseEnemyWorth).ToString();
    }

    public void ScoreAdd_DestroyedDiverEnemy()
    {
        scoreText.text = (int.Parse(scoreText.text) + diverEnemyWorth).ToString();
    }
}
