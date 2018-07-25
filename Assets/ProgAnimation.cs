using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgAnimation : MonoBehaviour {
    public static ProgAnimation instance;
    public Vector3 reloadPos;
    private Vector3 startPos;
    public Transform breakAction;
    public Vector3 breakOpenRotation;
    private Vector3 breakStartRotation;
    public bool reloading;
    [Range(0,1)]
    public float downTime;
    [Range(0, 1)]
    public float upTime;
    public float timer;
    public float reloadTime;
    private void Start()
    {
        instance = this;
        startPos = transform.localPosition;
        breakStartRotation = breakAction.localRotation.eulerAngles;
    }
    private void Update()
    {
        if (reloading)
        {
            timer += Time.deltaTime;
            float tvalue = timer / reloadTime;
            if (tvalue < downTime)
            {
                //breakAction.rotation = Quaternion.Euler(Vector3.Lerp(breakStartRotation, breakOpenRotation, tvalue));
                transform.localPosition = Vector3.Lerp(startPos, reloadPos, tvalue);
            }
            else if (tvalue > upTime && tvalue < 1)
            {
                //breakAction.rotation = Quaternion.Euler(Vector3.Lerp(breakOpenRotation, breakStartRotation, tvalue));
                transform.localPosition = Vector3.Lerp(reloadPos, startPos, tvalue);
            }
            else if (tvalue > 1)
            {
                //breakAction.rotation = Quaternion.Euler(breakStartRotation);
                transform.localPosition = startPos;
                reloading = false;
                timer = 0;
            }
        }
    }
}
