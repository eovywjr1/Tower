using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteScript : MonoBehaviour
{
    public PlayerScript playerScript;

    void Update()
    {
        Vector3 playerPosition = playerScript.transform.position;

        transform.position = new Vector3(playerPosition.x, playerPosition.y + 1.5f);
    }
}
