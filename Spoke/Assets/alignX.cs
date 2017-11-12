using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alignX : MonoBehaviour {

	// Use this for initialization
    public enum Axis { X, Y, Z};
    public GameObject ThingToAlign;
    public Axis axis;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        if (axis == Axis.X)
            pos.x = ThingToAlign.transform.position.x;
        else if(axis == Axis.Y)
            pos.y = ThingToAlign.transform.position.y;
        else if (axis == Axis.Z)
            pos.z = ThingToAlign.transform.position.z;
        transform.position = pos;
    }
}
