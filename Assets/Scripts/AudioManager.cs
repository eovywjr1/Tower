using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    BackGroundAudioScript backGroundAudioScript;
    public static AudioSource backGroundAudioSource;
    public Text audioOnOffText;
    public Image option;
    public Slider slider;
        
    void Start()
    {
        backGroundAudioScript = FindObjectOfType<BackGroundAudioScript>();
        backGroundAudioSource = backGroundAudioScript.GetComponent<AudioSource>();
    }

    public void OnOff()
    {
        switch (audioOnOffText.text)
        {
            case "On":
                audioOnOffText.text = "Off";
                backGroundAudioSource.enabled = true;
                break;
            case "Off":
                audioOnOffText.text = "On";
                backGroundAudioSource.enabled = false;
                break;
        }
    }

    public void Change()
    {
        backGroundAudioSource.volume = slider.value;
    }
}