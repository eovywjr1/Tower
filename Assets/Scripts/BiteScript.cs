using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteScript : MonoBehaviour
{
    public TigerScript tigerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("!");
            tigerScript.Bite();
        }
    }
}
