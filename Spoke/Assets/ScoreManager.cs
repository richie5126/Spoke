using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {

	// Use this for initialization
	public int score = 0;
	float scoredisplayed = 0;
	public int scoretracker = 0;
	public int combo = 1;
	int totalZeroes = 10;

	public float notesPlayed;
	public float noteAccuracy;

	public float accuracy = 0.0f;
	public Text scoreDisplay;
	public Text accuracyDisplay;
	public Text comboDisplay;
	void Start () {
		scoredisplayed = score;
	}
	public void AddToScore(int points)
	{
		score += points * combo;
		++combo;
	}
	public void BreakCombo()
	{
		combo = 1;
	}
	
	// Update is called once per frame
	float timer = 0.0f;
	void FixedUpdate () {
		if (notesPlayed <= 0)
			accuracy = 0.0f;
		else accuracy = noteAccuracy / notesPlayed;

		scoredisplayed = Mathf.Lerp (scoredisplayed, (float)(score), 5.0f * Time.deltaTime); 
		scoretracker = Mathf.CeilToInt(scoredisplayed);
		if (scoreDisplay != null) {
			int numOccupiedZeroes = scoretracker.ToString().Length;
			scoreDisplay.text = "";
			for (int i = 0; i < totalZeroes - numOccupiedZeroes; ++i)
				scoreDisplay.text += "0";
			scoreDisplay.text += "" + scoretracker;
		}
		if (accuracyDisplay != null) {
			accuracyDisplay.text = "" + (accuracy * 100.0f).ToString ("n2") + "%";
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
