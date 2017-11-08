using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumFlicker : MonoBehaviour {

    // Use this for initialization

    private MeshRenderer plane;
    private Color originalColor;
	void Start () {
        plane = GetComponent<MeshRenderer>();
        if (plane != null)
            originalColor = plane.material.color;
	}

    // Update is called once per frame
    float[] spectrum = new float[256];
    void Update () {

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        plane.material.SetColor("_Color", originalColor + (Color.white * (spectrum[1] * 2.0f)));
    }
}
