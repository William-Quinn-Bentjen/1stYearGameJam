using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointKeeper : MonoBehaviour {

    public static PointKeeper instance;
    public delegate void OnChange(float value);
    public OnChange onChange;
    private float _points = 0;
    public float Points
    {
        get
        {
            return _points;
        }
        set
        {
            _points = value;
            if (onChange != null)
            {
                onChange(value);
            }
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("one too many pointkeeper get rid of one", gameObject);
        }
    }
}
