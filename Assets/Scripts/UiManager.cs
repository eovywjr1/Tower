using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public int talkIndex;

    public Image talkImage;

    public Text talkText;
    public Text bossHpText;
    public Text playerHpText;

    public GameObject hpGroup;

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
        //스페이스 바 누르면 다음 대화 표시 or 끄기
        if (talkImage.gameObject.activeSelf && Input.GetKeyDown(KeyCode.G))
            NextTalkShow();
        BossHpShow();
        PlyerHpShow();
    }

    public void FirstTalkShow()
    {
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

            return;
        }

        talkText.text = talkData;
    }

    //보스 hp bar 표시
    void BossHpShow()
    {
        if (hpGroup.activeSelf)
        {
            if (bossBaseScript == null)
                bossBaseScript = FindObjectOfType<BossBaseScript>();

            if (!bossBaseScript.isDie && hpGroup.activeSelf)
                bossHpText.text = bossBaseScript.currentHp + " / " + bossBaseScript.maxHp;
        }
    }

    void PlyerHpShow()
    {
        playerHpText.text = "♥ x " + playerScript.currentHp;
    }
}