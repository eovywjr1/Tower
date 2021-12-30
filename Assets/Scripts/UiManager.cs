using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class UiManager : MonoBehaviour
{
    public int talkIndex;

    public Image talkImage;

    public Text talkText;
    public Text bossHpText;
    public Text playerHpText;

    public GameObject hpGroup;

    public GameManager gameManager;
    public BossBaseScript bossBaseScript;
    PlayerScript playerScript;
    TalkManager talkManager;

    private void Awake()
    {
        talkIndex = 0;
        playerScript = FindObjectOfType<PlayerScript>();
        talkManager = FindObjectOfType<TalkManager>();
        FirstTalkShow();
    }

    private void Update()
    {
        if(talkImage.gameObject.activeSelf && Input.GetKeyDown(KeyCode.G))
            NextTalkShow();
        BossHpShow();
        PlyerHpShow();
    }

    public void FirstTalkShow()
    {
        talkImage.gameObject.SetActive(true);
        playerScript.isTalk = true;
        talkText.text = talkManager.GetData(playerScript.talkId, talkIndex++);
    }

    //talk UI show
    public void NextTalkShow()
    {
        string talkData = talkManager.GetData(playerScript.talkId, talkIndex++);

        if (talkData == null)
        {
            talkImage.gameObject.SetActive(false);
            playerScript.isTalk = false;
            talkIndex = 0;
            playerScript.talkId = playerScript.talkId - playerScript.talkId % 10 + 10;

            if (playerScript.talkId % 20 == 10)
            {
                hpGroup.SetActive(true);
                bossBaseScript.gameObject.SetActive(true);
                bossBaseScript.isStart = true;
            }

            else if (playerScript.talkId > 20 && playerScript.talkId % 20 == 0)
                gameManager.MapChange();

            return;
        }

        talkText.text = talkData;
    }

    //보스 hp bar 표시
    void BossHpShow()
    {
        if (hpGroup.activeSelf)
        {
            if (!bossBaseScript.isDie)
                bossHpText.text = bossBaseScript.currentHp + " / " + bossBaseScript.maxHp;
            else
                bossHpText.text = 0 + " / " + bossBaseScript.maxHp;
        }
    }

    void PlyerHpShow()
    {
        playerHpText.text = "♥ x " + playerScript.currentHp;
    }

    public void StartEndCoolTime()
    {
        StartCoroutine(EndCoolTime());
    }

    IEnumerator EndCoolTime()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        FirstTalkShow();
    }
}