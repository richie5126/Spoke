using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class NoteList
{
	public string notedata = "";
	public int currentIndex = 0;
	public NoteList ()
	{
	}
}
public class EffectList : NoteList
{

    public EffectList()
    {
    }
}
public class MapReader : MonoBehaviour {
    // Use this for initialization

	public TextAsset textFile;
	public Color CorePrimaryColor = Color.cyan;

    int channels;
    float bpm;
    float offset;
    float approachrate;
	NoteList notes, notes2;
    EffectList effects;

    public AudioSource musicPlayer;
    public PlayerInput player;
    public GameObject targetObject;

    public AudioSource debugMetronome;
    public Text title;
    float secondsPerSixteenth = 0.0f;
    float audiotimer = 0.0f;

    Color startingColor;
	Renderer[] primaryRenderers;
	IEnumerator StartAudio()
	{

		//start the music!
		musicPlayer.PlayScheduled(AudioSettings.dspTime + 7.0);


		title.CrossFadeAlpha (1.0f, 1.0f, false);
		yield return new WaitForSeconds (1.0f);
		yield return new WaitForSeconds (5.0f);
		title.CrossFadeAlpha (0.0f, 1.0f, false);
		yield return new WaitForSeconds (1.0f);
	}
	public void ChangeColor(Color c)
	{
        CorePrimaryColor = c;
        foreach (Renderer r in primaryRenderers)
        {
            r.material.SetColor("_Color", CorePrimaryColor);
            var sr = r.gameObject.GetComponent<SpectrumFlicker>();
            if (sr != null) sr.originalColor = CorePrimaryColor;
        }
        Debug.Log(primaryRenderers.Length);

    }
    void Awake()
    {
        startingColor = CorePrimaryColor;
		primaryRenderers = FindObjectsOfType<Renderer> ();
		foreach (Renderer r in primaryRenderers)
			r.material.SetColor ("_Color", CorePrimaryColor);
        Debug.Log(primaryRenderers.Length);
		
		notes = new NoteList();
		notes2 = new NoteList ();
        effects = new EffectList();

        byte[] bytedata = textFile.bytes;
        string text = System.Text.ASCIIEncoding.ASCII.GetString(bytedata);

		//clear whitespace
		text = text.Replace ("\n", string.Empty);
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
                    musicPlayer.clip = songData;
                }
                else Debug.Log("Error: Could not find file requested!");

            } else if (strValues[i].Equals("channels"))
            {

                channels = int.Parse(strValues[i + 1]);
            }
            else if (strValues[i].Equals("title"))
            {

                title.text = strValues[i + 1];
                title.CrossFadeAlpha(0.0f, 5.0f, false);
            }
            else if (strValues[i].Equals("bpm"))
            {

                bpm = float.Parse(strValues[i + 1]);
            }
            else if (strValues[i].Equals("startoffset"))
            {

                offset = float.Parse(strValues[i + 1]);
            }
            else if (strValues[i].Equals("approach"))
            {
                approachrate = float.Parse(strValues[i + 1]);
            }

            else if (strValues[i].Equals("notes1"))
            {

                strValues[i + 1] = strValues[i + 1].Replace(" ", "");
                notes.notedata = strValues[i + 1];
                Debug.Log(notes.notedata);
            }

            else if (strValues[i].Equals("notes2"))
            {
                strValues[i + 1] = strValues[i + 1].Replace(" ", "");
                notes2.notedata = strValues[i + 1];
                Debug.Log(notes2.notedata);
            }
            else if (strValues[i].Equals("effects1"))
            {
                strValues[i + 1] = strValues[i + 1].Replace(" ", "");
                effects.notedata = strValues[i + 1];
                Debug.Log(effects.notedata);
            }
        }
        Debug.Log("Channels: " + channels);
        Debug.Log("BPM: " + bpm);
        Debug.Log("Approach: " + approachrate);
        MoveInward.timeToMove = approachrate * 0.2f;
		//eight time
        secondsPerSixteenth = 30.0f / bpm;


		Debug.Log("Note total: " + notes.notedata.Length);
		Debug.Log("Note2 total: " + notes2.notedata.Length);

		notes.currentIndex = (int)((MoveInward.timeToMove - (0.001f * offset)) / secondsPerSixteenth);
		notes2.currentIndex = (int)((MoveInward.timeToMove - (0.001f * offset)) / secondsPerSixteenth);
        effects.currentIndex = (int)(((0.001f * offset)) / secondsPerSixteenth);
        /*
		StartCoroutine (StartAudio ());
		*/
        musicPlayer.PlayScheduled (AudioSettings.dspTime + 5.0f);
    }
    int val = 0;

    bool audioToggle = false;
    bool audioToggle2 = false;
    public static float spawnTimer = 0.0f;
    void IncrementTime(ref int valueToIncrement)
    {
        /*
        if (musicPlayer.pitch < 0.0f) valueToIncrement -= 1;
        else valueToIncrement += 1;
        */
        ++valueToIncrement;
    }
    void FixedUpdate () {
		audiotimer = musicPlayer.time + (0.001f * offset);
		//spawnTimer = audiotimer - MoveInward.timeToMove;
		spawnTimer = audiotimer;

        if (((audiotimer) % (secondsPerSixteenth * 2.0f)) <= 0.1f)
        {

            if (!audioToggle)
			{
                debugMetronome.Stop();
                debugMetronome.Play();
                audioToggle = !audioToggle;

            }
        }
        else audioToggle = false;
        


        if (((spawnTimer) % (secondsPerSixteenth * 1.0f)) <= 0.1f)
        {

            if (!audioToggle2)
            {
				SpawnMarker (notes);
				SpawnMarker (notes2);
                PerformEffect(effects);
                audioToggle2 = !audioToggle2;

            }
        }
        else audioToggle2 = false;
    }
    
	void SpawnMarker(NoteList notesi)
    {

		if (notesi.currentIndex >= notesi.notedata.Length) return;
        if(notesi.currentIndex < 0)
        {
            IncrementTime(ref notesi.currentIndex);
            return;
        }
		int currentnote = notesi.notedata [notesi.currentIndex] - '0';

		if (currentnote == 0 || currentnote > player.ChannelsInput.Length)
        {
            IncrementTime(ref notesi.currentIndex);
            return;
        }

        else
        {
            float rotationAngle = ((currentnote-1) / (float)(player.ChannelsInput.Length)) * 360.0f;
            Quaternion rot = Quaternion.AngleAxis(rotationAngle, Vector3.forward);

            GameObject x = GameObject.Instantiate(targetObject, transform);
            x.transform.localPosition += rot * Vector3.right * 10.0f;
            x.transform.localRotation = rot;
			x.GetComponent<MoveInward> ().channel = currentnote - 1;
			x.GetComponent<MoveInward> ().musicPlayer = this.musicPlayer;
            x.GetComponent<MoveInward>().startTime = spawnTimer;
            //x.GetComponent<MoveInward> ().primaryColor = CorePrimaryColor;
            x.GetComponent<MoveInward> ().player = this.player;
            x.GetComponent<MoveInward>().targetPosition = player.gameObject.transform.position + (rot * Vector3.right * player.radius * 0.6f);

            IncrementTime(ref notesi.currentIndex);
        }


    }
    void PerformEffect(EffectList notesi)
    {

        if (notesi.currentIndex >= notesi.notedata.Length) return;
        if (notesi.currentIndex < 0)
        {
            ++notesi.currentIndex;
            return;
        }
        char currentnote = notesi.notedata[notesi.currentIndex];

        if (currentnote == '0')
        {
            ++notesi.currentIndex;
            return;
        }

        else
        {
            //we do an effect bois
            switch(currentnote)
            {
                case 'b':
                    ChangeColor(Color.blue);
                    break;
                case 'r':
                    ChangeColor(Color.red);
                    break;
                case 'g':
                    ChangeColor(Color.green);
                    break;
                case '1':
                    ChangeColor(startingColor);
                    break;
                case 'w':
                    ChangeColor(Color.white);
                    break;
                default:
                    break;

            }
            Debug.Log("bopo!");
            ++notesi.currentIndex;
        }


    }
}
