using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLabel : MonoBehaviour {
    public Text label;
    private void Start()
    {
        PointKeeper.instance.onChange += UpdateLabel;
        label.text = "Score: " + PointKeeper.instance.Points;
    }
    public void UpdateLabel(float value)
    {
        label.text = "Score: " + value;
    }

}
