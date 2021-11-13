using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Image talkImage;

    public Text talkText;
    public Text bossHpText;
    public Text playerHpText;

    BossBaseScript bossBaseScript;
    PlayerScript playerScript;
    TalkManager talkManager;

    private void Awake()
    {
        playerScript = FindObjectOfType<PlayerScript>();
        talkManager = FindObjectOfType<TalkManager>();
    }

    private void Update()
    {
        TutorialTalkShow();
        BossHpShow();
        PlyerHpShow();
    }

    //1층 튜토리얼 talk UI show
    void TutorialTalkShow()
    {
        if (talkImage != null)
        {
            if (talkImage.gameObject.activeSelf)
            {
                playerScript.isTalk = true;

                //대화 텍스트 표시
                talkText.text = talkManager.GetData(playerScript.tutorialIndex);

                //스페이스 바 누르면 다음 대화 표시 or 끄기
                if (Input.GetKeyDown(KeyCode.G))
                {
                    if (playerScript.tutorialIndex > 1)
                    {
                        talkImage.gameObject.SetActive(false);
                        playerScript.isTalk = false;
                    }

                    playerScript.tutorialIndex++;
                    talkText.text = talkManager.GetData(playerScript.tutorialIndex);
                }
            }
        }
    }

    //보스 hp bar 표시
    void BossHpShow()
    {
        if (bossHpText.gameObject.activeSelf)
        {
            if (bossBaseScript == null)
                bossBaseScript = FindObjectOfType<BossBaseScript>();

            if (!bossBaseScript.isDie)
                bossHpText.text = bossBaseScript.currentHp + " / " + bossBaseScript.maxHp;
        }
    }

    void PlyerHpShow()
    {
        playerHpText.text = "♥ x " + playerScript.currentHp;
    }
}