using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowScript : BossBaseScript
{
    PlayerScript playerScript;

    private void Awake()
    {
        playerScript = FindObjectOfType<PlayerScript>();
    }

    private void Update()
    {
        
    }

    void Dash()
    {
        Vector2 playerPosition = playerScript.transform.position;


    }
}
