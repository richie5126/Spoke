using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumPounder : MonoBehaviour {

    // Use this for initialization

    private Renderer plane;
	private Vector3 originalScale;
	public float intensity = 2.0f;
	void Start () {
        plane = GetComponent<Renderer>();
		if (plane != null)
			originalScale = plane.transform.localScale;
	}

    // Update is called once per frame
    float[] spectrum = new float[256];
    void Update () {

		AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
		transform.localScale = originalScale + Vector3.one * (spectrum[3] * 0.1f);
    }
}
