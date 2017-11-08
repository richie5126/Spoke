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
	void Start () {
        originalScale = transform.localScale;
        if (radiusMarker != null)
        {
            radius = (radiusMarker.position - transform.position).magnitude;
            if (radiusWidthMarker != null)
                radiusWorldWidth = (radiusWidthMarker.position - radiusMarker.position).magnitude;
        }

	}

    // Update is called once per frame
    float[] spectrum = new float[256];
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
	}
}
