using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject boss;

    PlayerScript playerScript;

    //새로하기
    public void StartGame()
    {
        SceneManager.LoadScene("1F");
    }

    //종료하기
    public void ExitGame()
    {
        Application.Quit();
    }

    void MapTransfer()
    {
        //1층 클리어
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
