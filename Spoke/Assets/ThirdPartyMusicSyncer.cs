using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPartyMusicSyncer : MonoBehaviour {

    // Use this for initialization
    public AudioSource metronome;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Music.IsJustChangedBeat())
            metronome.Play();
	}
}
