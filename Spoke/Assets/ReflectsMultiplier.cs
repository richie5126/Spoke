using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReflectsMultiplier : MonoBehaviour {

    SceneLoader sl;
	// Use this for initialization
	void Start () {
        sl = FindObjectOfType<SceneLoader>();

    }
	
	// Update is called once per frame
	void Update () {
		if(sl != null)
            GetComponent<Text>().text = "" + sl.resultingMultiplier.ToString("n2") +"x";
	}
}
