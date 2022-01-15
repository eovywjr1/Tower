using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteTargetFindScript : MonoBehaviour
{
    public TigerScript tigerScript;
    public SnakeScript snakeScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (tigerScript != null)
                tigerScript.Bite();
            if (snakeScript != null)
                snakeScript.Bite();
        }
    }
}
