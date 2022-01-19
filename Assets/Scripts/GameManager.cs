using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public int bossHp, playerHp;
    public string loadMapName;
    public StartHpScript startHpScript;
    public Image listScene;
    public Image status;
    public InputField bossHpInput;
    public InputField playerHpInput;

    //새로하기
    public void StartGame()
    {
        listScene.gameObject.SetActive(true);
    }

    void InputDefault(string mapName)
    {
        status.gameObject.SetActive(true);

        switch (mapName)
        {
            case "1F":
            case "2F":
                bossHpInput.text = "50";
                playerHpInput.text = "5";
                break;
            case "3F":
                bossHpInput.text = "100";
                playerHpInput.text = "7";
                break;
            case "4F":
                bossHpInput.text = "150";
                playerHpInput.text = "9";
                break;
            case "5F":
                bossHpInput.text = "200";
                playerHpInput.text = "11";
                break;
            case "6F":
                bossHpInput.text = "250";
                playerHpInput.text = "13";
                break;
        }
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void SetHp(string mapName)
    {
        loadMapName = mapName;
        InputDefault(mapName);
    }

    public void LoadScene()
    {
        startHpScript.bossHp = int.Parse(bossHpInput.text);
        startHpScript.playerHp = int.Parse(playerHpInput.text);

        SceneManager.LoadScene(loadMapName);
    }

    //종료하기
    public void ExitGame()
    {
        Application.Quit();
    }

    public void MapChange()
    {
        startHpScript.bossHp = bossHp;
        startHpScript.playerHp = playerHp;

        SceneManager.LoadScene(loadMapName);
    }

    public void CloseStatus()
    {
        status.gameObject.SetActive(false);
    }
}