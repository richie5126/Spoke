using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class AudioStaticAccessor : MonoBehaviour {

    // Use this for initialization
    public AudioMixer mMixer;
    public string channelName;
    Slider slider;
    public void SetAudioVolume()
    {
        if (slider != null && mMixer != null)
        {
            mMixer.SetFloat(channelName, slider.value);
            PlayerPrefs.SetFloat(channelName, slider.value);
        }

    }
	void Start ()
    {
        slider = GetComponent<Slider>();
        if(mMixer != null)
        mMixer.SetFloat(channelName, PlayerPrefs.GetFloat(channelName));


        if (slider != null)
            slider.value = PlayerPrefs.GetFloat(channelName);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
