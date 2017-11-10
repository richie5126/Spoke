﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    // Use this for initialization
    public GameObject[] MenuWindows;
    public static int activeMenu = 0;

    Vector3 originalPosition;
	void Start () {
        originalPosition = transform.position;
        SwapToMenuWithIndex(0);
	}
    public void SwapToMenuWithIndex(int value)
    {
        StopAllCoroutines();
        StartCoroutine(SwapMenus(gameObject, new Vector3(originalPosition.x - 10.0f, originalPosition.y), 10.0f, value));
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
        if (activeMenu < MenuWindows.Length && menuToSwapTo < MenuWindows.Length && MenuWindows[activeMenu] != null && MenuWindows[menuToSwapTo] != null)
        {
            MenuWindows[activeMenu].SetActive(false);
            activeMenu = menuToSwapTo;
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