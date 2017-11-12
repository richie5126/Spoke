using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class LevelButton : MonoBehaviour, IPointerClickHandler{

    // Use this for initialization
    public string levelToLoad = "Mainn";
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        SceneLoader tmp = FindObjectOfType<SceneLoader>();
        if(tmp != null)
        {
            ButtonManager.activeMenu = 1;
            tmp.LoadLevel(levelToLoad);
        }
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
