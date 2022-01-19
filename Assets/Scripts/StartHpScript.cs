using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartHpScript : MonoBehaviour
{
    public int bossHp, playerHp;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
