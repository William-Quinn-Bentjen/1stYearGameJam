using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool alive = false;
    public float LifeTime = 5;
    public float LifeTimer = 0;
    public PooledObject pooledObj;
    void OnTriggerEnter(Collider other)
    {
        alive = false;
        ReturnToPool();
    }
    private void OnCollisionEnter(Collision collision)
    {
        alive = false;
        ReturnToPool();
    }

    // Update is called once per frame
    void Update()
    {
        LifeTimer += Time.deltaTime;
        if (LifeTimer > LifeTime && alive)
        {
            ReturnToPool();
        }
    }
    void ReturnToPool()
    {
        alive = false;
        pooledObj.ReturnToPool();
        LifeTimer = 0;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //gameObject.GetComponent<TrailRenderer>().enabled = false;
    }
}