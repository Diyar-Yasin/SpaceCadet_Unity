using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkPlayerBulletPooler : MonoBehaviour
{
    // This code was taken from: https://www.youtube.com/watch?v=nf3gXt5m3Fc&ab_channel=BMo

    public static PinkPlayerBulletPooler current;
    public GameObject pinkPlayerPooledBullet;
    public int pooledAmount;
    public bool willGrow;

    private List<GameObject> pooledObjects;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pinkPlayerPooledBullet);
            obj.SetActive(false);
            pooledObjects.Add(obj);

        }
    }

    public GameObject GetPinkPlayerBullet()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = Instantiate(pinkPlayerPooledBullet);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }
}
