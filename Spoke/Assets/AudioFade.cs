using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour {

	// Use this for initialization
    public IEnumerator FadeOutAudio(AudioSource source, float duration)
    {
        if (duration == 0.0f) source.volume = 0;
        else while(source.volume > 0)
        {
            source.volume -= Time.deltaTime / duration;
            yield return new WaitForEndOfFrame();
        }
    }
    public void FadeSound(AudioSource source)
    {
        StartCoroutine(FadeOutAudio(source, 1.0f));
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
