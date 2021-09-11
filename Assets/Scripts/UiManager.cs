using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    Dictionary<int, string> tutorialData;

    Text text;

    PlayerScript playerScript;

    //�����ϱ�
    public void StartGame()
    {
        SceneManager.LoadScene("1F");

        playerScript = FindObjectOfType<PlayerScript>();

        SetData();
    }

    //�����ϱ�
    public void ExitGame()
    {
        Application.Quit();
    }

    //Ʃ�丮�� ��ȭ ������ ����
    public void SetData()
    {
        tutorialData.Add(1, "����Ű�� ������ �κб��� �̵��ϼ���");
        tutorialData.Add(2, "�տ� ���̴� �㸦 �����غ�����");
        tutorialData.Add(3, "���� ������ �����帮�ڽ��ϴ�.");
    }

    //��ȭâ Ȱ��ȭ �� ������ ǥ��
    private void OnEnable()
    {
        text.text = tutorialData[playerScript.tutorialIndex];
        playerScript.tutorialIndex++;
    }
}
