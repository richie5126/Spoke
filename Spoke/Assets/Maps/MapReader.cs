using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class MapReader : MonoBehaviour {
    // Use this for initialization
    public string filepath;

    int channels;
    float bpm;
    float offset;
    float approachrate;
    List<byte> notes = new List<byte>();
    string notedata;

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
        string text = File.ReadAllText(filepath);

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
                case "notes":
                    notedata = strValues[i + 1];
                    notedata = notedata.Replace(" ", string.Empty);
                    Debug.Log(notedata);
                    break;
                default:
                    break;
            }
        }
        Debug.Log("Channels: " + channels);
        Debug.Log("BPM: " + bpm);
        Debug.Log("Approach: " + approachrate);
        secondsPerSixteenth = 30.0f / bpm;
        

        foreach (char x in notedata)
        {
            byte t = (byte)(x - '0');
            notes.Add(t);
        }

        Debug.Log("Note total: " + notes.Count);


    }
    int val = 0;

    bool audioToggle = false;
    bool audioToggle2 = false;
    float spawnTimer = 0.0f;

    void Update () {
        audiotimer = musicPlayer.time;
        spawnTimer = audiotimer + MoveInward.timeToMove;
        if (((audiotimer + (0.00001 * offset)) % (secondsPerSixteenth * 1.0f)) <= 0.1f)
        {

            if (!audioToggle)
            {
                debugMetronome.Stop();
                debugMetronome.Play();
                Debug.Log(val++);
                audioToggle = !audioToggle;

            }
        }
        else audioToggle = false;


        if (((spawnTimer + (0.00001 * offset)) % (secondsPerSixteenth * 1.0f)) <= 0.1f)
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
            x.GetComponent<MoveInward>().targetPosition = player.gameObject.transform.position + (rot * Vector3.right * player.radius * 0.6f);
            ++currentIndex;
        }


    }
}
