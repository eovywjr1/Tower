using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstDestinationScript : MonoBehaviour
{
    public Image image;
    public GameObject firstBoss;

    //Ʃ�丮�� ���� Ʈ���� ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);

            if (image != null && firstBoss != null)
            {
                image.gameObject.SetActive(true);
                firstBoss.SetActive(true);
            }
        }
    }
}
