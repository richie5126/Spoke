﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour {

    // Use this for initialization
    static SceneLoader mLoader;
    public string ResourceName = "testmap";
    public float SongSpeed = 1.0f;
    public static int maximumDifficultiesPossible = 5;
    public int OverallDifficulty = 3;

    public float resultingMultiplier = 1.0f;
    public Canvas faderCanvas;

    MaskableGraphic[] canvasElements;

    public int fadeOutElement = 0;
    public int loadingGraphic = 1;
    IEnumerator LoadResources()
    {
        ResourceRequest tmp = Resources.LoadAsync("");
        if (faderCanvas != null)
            faderCanvas.gameObject.SetActive(true);

        while (!tmp.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < canvasElements.Length; ++i)
        {
                canvasElements[i].CrossFadeAlpha(0.0f, 1.0f, false);
        }
    }
	void Awake () {
		DontDestroyOnLoad(this);
        
        if (mLoader != null) Destroy(gameObject);
        else mLoader = this;
        if (faderCanvas != null)
        {
            canvasElements = faderCanvas.GetComponentsInChildren<MaskableGraphic>();
        }
        StartCoroutine(LoadResources());


    }
    public void SetResourceName(string pName) { ResourceName = pName; }

    public void SetSongSpeed(float val) { SongSpeed = val;  }
    public void SetDifficulty(int val) { OverallDifficulty = val;  }

	public void LoadLevel(string pName = "Mainn")
	{
		StartCoroutine (LoadGame(pName));
	}
	// Update is called once per frame
	public IEnumerator LoadGame(string pName)
    {
        Time.timeScale = 1;
        canvasElements[fadeOutElement].raycastTarget = true;
        foreach (MaskableGraphic element in canvasElements)
        {
			element.CrossFadeAlpha (1.0f, 1.0f, true);
		}
		
		yield return new WaitForSeconds (2.0f);
		AsyncOperation tmp = SceneManager.LoadSceneAsync (pName);

        while (!tmp.isDone)
            yield return new WaitForEndOfFrame();

        canvasElements[fadeOutElement].raycastTarget = false;
        foreach (MaskableGraphic element in canvasElements)
        {
            element.CrossFadeAlpha(0.0f, 1.0f, true);
        }
        ButtonManager tmp2 = FindObjectOfType<ButtonManager>();
        TitleButton tmp3 = FindObjectOfType<TitleButton>();
        if (tmp2 != null)
        {
            tmp2.SwapToMenuWithIndex(ButtonManager.activeMenu);
            if (tmp3 != null)
                tmp3.LerpMove();
        }


    }
	void Update () {
        resultingMultiplier = SongSpeed * (0.7f + ((0.1f) * OverallDifficulty));
        if (loadingGraphic < canvasElements.Length) canvasElements[loadingGraphic].transform.Rotate(0.0f, 0.0f, -135.0f * Time.deltaTime);
	}
}
