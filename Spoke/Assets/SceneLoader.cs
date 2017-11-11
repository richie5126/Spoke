using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	public string ResourceName = "ALMONATHAN";
	public Sprite FadeOutImage;
	public Image imageToFade;
	void Start () {
		DontDestroyOnLoad(this);
	}
	public void LoadLevel()
	{
		StartCoroutine (LoadGame());
	}
	// Update is called once per frame
	public IEnumerator LoadGame()
	{
		if (imageToFade != null) {
			imageToFade.CrossFadeAlpha (1.0f, 1.0f, false);
			Debug.Log ("Fading");
		}
		
		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadSceneAsync ("Mainn");
	}
	void Update () {
		
	}
}
