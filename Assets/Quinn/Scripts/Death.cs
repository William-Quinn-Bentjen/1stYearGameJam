using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {
    public int deathScene = 1;
	void OnCollisionEnter()
    {
        //restart
        SceneManager.LoadScene(deathScene);
    }
}
