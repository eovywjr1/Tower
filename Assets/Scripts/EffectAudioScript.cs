using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAudioScript : MonoBehaviour
{
    public static float value = 1f;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
