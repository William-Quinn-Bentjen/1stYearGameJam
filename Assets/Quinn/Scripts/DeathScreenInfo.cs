using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreenInfo : MonoBehaviour {
    public static float Score;
    public int deathSceneIndex;
	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this);
	}
    private void Start()
    {
        if (PointKeeper.instance != null)
        {
            PointKeeper.instance.onChange += UpdateScore;
        }
        else
        {
            Debug.Log("point keeper doesn't exist yet");
        }
    }
    public void UpdateScore(float score)
    {
        Score = score;
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == deathSceneIndex)
        {
            Text label = GameObject.FindGameObjectWithTag("ScoreLabel").GetComponent<Text>();
            label.text = "YOUR SCORE\n" + Score;
            Destroy(gameObject);
        }
    }
}
