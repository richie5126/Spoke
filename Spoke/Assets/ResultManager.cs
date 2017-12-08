using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {

    // Use this for initialization
    const int ZEROES = 5;
    public Text perfect, great, early, late, miss, score, combo, accuracy, rating;
    int p = 0, gr = 0, e = 0, l = 0, m = 0, sc = 0, c = 0;
    double a = 0.0;
    void Start () {
        StartCoroutine(WriteValue(perfect, p, ScoreManager.perfectCount, 0.0f));
        StartCoroutine(WriteValue(great, gr, ScoreManager.greatCount, 0.2f));
        StartCoroutine(WriteValue(early, e, ScoreManager.earlyCount, 0.4f));
        StartCoroutine(WriteValue(late, l, ScoreManager.lateCount, 0.6f));
        StartCoroutine(WriteValue(miss, m, ScoreManager.missCount, 0.8f));
        StartCoroutine(WriteValue(score, sc, ScoreManager.scoreValue, 1.2f));
        StartCoroutine(WriteValue(combo, c, ScoreManager.comboCount, 1.4f, "x"));
        StartCoroutine(WriteValue(accuracy, a, ScoreManager.accuracyValue, 1.6f, "%"));
        StartCoroutine(WriteRating(rating, ScoreManager.accuracyValue, 1.8f));
    }
    void WriteInteger(Text t, int val)
    {
        if (t == null) return;

        t.text = "";
        for (int i = 0; i < ZEROES - val.ToString().Length; ++i)
            t.text += "0";
        t.text += val;
    }
    void WriteDouble(Text t, double val)
    {
        if (t == null) return;
        t.text = val.ToString("n2");
    }
    IEnumerator WriteValue(Text t, int val, int targetVal, float time, string additionalChars = "")
    {
        if (t != null)
        {

            yield return new WaitForSecondsRealtime(time);
            while (val != targetVal)
            {
                val = Mathf.CeilToInt(Mathf.Lerp(val, targetVal, 5.0f * Time.deltaTime));
                WriteInteger(t, val);
                t.text += additionalChars;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }
        else yield return new WaitForEndOfFrame();
    }
    IEnumerator WriteValue(Text t, double val, double targetVal, float time, string additionalChars = "")
    {
        if (t != null)
        {

            yield return new WaitForSecondsRealtime(time);
            while (val != targetVal)
            {
                val = Mathf.Lerp((float)val, (float)targetVal, 5.0f * Time.deltaTime);
                WriteDouble(t, (float)val * 100.0f);
                t.text += additionalChars;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }
        else yield return new WaitForEndOfFrame();
    }
    IEnumerator WriteRating(Text t, double a, float time)
    {
        t.canvasRenderer.SetAlpha(0.0f);
        yield return new WaitForSecondsRealtime(time);

        t.CrossFadeAlpha(1.0f, 1.0f, true);
            if (a >= 1.0)
            {
                t.text = "S";
            }
            else if (a >= 0.95)
            {
                t.text = "A+";
            }
            else if (a >= 0.90)
            {
                t.text = "A";
            }
            else if (a >= 0.85)
            {
                t.text = "B+";
            }
            else if (a >= 0.80)
            {
                t.text = "B";
            }
            else if (a >= 0.75)
            {
                t.text = "C+";
            }
            else if (a >= 0.70)
            {
                t.text = "C";
            }
            else if (a >= 0.65)
            {
                t.text = "D+";
            }
            else if (a >= 0.60)
            {
                t.text = "D";
            }
            else if (a >= 0.50)
            {
                t.text = "D-";
            }
            else t.text = "F";


        yield return new WaitForEndOfFrame();
    }
    // Update is called once per frame
    void Update () {
	}
}
