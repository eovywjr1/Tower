using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstDestinationScript : MonoBehaviour
{
    public Image image;
    public GameObject firstBoss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.name == "FirstDestination")
        {
            Debug.Log(collision.tag);
            if (collision.CompareTag("Player"))
            {
                if (image != null && firstBoss != null)
                {
                    image.gameObject.SetActive(true);
                    firstBoss.SetActive(true);
                }
            }
        }
    }
}
