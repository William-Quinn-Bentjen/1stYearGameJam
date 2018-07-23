using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadInicator : MonoBehaviour {
    public static ReloadInicator instance;
    public RectTransform emptyBar;
    public RectTransform fullBar;
    private float barLength;
    private float reloadTimer;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("two reload indicators detected please remove one", gameObject);
        }
        else
        {
            instance = this;
            //onChange += OnChange;
            barLength = emptyBar.sizeDelta.x;
            emptyBar.gameObject.SetActive(false);
            fullBar.gameObject.SetActive(false);
            //StartReload(3);
        }
    }
    public void StartReload(float reloadTime)
    {
        reloadTimer = 0;
        emptyBar.gameObject.SetActive(true);
        fullBar.gameObject.SetActive(true);
        StartCoroutine(Change(reloadTime));
    }
    public void StopReload()
    {
        StopAllCoroutines();
        emptyBar.gameObject.SetActive(false);
        fullBar.gameObject.SetActive(false);
    }
    private IEnumerator Change(float value)
    {
        float barSize;
        while (reloadTimer < value)
        {
            Debug.Log("UI TIMER =" + reloadTimer);
            barSize = reloadTimer / value;
            fullBar.sizeDelta = new Vector2(barSize * barLength, fullBar.sizeDelta.y);
            reloadTimer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        emptyBar.gameObject.SetActive(false);
        fullBar.gameObject.SetActive(false);
        yield return null;
    }
}
