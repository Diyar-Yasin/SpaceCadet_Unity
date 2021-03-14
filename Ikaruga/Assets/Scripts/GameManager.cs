using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // PUBLIC
        // CONSTANTS

        // VARIABLES

    // PRIVATE
        // CONSTANTS
        
        // VARIABLES

    void Start()
    {
        int currentLevel = 0;
        LevelSelect(currentLevel);
    }

    private void LevelSelect(int currentLevel)
    {
        switch (currentLevel)
        {
            case 0:
                LevelZero();
                break;

            default:
                break;

        }
    }

    private void LevelZero()
    {

    }
}
