using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Image image;
    public Text text;

    PlayerScript playerScript;
    TalkManager talkManager;

    private void Awake()
    {
        playerScript = FindObjectOfType<PlayerScript>();
        talkManager = FindObjectOfType<TalkManager>();
    }

    private void Update()
    {
        if (image.gameObject.activeSelf)
        {
            playerScript.isTalk = true;

            //��ȭ �ؽ�Ʈ ǥ��
            text.text = talkManager.GetData(playerScript.tutorialIndex);

            //�����̽� �� ������ ���� ��ȭ ǥ�� or ����
            if (image != null && image.gameObject.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (playerScript.tutorialIndex != 1)
                    {
                        image.gameObject.SetActive(false);
                        playerScript.isTalk = false;
                    }
                    playerScript.tutorialIndex++;
                    text.text = talkManager.GetData(playerScript.tutorialIndex);
                }
            }
        }
    }
}
