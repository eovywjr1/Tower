using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject boss;

    PlayerScript playerScript;

    //�����ϱ�
    public void StartGame()
    {
        SceneManager.LoadScene("1F");
    }

    //�����ϱ�
    public void ExitGame()
    {
        Application.Quit();
    }

    void MapTransfer()
    {
        //1�� Ŭ����
        if (boss != null)
        {
            if (SceneManager.GetActiveScene().name == "1F")
            {
                BossBaseScript bossBaseScript = FindObjectOfType<BossBaseScript>();

                if (bossBaseScript.isDie)
                {
                    SceneManager.LoadScene("2F");

                    playerScript.power++;
                }
            }
        }
    }
}
