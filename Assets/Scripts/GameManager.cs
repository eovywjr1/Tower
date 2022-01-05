using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Image listScene;
    public Image status;
    public InputField bossHpInput;
    public InputField playerHpInput;

    //�����ϱ�
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
                bossHpInput.text = "5";
                playerHpInput.text = "5";
                break;
            case "3F":
                bossHpInput.text = "10";
                playerHpInput.text = "5";
                break;
        }
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void Load1F()
    {
        InputDefault("1F");
        SceneManager.LoadScene("1F");
    }
    public void Load2F()
    {
        InputDefault("2F");
        SceneManager.LoadScene("2F");
    }
    public void Load3F()
    {
        InputDefault("3F");
        SceneManager.LoadScene("3F");
    }

    //�����ϱ�
    public void ExitGame()
    {
        Application.Quit();
    }

    public void MapChange()
    {
        string currentMapName = SceneManager.GetActiveScene().name;

        switch (currentMapName)
        {
            //1�� Ŭ����
            case "1F":
                SceneManager.LoadScene("2F");
                break;
            case "2F":
                SceneManager.LoadScene("3F");
                break;
        }
    }
}