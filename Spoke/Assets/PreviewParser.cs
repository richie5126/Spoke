using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PreviewParser : MonoBehaviour {

    // Use this for initialization
    public string ResourceName;
    public AudioFade Previewer;

    int channels;
    float bpm;
    float approach;
    string title;
    int notetotal = 0;

    public Text titleT;
    public Text bpmT;
    public Text approachT;
    public Text noteTotalT;

    TextAsset textFile;
    public void ParseFile(string MapName)
    {
        notetotal = 0;
        textFile = Resources.Load(MapName) as TextAsset;
        if (textFile == null)
        {
            Debug.Log("Resource not found...");
            return;
        }

        ResourceName = MapName;
        byte[] bytedata = textFile.bytes;
        string text = System.Text.ASCIIEncoding.ASCII.GetString(bytedata);

        //clear whitespace
        text = text.Replace("\n", string.Empty);
        Debug.Log(text);
        char[] separators = { '=', ';' };
        string[] strValues = text.Split(separators);
        for (int i = 0; i < strValues.Length; i += 2)
        {
            if (i + 1 >= strValues.Length) break;
            if (strValues[i].Equals("songfile"))
            {
                Debug.Log(strValues[i + 1]);
                AudioClip songData = (AudioClip)Resources.Load(strValues[i + 1]);
                Debug.Log(songData);
                if (songData != null)
                {
                    Debug.Log("Found the file!");
                    if (Previewer != null) Previewer.FadeInSoundOneShot(songData);
                }
                else Debug.Log("Error: Could not find file requested!");

            }
            else if (strValues[i].Equals("channels"))
            {

                channels = int.Parse(strValues[i + 1]);
            }
            else if (strValues[i].Equals("title"))
            {

                title = strValues[i + 1];
            }
            else if (strValues[i].Equals("bpm"))
            {

                bpm = float.Parse(strValues[i + 1]);
            }
            else if (strValues[i].Equals("approach"))
            {
                approach = float.Parse(strValues[i + 1]);
            }

            else if (strValues[i].Equals("notes1"))
            {

                strValues[i + 1] = strValues[i + 1].Replace(" ", "");
                notetotal += strValues[i + 1].Length;
            }

            else if (strValues[i].Equals("notes2"))
            {
                strValues[i + 1] = strValues[i + 1].Replace(" ", "");
                notetotal += strValues[i + 1].Length;
            }
        }
        if (titleT != null) titleT.text = title;
        if (bpmT != null) bpmT.text = bpm.ToString("n2");
        if (approachT != null) approachT.text = approach.ToString("n2");
        if (noteTotalT != null) noteTotalT.text = notetotal.ToString();
    }
	void Start () {
    }
	
    public void UpdateTempo(float value)
    {
        if (bpmT != null) bpmT.text = (bpm * value).ToString("n2");
    }
    public void UpdateDifficulty(float value)
    {
        if (approachT != null) approachT.text = (approach + (value - Mathf.Ceil(SceneLoader.maximumDifficultiesPossible / 2.0f))).ToString("n2");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
