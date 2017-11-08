﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInward : MonoBehaviour
{

    Vector3 origin;
    public Vector3 targetPosition;

    public static float timeToMove = 2.0f;
    public GameObject objToMove;
    // Use this for initialization
	public int channel;
	private float speed;

	public ScoreManager scoreManager;
	public double startTime;

	public AudioSource musicPlayer;
	public PlayerInput player;

	Renderer[] primaryRenderers;
	void Start ()
    {

		//startTime = AudioSettings.dspTime;

		if (musicPlayer == null)
			startTime = AudioSettings.dspTime;
		else
			startTime = (double) musicPlayer.time;


        origin = objToMove.transform.localPosition;
        Destroy(gameObject, timeToMove * 1.4f);
    }

    // Update is called once per frame
	void FixedUpdate () {
		if (scoreManager == null)
			scoreManager = FindObjectOfType<ScoreManager> ();
		
		double offset;
		float t;


		if(musicPlayer == null)
		t = (float)(AudioSettings.dspTime - startTime) / timeToMove;
		else t = (float) (musicPlayer.time - startTime) / timeToMove;

		//t = (float)(AudioSettings.dspTime - startTime) / timeToMove;

        objToMove.transform.localPosition = origin + (t * (transform.InverseTransformPoint(targetPosition) - origin));

		if (Input.GetKeyDown (player.ChannelsInput [channel]) && t > 0.85f) {
			++scoreManager.notesPlayed;
			if (t < 0.90f) {
				Debug.Log ("Good!");
				scoreManager.noteAccuracy += 0.50f;
				scoreManager.AddToScore (50);
			} else if (t < 0.95f) {
				Debug.Log ("Great!");
				scoreManager.noteAccuracy += 0.75f;
				scoreManager.AddToScore (100);
			} else if (t < 1.05f) {
				Debug.Log ("Perfect!");
				scoreManager.noteAccuracy += 1.0f;
				scoreManager.AddToScore (200);
			} else if (t < 1.2f) {
				Debug.Log ("Almost...");
				scoreManager.AddToScore (25);
				scoreManager.noteAccuracy += 0.25f;
				scoreManager.BreakCombo ();
			}
			
			Destroy (gameObject);
		}
		if (t > 1.2f) {
			Debug.Log ("Miss...");
			++scoreManager.notesPlayed;
			scoreManager.BreakCombo ();
			Destroy (gameObject);
		}
    }
}
