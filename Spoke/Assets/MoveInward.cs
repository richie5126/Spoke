using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInward : MonoBehaviour
{

    Vector3 origin;
    public Vector3 targetPosition;

    public static float timeToMove = 1.3f;
    public GameObject objToMove;
    // Use this for initialization
	public int channel;
	private float speed;

	public PlayerInput player;
	void Start ()
    {
        origin = objToMove.transform.localPosition;
        Destroy(gameObject, timeToMove * 1.4f);
    }

    // Update is called once per frame
    float timer = 0.0f;
	void FixedUpdate () {
        timer += Time.fixedDeltaTime;
        float t = timer / timeToMove;
        objToMove.transform.localPosition = origin + (t * (transform.InverseTransformPoint(targetPosition) - origin));

		if (Input.GetKeyDown (player.ChannelsInput [channel]) && t > 0.85f) {
			if (t < 0.90f)
				Debug.Log ("Good!");
			else if (t < 0.95f)
				Debug.Log ("Great!");
			else if (t < 1.05f)
				Debug.Log ("Perfect!");
			else if (t < 1.2f)
				Debug.Log ("Almost...");
			
			Destroy (gameObject);
		}
		if (t > 1.2f) {
			Debug.Log ("Miss...");
			Destroy (gameObject);
		}
    }
}
