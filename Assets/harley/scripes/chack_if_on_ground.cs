using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chack_if_on_ground : MonoBehaviour {
    private bool _isGrounded;
    private float airTime = 0;
    public bool isGrounded
    {
        get
        {
            return _isGrounded;
        }
        set
        {
            if (value != _isGrounded)
            {
                _isGrounded = value;
                if (groundedChange != null)
                {
                    groundedChange.Invoke(value);
                }
            }
        }
    }
    public delegate void Change(bool grounded);
    public Change groundedChange;
    public float hitdistance;
    public LayerMask layer;
    public float down;
    public float timeToReachTerminalVelocity = 4;
    public AnimationCurve velocityCurve;
    public player_movment player;
    public float offset = 0.5f;
	// Use this for initialization
	void Awake () {
        groundedChange += TerminalVelocityLogic;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateStates();
	}
    public void TerminalVelocityLogic(bool grounded)
    {
        if (grounded == true)
        {
            airTime = 0;
        }
    }
    public void UpdateStates()
    {
        if(isGrounded == true)
        {
            hitdistance = 0.35f;
        }
        else
        {
            airTime += Time.deltaTime;
            float curveEval = velocityCurve.Evaluate(Mathf.Clamp01(airTime / timeToReachTerminalVelocity));
            hitdistance = 0.15f;
            player.rm.AddForce(Vector3.down * curveEval * down);

        }
        if (Physics.Raycast(transform.position + (Vector3.down * offset), -transform.up, hitdistance, layer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
