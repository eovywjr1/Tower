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

    //튜토리얼 대화 데이터 생성
    public void SetData()
    {
        tutorialData.Add(1, "위 이동 - ↑, 아래 이동 - ↓, 왼쪽 이동 - ←, 오른쪽 이동 - →");
        tutorialData.Add(2, "화살표를 따라 위에 있는 파란색 원으로 이동하세요");
        tutorialData.Add(3, "앞에 보이는 쥐를 공격해보세요");
        tutorialData.Add(4, "다음 층으로 보내드리겠습니다.");
    }

    //데이터 반환
    public string GetData(int talkIndex)
    {
        if (tutorialData[talkIndex] != null)
            return tutorialData[talkIndex];
        else
            return null;
    }
}
