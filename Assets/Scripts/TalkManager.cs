using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string> tutorialData;

    private void Awake()
    {
        tutorialData = new Dictionary<int, string>();
        SetData();
    }

    //Ʃ�丮�� ��ȭ ������ ����
    public void SetData()
    {
        tutorialData.Add(1, "�� �̵� - ��, �Ʒ� �̵� - ��, ���� �̵� - ��, ������ �̵� - ��");
        tutorialData.Add(2, "�⺻���� - Ctrl, ȸ�� ��� - Space Bar");
        tutorialData.Add(3, "ȭ��ǥ�� ���� ���� �ִ� �Ķ��� ������ �̵��ϼ���");
        tutorialData.Add(4, "�տ� ���̴� �㸦 �����غ�����");
        tutorialData.Add(5, "���� ������ �����帮�ڽ��ϴ�.");
    }

    //������ ��ȯ
    public string GetData(int talkIndex)
    {
        if (tutorialData[talkIndex] != null)
            return tutorialData[talkIndex];
        else
            return null;
    }
}
