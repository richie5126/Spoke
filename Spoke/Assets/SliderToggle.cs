using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class SliderToggle : MonoBehaviour
{

    // Use this for initialization
    Slider slider;
    public bool isSpeed = false;
    public void SetSpeed(float value)
    {

        SceneLoader tmp = FindObjectOfType<SceneLoader>();
        if (tmp != null)
        {
            if (slider != null)
                tmp.SetSongSpeed(slider.value);
        }
    }
    public void SetDifficulty(float value)
    {
        SceneLoader tmp = FindObjectOfType<SceneLoader>();
        if (tmp != null)
        {
            if (slider != null)
                tmp.SetDifficulty((int) value);
        }
    }

    void UpdateThis()
    {
        if (isSpeed && slider != null) SetSpeed(slider.value);
        else if (!isSpeed && slider != null) SetDifficulty((int) slider.value);
    }

    void Start () {
        slider = GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
