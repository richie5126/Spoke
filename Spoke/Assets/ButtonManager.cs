using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    // Use this for initialization
    public GameObject[] MenuWindows;
    public static int activeMenu = 0;

    public int levelSelectIndex = 1;
    public GameObject ButtonPrefab;
    Vector3 originalPosition;

    public bool GenerateButtonsFromResources = false;

    public static Dictionary<string, TextAsset> MapDatabase;
    public GameObject MapsViewport;
    private IEnumerator swap_routine;
	void Start () {
        originalPosition = transform.position;
        MapDatabase = new Dictionary<string, TextAsset>();

        TextAsset[] tmp = Resources.LoadAll<TextAsset>("Notemaps");
        float offsetHeight = 0.0f;
        foreach (TextAsset t in tmp)
        {
            Debug.Log("Adding " + t.name + " to the database.");
            MapDatabase.Add(t.name, t);

            //do not activate unless you're a fag
            if (GenerateButtonsFromResources)
            {
                if (MapsViewport != null)
                {
                    //instantiate inside the content window;
                    MapsViewport.SetActive(true);
                    var g = GameObject.Instantiate(ButtonPrefab, MapsViewport.transform);
                    g.GetComponentInChildren<Text>().text = t.name;
                    g.GetComponentInChildren<LevelNameButton>().resourceName = t.name;

                    var lnb = g.GetComponentInChildren<LevelNameButton>();
                    lnb.tb = FindObjectOfType<TitleButton>();
                    lnb.pp = FindObjectOfType<PreviewParser>();
                    lnb.bm = FindObjectOfType<ButtonManager>();
                    if (lnb.tb != null) lnb.source = lnb.tb.gameObject.GetComponent<AudioSource>();
                    lnb.af = GameObject.Find("SpectrumDrawer").GetComponent<AudioFade>();
                    g.transform.localPosition += new Vector3(0.0f, offsetHeight);
                    offsetHeight -= 40.0f;
                }
            }
        }
	}
    public void SwapToMenuWithIndex(int value)
    {
        if(swap_routine != null)
        StopCoroutine(swap_routine);

       swap_routine =  SwapMenus(gameObject, new Vector3(originalPosition.x - 10.0f, originalPosition.y), 10.0f, value);
        StartCoroutine(swap_routine);
    }
    IEnumerator SwapMenus(GameObject objectToMove, Vector3 end, float speed, int menuToSwapTo)
    {
        // speed should be 1 unit per second

        //move this offscreen first.
        float movementspeed = speed;
        while ((objectToMove.transform.position-end).magnitude > 0.1f)
        {
            objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        //perform the swap
        if (activeMenu < MenuWindows.Length && menuToSwapTo < MenuWindows.Length)
        {
            if(activeMenu >= 0)
            MenuWindows[activeMenu].SetActive(false);
            activeMenu = menuToSwapTo;

            if (activeMenu >= 0)
                MenuWindows[activeMenu].SetActive(true);
        }

        //return this back to its original location
        while (objectToMove.transform.position != originalPosition)
        {
            objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, originalPosition, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

    }
    // Update is called once per frame
    void Update () {
		
	}
}
