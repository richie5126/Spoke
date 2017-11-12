using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GenericButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    // Use this for initialization
    public AudioSource source;
    public AudioClip sound;
    Vector3 origScale;

    bool isOver = false;
    void Start() { origScale = transform.localScale;  }
    void Update() { if(!isOver && Time.timeScale >= 1.0f) transform.localScale = Vector3.Lerp(transform.localScale, origScale, 10.0f * Time.deltaTime); }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
        transform.localScale = origScale * 1.1f;
        if(source != null && sound != null)
        source.PlayOneShot(sound);
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
    }
}
