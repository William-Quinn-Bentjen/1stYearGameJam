using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnStart : MonoBehaviour {
    public BreakableObject breakable;
	// Use this for initialization
	void Start () {
        breakable.BreakObject();
	}

    private void Reset()
    {
        breakable = GetComponent<BreakableObject>();
    }
}
