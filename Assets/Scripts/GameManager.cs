using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image talkImage;

    public BossBaseScript bossBaseScript;
    PlayerScript playerScript;

    private void Awake()
    {
        playerScript = FindObjectOfType<PlayerScript>();
    }

    private void Update()
    {
        MapChange();
    }

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

    void MapChange()
    {
        //1�� Ŭ����
        if (bossBaseScript != null)
        {
            if (SceneManager.GetActiveScene().name == "1F")
            {
                if (bossBaseScript.isDie)
                {
                    playerScript.power++;
                    playerScript.isAvoidancePossible = true;

                    talkImage.gameObject.SetActive(true);
                }

                if (playerScript.tutorialIndex >= 5)
                    SceneManager.LoadScene("2F");
            }
        }
    }
}