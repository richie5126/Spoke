using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

    // Use this for initialization
    public KeyCode[] ChannelsInput;
	public AudioClip[] hitsounds;
    public Transform radiusMarker;
    public Transform radiusWidthMarker;
	public AudioSource hitsounder;

    public GameObject effectToSpawn;

    private Vector3 originalScale;
    public float radius;
    private float radiusWorldWidth;

    public KeyCode pauseButton;
	void Start () {
        originalScale = transform.localScale;
        if (radiusMarker != null)
        {
            radius = (radiusMarker.position - transform.position).magnitude;
            if (radiusWidthMarker != null)
                radiusWorldWidth = (radiusWidthMarker.position - radiusMarker.position).magnitude;
        }

	}
    public Canvas PauseOverlay;
    IEnumerator PauseCoroutine()
    {

        isPaused = !isPaused;
        if (isPaused)
        {
            Music.Pause();
            Time.timeScale = 0;
        }
        else
        {
            if (Music.AudioTimeSec != 0.0f)
                Music.Resume();

            Time.timeScale = 1;
        }

        if (PauseOverlay == null) yield return new WaitForEndOfFrame();
        else
        {
            PauseOverlay.gameObject.SetActive(true);
            MaskableGraphic[] pauseElements = PauseOverlay.GetComponentsInChildren<MaskableGraphic>();
            if (isPaused)
            {
                foreach (MaskableGraphic item in pauseElements)
                {
                    item.canvasRenderer.SetAlpha(0.01f);
                    item.CrossFadeAlpha(1.0f, 0.2f, true);
                }
                yield return new WaitForSecondsRealtime(0.2f);
            }
            else
            {
                foreach (MaskableGraphic item in pauseElements)
                {
                    item.CrossFadeAlpha(0.0f, 0.2f, true);
                }
                yield return new WaitForSecondsRealtime(0.2f);
                PauseOverlay.gameObject.SetActive(false);
            }
        }
    }
    // Update is called once per frame
    float[] spectrum = new float[256];
    bool isPaused = false;
	void Update ()
    {
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        transform.localScale = originalScale + Vector3.one * (spectrum[3] * 0.1f);
        for (int i = 0; i < ChannelsInput.Length; ++i)
        {
            if(Input.GetKeyDown(ChannelsInput[i]))
            {
                float rotationAngle = (i / (float)(ChannelsInput.Length)) * 360.0f;
                Quaternion rot = Quaternion.AngleAxis(rotationAngle, Vector3.forward);

                GameObject x = GameObject.Instantiate(effectToSpawn, transform);
                x.transform.localPosition += rot * Vector3.right * radius;
                x.transform.localRotation = rot;

				if (i < hitsounds.Length && hitsounder != null) {
					hitsounder.Stop ();
					hitsounder.clip = hitsounds [i];
					hitsounder.Play ();
				}

            }
        }
        if (Input.GetKeyDown(pauseButton))
        {
            TogglePause();
        }
	}
    private Coroutine coroutine;
    public void TogglePause()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(PauseCoroutine());
    }
}
