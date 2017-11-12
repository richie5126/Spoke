using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour {

    // Use this for initialization
    AudioSource mAudio;
    public IEnumerator FadeOutAudioOneShot(AudioSource source, float duration)
    {
        if (duration == 0.0f) source.volume = 0;
        else while(source.volume > 0)
        {
            source.volume -= Time.deltaTime / duration;
            yield return new WaitForEndOfFrame();
        }
        source.Stop();
    }
    public IEnumerator FadeOutAudio(AudioSource source, float duration)
    {
        if (duration == 0.0f) source.volume = 0;
        else while (source.volume > 0)
            {
                source.volume -= Time.deltaTime / duration;
                yield return new WaitForEndOfFrame();
            }
    }

    public IEnumerator FadeInAudio(AudioSource source, float duration, AudioClip clip = null)
    {
        if (clip == null) yield return new WaitForEndOfFrame();

        if(!source.isPlaying)
        source.Play();

        if (duration == 0.0f) source.volume = 1f;
        else while (source.volume < 1.0f)
            {
                source.volume += Time.deltaTime / duration;
                yield return new WaitForEndOfFrame();
            }
    }
    private Coroutine coroutine;
    public void FadeSound(AudioSource source)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(FadeOutAudio(source, 1.0f));
    }
    public void FadeSoundOneShot(AudioSource source)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(FadeOutAudioOneShot(source, 1.0f));
    }
    public void FadeInSound(AudioSource source)
    {

        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(FadeInAudio(source, 1.0f));
    }

    public void FadeInSoundOneShot(AudioClip clipToUse)
    {
        if (mAudio == null) return;
        mAudio.clip = clipToUse;
        mAudio.time = mAudio.clip.length / 2.0f;

        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(FadeInAudio(mAudio, 1.0f, clipToUse));
    }
    void Start () {
        mAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
