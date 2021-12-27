using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> tutorialData;

    private void Awake()
    {
        tutorialData = new Dictionary<int, string[]>();
        SetData();
    }

    //Ʃ�丮�� ��ȭ ������ ����
    public void SetData()
    {
        tutorialData.Add(10, new string[] {"�� �̵� - ��, �Ʒ� �̵� - ��, ���� �̵� - ��, ������ �̵� - ��\n�⺻���� - Ctrl", "ȭ��ǥ�� ���� ���� �ִ� �Ķ��� ������ �̵��ϼ���" });
        tutorialData.Add(20, new string[] { "�տ� ���̴� �㸦 �����غ�����" });
        tutorialData.Add(30, new string[] { "���� ������ �����帮�ڽ��ϴ�." });
        tutorialData.Add(40, new string[] { "ȸ�� ����� ȹ���߽��ϴ�.\nȸ�� - Space Bar", "2�� �� �����ϰڽ��ϴ�." });
        tutorialData.Add(50, new string[] { "���� ������ �����帮�ڽ��ϴ�." });
    }

    //������ ��ȯ
    public string GetData(int id, int talkIndex)
    {
        if (talkIndex != tutorialData[id].Length)
            return tutorialData[id][talkIndex];
        else
            return null;
    }
}
