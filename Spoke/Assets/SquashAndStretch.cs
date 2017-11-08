using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashAndStretch : MonoBehaviour {

		// Use this for initialization
	public float xMaxDegree = 10.0f;
	public float yMaxDegree = 10.0f;
	public float zMaxDegree = 10.0f;
	public float xRate = 0.2f;
	public float yRate = 0.2f;
	public float zRate = 0.2f;


		float start;
		void Start () {
			start = Time.time;
		}

		// Update is called once per frame
		void Update () {
			float xRotMult = Mathf.Sin (xRate * (Time.time - start));
		float yRotMult = Mathf.Sin (yRate * (Time.time - start));
		float zRotMult = Mathf.Sin (zRate * (Time.time - start));
			transform.parent.localRotation = Quaternion.Euler (
			new Vector3 (xRotMult * xMaxDegree, yRotMult * yMaxDegree, zRotMult * zMaxDegree));

		}
}
