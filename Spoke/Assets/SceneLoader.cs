using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour {

    // Use this for initialization
    static SceneLoader mLoader;
    public string mainMenu = "Menu";
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
        {
            canvasElements = faderCanvas.GetComponentsInChildren<MaskableGraphic>();
            faderCanvas.gameObject.SetActive(true);
        }

        while (!tmp.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        var tmp2 = SceneManager.LoadSceneAsync(mainMenu);
        while(!tmp2.isDone)
            yield return new WaitForEndOfFrame();

        for (int i = 0; i < canvasElements.Length; ++i)
        {
                canvasElements[i].CrossFadeAlpha(0.0f, 1.0f, false);
        }
    }
	void OnEnable () {
		DontDestroyOnLoad(this);
        
        if (mLoader != null) Destroy(gameObject);
        else mLoader = this;
        StartCoroutine(LoadResources());
        StartCoroutine(RotateTimeIndependent());
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
    public IEnumerator RotateTimeIndependent()
    {
        while(true)
        {
            if (loadingGraphic < canvasElements.Length) canvasElements[loadingGraphic].transform.Rotate(0.0f, 0.0f, -135.0f * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
	void Update () {
        resultingMultiplier = SongSpeed * (0.7f + ((0.1f) * OverallDifficulty));
	}
}
