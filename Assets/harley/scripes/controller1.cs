using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
public class controller1 : MonoBehaviour {
    public PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    public float horizontal;
    public float vertical;
    public float horizontalcamra;
    public float verticalcamra;
    public bool contrler1;
    public bool contrler2;
    public float leftrun;
    public bool firegun;
    public float rightrun;
    IEnumerator startviprat()
    {
        GamePad.SetVibration(playerIndex, leftrun, rightrun);
        yield return new WaitForSeconds(.4f);
        GamePad.SetVibration(playerIndex, 0, 0);
    }
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);
        if (contrler1 == true)
        {
            horizontal = state.ThumbSticks.Left.X;
            vertical = state.ThumbSticks.Left.Y;
            horizontalcamra = state.ThumbSticks.Right.X;
            verticalcamra = state.ThumbSticks.Right.Y;
            if (prevState.Triggers.Right <= 0.2f && state.Triggers.Right >= .5f /*&& reload == false*/)
            {
                StartCoroutine(startviprat());
                firegun = true;
            }
            else
            {
                firegun = false;
            }
            }
    }
}
