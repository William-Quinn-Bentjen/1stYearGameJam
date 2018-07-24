using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrailPool : MonoBehaviour
{
    public static BulletTrailPool instance;
    public ObjectPool pool;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("More than 1 bullet trail pool exists remove one", gameObject);
        }
    }
}
