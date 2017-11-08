using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

    // Use this for initialization
	public enum RotationAxis{X, Y, Z};

    Vector3 originalScale;
	public RotationAxis rotAxis;
	void Start () {
        originalScale = transform.localScale;
	}

    // Update is called once per frame
    float[] spectrum = new float[256];
	void Update ()
    {
        //AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        //transform.localScale = originalScale + Vector3.one * (spectrum[3] * 0.5f);
		if(rotAxis == RotationAxis.X)
			transform.Rotate(new Vector3(5.0f * Time.deltaTime, 0.0f, 0.0f));
		else if(rotAxis == RotationAxis.Y)
			transform.Rotate(new Vector3(0.0f, 5.0f * Time.deltaTime, 0.0f));
		else if(rotAxis == RotationAxis.Z)
			transform.Rotate(new Vector3(0.0f, 0.0f, 5.0f * Time.deltaTime));
			
	}
}
