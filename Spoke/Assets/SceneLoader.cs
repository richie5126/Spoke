using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	public string ResourceName = "ALMONATHAN";
	public Sprite FadeOutImage;
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
		GameObject blackCanvas = new GameObject ("fader");
		Canvas c = blackCanvas.AddComponent<Canvas> ();
		c.sortingOrder = -10000;
		GameObject imageobj = new GameObject ("fader");
		imageobj.transform.parent = blackCanvas.transform;
		imageobj.transform.localPosition = Vector3.zero;
		Image pic = imageobj.AddComponent<Image> ();
		pic.sprite = FadeOutImage;
		pic.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);

		pic.CrossFadeAlpha (1.0f, 1.0f, false);
		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadSceneAsync ("Mainn");
	}
	void Update () {
		
	}
}
