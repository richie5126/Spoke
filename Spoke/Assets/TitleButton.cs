using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour {

	// Use this for initialization
	private IEnumerator i;
	Vector3 origPosition;
	IEnumerator MoveToPosition(Vector3 end, float speed)
	{

		float movementspeed = speed;
		while ((transform.position-end).magnitude > 0.1f)
		{
			transform.position = Vector3.Lerp(transform.position, end, speed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}
	public void LerpMove()
	{
		StartCoroutine(i);

	}
	public void restorePosition()
	{
		i = MoveToPosition (origPosition, 10.0f);
		StartCoroutine (i);
	}
	void Start () {
		origPosition = transform.position;
		i = MoveToPosition (transform.position + new Vector3 (4.0f, 0.0f, 0.0f), 10.0f);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
