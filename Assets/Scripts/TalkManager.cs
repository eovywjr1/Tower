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
        tutorialData.Add(60, new string[] { "3�� ȣ���� �����ϰڽ��ϴ�." });
        tutorialData.Add(70, new string[] { "���� ������ �����帮�ڽ��ϴ�." });
        tutorialData.Add(80, new string[] { "4�� �䳢 �����ϰڽ��ϴ�." });
        tutorialData.Add(90, new string[] { "���� ������ �����帮�ڽ��ϴ�." });
        tutorialData.Add(100, new string[] { "5�� �� �����ϰڽ��ϴ�." });
        tutorialData.Add(110, new string[] { "���� ������ �����帮�ڽ��ϴ�." });
        tutorialData.Add(120, new string[] { "6�� �� �����ϰڽ��ϴ�." });
        tutorialData.Add(130, new string[] { "���� ���� �����ϴ�." });
        tutorialData.Add(140, new string[] { "7�� �� �����ϰڽ��ϴ�." });
        tutorialData.Add(150, new string[] { "���� ���� �����ϴ�." });

    }

    //������ ��ȯ
    public string GetData(int id, int talkIndex)
    {
        if (talkIndex < tutorialData[id].Length)
            return tutorialData[id][talkIndex];
        else
            return null;
    }
}
