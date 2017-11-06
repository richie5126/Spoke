using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

    // Use this for initialization
    Vector3 originalScale;
	void Start () {
        originalScale = transform.localScale;
	}

    // Update is called once per frame
    float[] spectrum = new float[256];
	void Update ()
    {
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        transform.localScale = originalScale + Vector3.one * (spectrum[3] * 0.5f);
        //transform.Rotate(new Vector3(0.0f, 0.0f, 5.0f * Time.deltaTime));
	}
}
