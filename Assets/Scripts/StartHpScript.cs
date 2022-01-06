using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHpScript : MonoBehaviour
{
    public int bossHp, playerHp;

    void Start()
    {
        DontDestroyOnLoad(gameObject);    
    }
}
