using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameAssets
//              This script allows you to access instances of assets from any other script.
//
//          CREDITS: https://www.youtube.com/watch?v=iD1_JczQcFY&ab_channel=CodeMonkey
public class GameAssets : MonoBehaviour
{

    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            } 

            return _i;
        }
    }

    public Transform pointsPopup;

    public AudioClip bulletShot;

    public GameObject pBaseEnemy;
    public GameObject oBaseEnemy;
    public GameObject pDiverEnemy;
    public GameObject oDiverEnemy;

    public Transform base1Move;
    public Transform base2Move;
    public Transform base3Move;
}
