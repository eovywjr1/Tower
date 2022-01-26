using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    PlayerScript playerScript;
    public static Vector3 playerPoistion;

    private void Start()
    {
        playerScript = FindObjectOfType<PlayerScript>();
    }

    void Update()
    {
        playerPoistion = playerScript.transform.position;
    }
}
