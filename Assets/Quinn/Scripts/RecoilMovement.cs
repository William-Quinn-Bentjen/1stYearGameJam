using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilMovement : MonoBehaviour {
    public static RecoilMovement instance;
    public Rigidbody rb;
    public float amount;
    public ForceMode forceMode;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one recoil scrit running remove one", gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void Move(Vector3 shotDirection)
    {
        rb.AddForce(shotDirection.normalized * -1 * amount, forceMode);
    }
    public void Reset()
    {
        rb = GetComponent<Rigidbody>();
    }
}
