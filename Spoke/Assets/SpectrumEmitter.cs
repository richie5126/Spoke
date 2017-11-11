using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumEmitter : MonoBehaviour {

    // Use this for initialization
    ParticleSystem ps;
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}

    // Update is called once per frame
    float[] spectrum = new float[256];
    void Update()
    {

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        var tmp = ps.main;
        float origSpeed = ps.main.startSpeed.constant;
        tmp.startSpeed = 10.0f;

        ps.Emit((int)(spectrum[3] * 10.0f));
        tmp.startSpeed = origSpeed;

    }
}
