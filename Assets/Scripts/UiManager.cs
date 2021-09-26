using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Image talkImage;

    public Text talkText;
    public Text hpText;

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
        HpShow();
    }

    //1�� Ʃ�丮�� talk UI show
    void TutorialTalkShow()
    {
        if (talkImage != null)
        {
            if (talkImage.gameObject.activeSelf)
            {
                playerScript.isTalk = true;

                //��ȭ �ؽ�Ʈ ǥ��
                talkText.text = talkManager.GetData(playerScript.tutorialIndex);

                //�����̽� �� ������ ���� ��ȭ ǥ�� or ����
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

    //hp bar ǥ��
    void HpShow()
    {
        if (hpText.gameObject.activeSelf)
        {
            if (bossBaseScript == null)
                bossBaseScript = FindObjectOfType<BossBaseScript>();

            if (!bossBaseScript.isDie)
                hpText.text = bossBaseScript.currentHp + " / " + bossBaseScript.maxHp;
        }
    }
}