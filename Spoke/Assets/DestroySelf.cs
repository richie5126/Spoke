using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {

    // Use this for initialization
    public float fadetime = 1.0f;
    public SpriteRenderer sr;
    private float timer = 0.0f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        Color x = sr.color;
        x.a = 1.0f - (timer / fadetime);
        sr.color = x;
        if (timer > fadetime) Destroy(gameObject);
	}
}
