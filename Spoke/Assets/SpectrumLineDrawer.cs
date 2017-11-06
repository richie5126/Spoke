using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumLineDrawer : MonoBehaviour {

    public float usedSamples = 32;
    public Transform rightbound;
    float[] spectrum = new float[256];
    public LineRenderer lr;
    void Update()
    {

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        int sampleOffset = 6;
        for (int i = 0; i < usedSamples; i++)
        {
            if(i % 2 == 0)
            lr.SetPosition(i, transform.position + new Vector3((i / usedSamples) * (rightbound.position - transform.position).magnitude, spectrum[i + sampleOffset], 0));
            else
            lr.SetPosition(i, transform.position + new Vector3((i / usedSamples) * (rightbound.position - transform.position).magnitude, -spectrum[i + sampleOffset], 0));
        }
    }
}
