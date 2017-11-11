using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpectrumFlicker : MonoBehaviour {

    // Use this for initialization

    private Renderer plane;
	private MaskableGraphic uiObject;
    public Color originalColor;
	public float intensity = 2.0f;
	void Start () {
        plane = GetComponent<Renderer>();
		uiObject = GetComponent<MaskableGraphic> ();
	}

    // Update is called once per frame
    float[] spectrum = new float[256];
    void Update () {

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
		if(plane != null)
		plane.material.SetColor("_Color", originalColor + (Color.white * (spectrum[1] * intensity)));
		if (uiObject != null)
			uiObject.color = originalColor + (Color.white * (spectrum [1] * intensity));
    }
}
