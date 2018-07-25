using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {
    public bool godMode = false;
    public int deathScene = 1;
	void OnCollisionEnter()
    {
        if (!godMode)
        {
            //restart
            SceneManager.LoadScene(deathScene);
        }
    }
}
