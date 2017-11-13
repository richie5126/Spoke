using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class LevelNameButton : MonoBehaviour, IPointerClickHandler{

    // Use this for initialization
    public string resourceName = "testmap";
    public AudioSource source;
    public AudioClip clip;
    public AudioFade af;
    public ButtonManager bm;
    public PreviewParser pp;
    public TitleButton tb;

    public int menuToSwapTo = 3;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        SceneLoader tmp = FindObjectOfType<SceneLoader>();
        if(tmp != null)
        {
            tmp.SetResourceName(resourceName);
        }
        if (clip != null && source != null) source.PlayOneShot(clip);
        if (bm != null) bm.SwapToMenuWithIndex(menuToSwapTo);
        if (af != null) af.FadeSound(af.gameObject.GetComponent<AudioSource>());
        if (pp != null) pp.ParseFile(resourceName);
        if (tb != null) tb.FlyOut();

    }
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
