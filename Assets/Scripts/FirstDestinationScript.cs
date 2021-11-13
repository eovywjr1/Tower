using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstDestinationScript : MonoBehaviour
{
    public Image image;
    public GameObject boss;

    //Ʃ�丮�� ���� Ʈ���� ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (boss != null)
            {
                if (image != null)
                    image.gameObject.SetActive(true);
                boss.SetActive(true);

            }
            this.gameObject.SetActive(false);
        }
    }
}
