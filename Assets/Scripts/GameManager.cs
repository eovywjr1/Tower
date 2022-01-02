using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Image listScene;

    //�����ϱ�
    public void StartGame()
    {
        listScene.gameObject.SetActive(true);
    }

    public void Load1F()
    {
        SceneManager.LoadScene("1F");
    }
    public void Load2F()
    {
        SceneManager.LoadScene("2F");
    }
    public void Load3F()
    {
        SceneManager.LoadScene("3F");
    }

    //�����ϱ�
    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
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