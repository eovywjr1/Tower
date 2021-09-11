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

    //새로하기
    public void StartGame()
    {
        SceneManager.LoadScene("1F");

        playerScript = FindObjectOfType<PlayerScript>();

        SetData();
    }

    //종료하기
    public void ExitGame()
    {
        Application.Quit();
    }

    //튜토리얼 대화 데이터 생성
    public void SetData()
    {
        tutorialData.Add(1, "방향키로 검은색 부분까지 이동하세요");
        tutorialData.Add(2, "앞에 보이는 쥐를 공격해보세요");
        tutorialData.Add(3, "다음 층으로 보내드리겠습니다.");
    }

    //대화창 활성화 시 데이터 표시
    private void OnEnable()
    {
        text.text = tutorialData[playerScript.tutorialIndex];
        playerScript.tutorialIndex++;
    }
}
