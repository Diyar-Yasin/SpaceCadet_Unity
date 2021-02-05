using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// OrangePlayerBulletPooler
//              Creates a bullet pool that holds a list of orange player bullet gameObjects to be
//          reused by the player.
//
//          Credits: This code was taken from the following Youtube video,
//          https://www.youtube.com/watch?v=nf3gXt5m3Fc&ab_channel=BMo
public class OrangePlayerBulletPooler : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public static OrangePlayerBulletPooler current;
        public GameObject orangePlayerPooledBullet;
        public int pooledAmount;
        public bool willGrow;

    // PRIVATE
        // VARIABLES
        private List<GameObject> pooledObjects;

    private void Awake()
    {
        current = this;                                                                                                // Setting current = this allows us to reference this function from other scripts to fetch bullets
    }

    void Start()                                                                                                       // On start we create a new list for the pool of orange player bullets and instantiate pooledAmount
    {                                                                                                                  //   of bullets (which are inactive) and add them to our list.
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(orangePlayerPooledBullet);
            obj.SetActive(false);
            pooledObjects.Add(obj);

        }
    }

    public GameObject GetOrangePlayerBullet()                                                                          // This function gets used by outside scripts. We first check for a bullet that is inactive in our
    {                                                                                                                  //   pooledBullets list. If we find one, then we return it.
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (willGrow)                                                                                                  // If willGrow is set to true, from inspector, then since every bullet in our current list is
        {                                                                                                              //   already in use, we create a new bullet, add it to our list, and return it.
            GameObject obj = Instantiate(orangePlayerPooledBullet);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;                                                                                                   // If willGrow is false then we have to just return null.
    }
}
