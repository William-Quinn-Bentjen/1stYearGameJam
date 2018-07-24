using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    public GameObject pooledObject;
    public Stack<GameObject> pool;

    public int pooledAmount;
	// SPAWNS PREFAB ADDS TO THE STACK AND ASSIGNS THE PARENT TO OURSELVES
	void Awake () {
        pool = new Stack<GameObject>();
        for(int i = 0; i < pooledAmount; i++)
        {
            GameObject spawnedObj = Instantiate(pooledObject);
            spawnedObj.transform.SetParent(transform);
            spawnedObj.GetComponent<PooledObject>().myPool = this;
            spawnedObj.SetActive(false);
            pool.Push(spawnedObj);
        }
	}
    public GameObject getObject()
    {
        if (pool.Count > 0)
        {
            GameObject poppedObj = pool.Pop();
            poppedObj.transform.SetParent(null);
            poppedObj.SetActive(true);
            return poppedObj;
        }
        else
        {
            pooledAmount *= 2;
            for(int i = 0; i< pooledAmount; i++)
            {
                GameObject spawnedObj = Instantiate(pooledObject);
                spawnedObj.transform.SetParent(transform);
                spawnedObj.GetComponent<PooledObject>().myPool = this;
                spawnedObj.SetActive(false);
                pool.Push(spawnedObj);
            }
            Debug.Log("Pool was empty added more stuff");
            GameObject poppedObj = pool.Pop();
            poppedObj.transform.SetParent(null);
            poppedObj.SetActive(true);
            return poppedObj;
        }
    }
    public GameObject getObject(Vector3 pos)
    {
        if (pool.Count > 0)
        {
            GameObject poppedObj = pool.Pop();
            poppedObj.transform.SetParent(null);
            poppedObj.transform.position = pos;
            poppedObj.SetActive(true);
            return poppedObj;
        }
        else
        {
            pooledAmount *= 2;
            for (int i = 0; i < pooledAmount; i++)
            {
                GameObject spawnedObj = Instantiate(pooledObject);
                spawnedObj.transform.SetParent(transform);
                spawnedObj.GetComponent<PooledObject>().myPool = this;
                spawnedObj.transform.position = pos;
                spawnedObj.SetActive(false);
                pool.Push(spawnedObj);
            }
            Debug.Log("Pool was empty added more stuff");
            GameObject poppedObj = pool.Pop();
            poppedObj.transform.SetParent(null);
            poppedObj.transform.position = pos;
            poppedObj.SetActive(true);
            return poppedObj;
        }
    }
}
