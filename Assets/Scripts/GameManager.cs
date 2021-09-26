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

    void MapChange()
    {
        //1층 클리어
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