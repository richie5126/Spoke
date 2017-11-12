﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {

	// Use this for initialization
	public int score = 0;
	public float scoredisplayed = 0;
	public int scoretracker = 0;
	public int combo = 1;
	int totalZeroes = 10;
    float globalMultiplier = 1.0f;

	public float notesPlayed;
	public float noteAccuracy;

	public float accuracy = 0.0f;
	public float displayedAccuracy = 0.0f;
	public Text scoreDisplay;
	public Text accuracyDisplay;
	public Text comboDisplay;


    public GameObject goodSplash;
    public GameObject greatSplash;
    public GameObject perfectSplash;
    public GameObject missSplash;

    GameObject singleSplash;
    void Start () {
		scoredisplayed = score;
        SceneLoader tmp = FindObjectOfType<SceneLoader>();
        if (tmp != null) globalMultiplier = tmp.resultingMultiplier;

	}
	public void AddToScore(int points)
	{
        score += (int)((points * combo) * globalMultiplier);
		++combo;
	}
	public void BreakCombo()
	{
		combo = 1;
	}
    public void CreateScoreMessage(int scoretype)
    {
        switch(scoretype)
        {
            case 3:
                if (perfectSplash != null)
                {
                    if (singleSplash != null) Destroy(singleSplash);
                    singleSplash = GameObject.Instantiate(perfectSplash, Camera.main.transform);
                }
                break;
            case 2:
                if (greatSplash != null)
                {
                    if (singleSplash != null) Destroy(singleSplash);
                    singleSplash = GameObject.Instantiate(greatSplash, Camera.main.transform);
                }
                break;
            case 1:
                if (goodSplash != null)
                {
                    if (singleSplash != null) Destroy(singleSplash);
                    singleSplash = GameObject.Instantiate(goodSplash, Camera.main.transform);
                }
                break;
            case 0:
                if (perfectSplash != null)
                {
                    if (singleSplash != null) Destroy(singleSplash);
                    singleSplash = GameObject.Instantiate(missSplash, Camera.main.transform);
                }
                break;

        }
    }
	// Update is called once per frame
	float timer = 0.0f;
	void FixedUpdate () {
		if (notesPlayed <= 0)
			accuracy = 0.0f;
		else accuracy = noteAccuracy / notesPlayed;

		scoredisplayed = Mathf.Lerp (scoredisplayed, (float)(score), 5.0f * Time.deltaTime); 
		displayedAccuracy = Mathf.Lerp (displayedAccuracy, accuracy, 10.0f * Time.deltaTime);
		scoretracker = Mathf.CeilToInt(scoredisplayed);
		if (scoreDisplay != null) {
			int numOccupiedZeroes = scoretracker.ToString().Length;
			scoreDisplay.text = "";
			for (int i = 0; i < totalZeroes - numOccupiedZeroes; ++i)
				scoreDisplay.text += "0";
			scoreDisplay.text += "" + scoretracker;
		}
		if (accuracyDisplay != null) {
			accuracyDisplay.text = "" + (displayedAccuracy * 100.0f).ToString ("n2") + "%";
		}
		if (comboDisplay != null) {
			comboDisplay.text = "x" + combo;
		}
		/*
		timer += Time.fixedDeltaTime;
		if (timer > 1.0f) {
			score += 300;
			timer = 0.0f;
		}
		*/
	}
}
