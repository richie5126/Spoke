using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class MapReader : MonoBehaviour {
    // Use this for initialization
	public TextAsset textFile;

    int channels;
    float bpm;
    float offset;
    float approachrate;
    List<byte> notes = new List<byte>();
	List<byte> notes2 = new List<byte>();
	string notedata = "";
	string notedata2 = "";

    public AudioSource musicPlayer;
    public PlayerInput player;
    public GameObject targetObject;

    public AudioSource debugMetronome;

    float secondsPerSixteenth = 0.0f;
    float audiotimer = 0.0f;
    void Start()
    {
        //start the music!
        musicPlayer.Play();
		string text = textFile.text;

        char[] separators = { '=', ';' };
        string[] strValues = text.Split(separators);

        for (int i = 0; i < strValues.Length; i += 2)
        {
            if (i + 1 >= strValues.Length) break;

            switch (strValues[i])
            {
                case "channels":
                    channels = int.Parse(strValues[i + 1]);
                    break;
                case "bpm":
                    bpm = float.Parse(strValues[i + 1]);
                    break;
                case "startoffset":
                    offset = float.Parse(strValues[i + 1]);
                    break;
                case "approach":
                    approachrate = float.Parse(strValues[i + 1]);
                    break;
                case "notes1":
                    notedata = strValues[i + 1];
				notedata = notedata.Replace(" ", string.Empty);
				notedata = notedata.Replace("\n", string.Empty);
                    Debug.Log(notedata);
				break;
				case "notes2":
				notedata2 = strValues[i + 1];
				notedata2 = notedata2.Replace(" ", string.Empty);
				notedata2 = notedata2.Replace("\n", string.Empty);
				Debug.Log(notedata2);
				break;
                default:
                    break;
            }
        }
        Debug.Log("Channels: " + channels);
        Debug.Log("BPM: " + bpm);
        Debug.Log("Approach: " + approachrate);
		//eight time
        secondsPerSixteenth = 30.0f / bpm;
        

        foreach (char x in notedata)
        {
            byte t = (byte)(x - '0');
            notes.Add(t);
        }

		foreach (char x in notedata2)
		{
			byte t = (byte)(x - '0');
			notes2.Add(t);
		}


        Debug.Log("Note total: " + notes.Count);


    }
    int val = 0;

    bool audioToggle = false;
    bool audioToggle2 = false;
    float spawnTimer = 0.0f;

    void FixedUpdate () {
		audiotimer = musicPlayer.time + (0.001f * offset);
		spawnTimer = audiotimer - MoveInward.timeToMove;
		//spawnTimer = audiotimer;

        if (((audiotimer) % (secondsPerSixteenth * 4.0f)) <= 0.06f)
        {

            if (!audioToggle)
			{
                debugMetronome.Stop();
                debugMetronome.Play();
                audioToggle = !audioToggle;

            }
        }
        else audioToggle = false;
        


        if (((spawnTimer) % (secondsPerSixteenth * 1.0f)) <= 0.06f)
        {

            if (!audioToggle2)
            {
                SpawnMarker();
                audioToggle2 = !audioToggle2;

            }
        }
        else audioToggle2 = false;
    }
    
    int currentIndex = 0;
    void SpawnMarker()
    {
        if (musicPlayer.time <= 0.01f) currentIndex = 0;

        if (currentIndex >= notes.Count) return;

        if (notes[currentIndex] == 0 || notes[currentIndex] > player.ChannelsInput.Length)
        {
            ++currentIndex;
            return;
        }

        else
        {
            float rotationAngle = ((notes[currentIndex]-1) / (float)(player.ChannelsInput.Length)) * 360.0f;
            Quaternion rot = Quaternion.AngleAxis(rotationAngle, Vector3.forward);

            GameObject x = GameObject.Instantiate(targetObject, transform);
            x.transform.localPosition += rot * Vector3.right * 10.0f;
            x.transform.localRotation = rot;
			x.GetComponent<MoveInward> ().channel = notes[currentIndex] - 1;
			x.GetComponent<MoveInward> ().player = this.player;
            x.GetComponent<MoveInward>().targetPosition = player.gameObject.transform.position + (rot * Vector3.right * player.radius * 0.6f);
            ++currentIndex;
        }


    }
}
