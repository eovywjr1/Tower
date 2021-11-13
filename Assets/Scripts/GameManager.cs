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
        
        if (bossBaseScript != null)
        {
            if (bossBaseScript.isDie)
            {
                StartCoroutine(EndCoolTime());
                //1층 클리어
                if (SceneManager.GetActiveScene().name == "1F")
                {
                    if (playerScript.tutorialIndex >= 5)
                    {
                        playerScript.power++;
                        playerScript.isAvoidancePossible = true;
                        playerScript.transform.position = new Vector2(0, -7);
                        SceneManager.LoadScene("2F");
                    }
                }
            }
        }
    }

    IEnumerator EndCoolTime()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        talkImage.gameObject.SetActive(true);
    }
}