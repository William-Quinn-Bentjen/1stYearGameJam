﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointKeeper : MonoBehaviour {

    public static PointKeeper instance;
    public delegate void OnChange(float value);
    OnChange onChange;
    private float _points = 0;
    private void Start()
    {
        onChange += TestDel;
    }
    public float Points
    {
        get
        {
            return _points;
        }
        set
        {
            _points = value;
            onChange(value);
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
    public void Update()
    {
        Debug.Log("POINTS =" + Points);
    }
    public void TestDel(float value)
    {
        Debug.Log("TEST" + value);
    }
}
