using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderScript : MonoBehaviour
{
    Slider slider;

    void Start()
    {
        DontDestroyOnLoad(this);

        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = AudioManager.backGroundAudioSource.volume;
    }
}