using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInward : MonoBehaviour
{

    Vector3 origin;
    public Vector3 targetPosition;

    public static float timeToMove = 1.3f;
    public GameObject objToMove;
    // Use this for initialization
    private float speed;
	void Start ()
    {
        origin = objToMove.transform.localPosition;
        Destroy(gameObject, timeToMove);
    }

    // Update is called once per frame
    float timer = 0.0f;
	void Update () {
        timer += Time.deltaTime;
        float t = timer / timeToMove;
        objToMove.transform.localPosition = origin + (t * (transform.InverseTransformPoint(targetPosition) - origin));

    }
}
