using System.Collections;
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

    public Color primaryColor = Color.white;
	void Start ()
    {

		//startTime = AudioSettings.dspTime;

		if (musicPlayer == null)
			startTime = AudioSettings.dspTime;
		else
			startTime = (double) musicPlayer.time;
		transform.GetChild(0).GetComponent<Renderer> ().material.SetColor ("_Color", primaryColor);
        origin = objToMove.transform.localPosition;
        //Destroy(gameObject, timeToMove * 1.4f);
    }

    // Update is called once per frame
	void Update () {
		if (scoreManager == null)
			scoreManager = FindObjectOfType<ScoreManager> ();
		
		float t;


		if(musicPlayer == null)
		t = (float)(AudioSettings.dspTime - startTime) / timeToMove;
		else t = (float) (musicPlayer.time - startTime) / timeToMove;

		//t = (float)(AudioSettings.dspTime - startTime) / timeToMove;
        objToMove.transform.localPosition = origin + (t * (transform.InverseTransformPoint(targetPosition) - origin));

		if (Input.GetKeyDown (player.ChannelsInput [channel]) && t > 0.85f) {
			++scoreManager.notesPlayed;
			if (t < 0.90f) {
				scoreManager.noteAccuracy += 0.50f;
				scoreManager.AddToScore (50);
                scoreManager.CreateScoreMessage(1);
			} else if (t < 0.95f) {
				scoreManager.noteAccuracy += 0.75f;
				scoreManager.AddToScore (100);
                scoreManager.CreateScoreMessage(2);
            } else if (t < 1.05f) {
				scoreManager.noteAccuracy += 1.0f;
				scoreManager.AddToScore (200);
                scoreManager.CreateScoreMessage(3);
            } else if (t < 1.2f) {
				Debug.Log ("Almost...");
				scoreManager.AddToScore (25);
				scoreManager.noteAccuracy += 0.25f;
				scoreManager.BreakCombo ();
                scoreManager.CreateScoreMessage(1);
            }
			
			Destroy (gameObject);
		}
		if (t > 1.3f) {
			Debug.Log ("Miss...");
            ++scoreManager.notesPlayed;
			scoreManager.BreakCombo ();
            scoreManager.CreateScoreMessage(0);
            Destroy (gameObject);
		}
    }
}
