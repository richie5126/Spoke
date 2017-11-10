using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumFlicker : MonoBehaviour {

    // Use this for initialization

    private Renderer plane;
    public Color originalColor;
	public float intensity = 2.0f;
	void Start () {
        plane = GetComponent<Renderer>();
	}

    // Update is called once per frame
    float[] spectrum = new float[256];
    void Update () {

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        plane.material.SetColor("_Color", originalColor + (Color.white * (spectrum[1] * intensity)));
    }
}
