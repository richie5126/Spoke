using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class LevelButton : MonoBehaviour, IPointerClickHandler{

    // Use this for initialization
    public string resourceName = "testmap";
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        SceneLoader tmp = FindObjectOfType<SceneLoader>();
        if(tmp != null)
        {
            tmp.SetResourceName(resourceName);
            tmp.LoadLevel();


        }
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
