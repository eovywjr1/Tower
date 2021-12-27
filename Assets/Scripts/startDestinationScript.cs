using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startDestinationScript : MonoBehaviour
{
    public UiManager uiManager;

    public GameObject talk;
    public GameObject hp;
    public GameObject boss;

    //튜토리얼 원형 트리거 시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (boss != null)
            {
                if (talk != null)
                {
                    talk.SetActive(true);
                    uiManager.FirstTalkShow();
                }

            }
            this.gameObject.SetActive(false);
        }
    }
}
