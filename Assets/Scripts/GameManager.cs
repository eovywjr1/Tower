using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Image talkImage;

    public BossBaseScript bossBaseScript;
    PlayerScript playerScript;

    private void Awake()
    {
        playerScript = FindObjectOfType<PlayerScript>();
    }

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

    public void MapChange()
    {
        string currentMapName = SceneManager.GetActiveScene().name;

        switch (currentMapName)
        {
            //1층 클리어
            case "1F":
                playerScript.power++;
                playerScript.isAvoidancePossible = true;
                playerScript.transform.position = new Vector2(0, -7);
                SceneManager.LoadScene("2F");
                break;
            case "2F":
                playerScript.transform.position = new Vector2(0, -7);
                SceneManager.LoadScene("3F");
                break;
        }
    }
}