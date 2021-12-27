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

    //튜토리얼 대화 데이터 생성
    public void SetData()
    {
        tutorialData.Add(10, new string[] {"위 이동 - ↑, 아래 이동 - ↓, 왼쪽 이동 - ←, 오른쪽 이동 - →\n기본공격 - Ctrl", "화살표를 따라 위에 있는 파란색 원으로 이동하세요" });
        tutorialData.Add(20, new string[] { "앞에 보이는 쥐를 공격해보세요" });
        tutorialData.Add(30, new string[] { "다음 층으로 보내드리겠습니다." });
        tutorialData.Add(40, new string[] { "회피 기술을 획득했습니다.\n회피 - Space Bar", "2층 소 시작하겠습니다." });
        tutorialData.Add(50, new string[] { "다음 층으로 보내드리겠습니다." });
    }

    //데이터 반환
    public string GetData(int id, int talkIndex)
    {
        if (talkIndex != tutorialData[id].Length)
            return tutorialData[id][talkIndex];
        else
            return null;
    }
}
