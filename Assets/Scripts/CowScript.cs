using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowScript : BossBaseScript
{
    public int feedIndex;

    public float speed;

    public GameObject line, circle, dung;
    public GameObject[] feed;
    public LineRenderer[] directionRenderer;
    public LineRenderer lineRenderer;

    public bool isPattern, isDash, isPushDown, isGoToFeed;

    public void Update()
    {
        if (isStart && !isPattern)
        {
            isPattern = true;
            StartCoroutine(PatternCooltime());
        }

        JudgeDie();

        if (isDash)
        {
            Dash();

            //대쉬 후 위치 같으면 종료
            if (transform.position == playerPosition)
            {
                isPattern = false;
                isDash = false;
                gameObject.layer = 6;
            }
        }

        if(isGoToFeed)
        {
            if (feedIndex > 3)
                EndFeed();
            else if (feed[feedIndex].activeSelf)
                GoToFeed(feedIndex);
            else
                feedIndex++;
        }
    }

    //돌진
    void Dash()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, Time.deltaTime * speed);
        gameObject.layer = 7;
    }

    void PossibleDash()
    {
        line.SetActive(false);
        isDash = true;
    }

    //돌진 전 선 긋기
    void DrawDashLine()
    {
        line.SetActive(true);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, playerPosition);
        StartCoroutine(ExecuteMethodCorutine(0.5f, PossibleDash));
    }

    void CircleActive()
    {
        circle.transform.position = playerPosition;
        circle.SetActive(true);
        StartCoroutine(ExecuteMethodCorutine(1f, PushDown));
    }

    void PushDown()
    {
        circle.SetActive(false);
        gameObject.transform.position = circle.transform.position;
        gameObject.layer = 7;

        for (int i = 0; i < directionRenderer.Length; i++)
        {
            directionRenderer[i].gameObject.SetActive(true);
            directionRenderer[i].gameObject.transform.position = transform.position;
            directionRenderer[i].SetPosition(0, transform.position);
            switch (i)
            {
                case 0:
                    directionRenderer[i].SetPosition(1, new Vector2(transform.position.x, transform.position.y + 10));
                    break;
                case 1:
                    directionRenderer[i].SetPosition(1, new Vector2(transform.position.x, transform.position.y - 10));
                    break;
                case 2:
                    directionRenderer[i].SetPosition(1, new Vector2(transform.position.x + 10, transform.position.y));
                    break;
                case 3:
                    directionRenderer[i].SetPosition(1, new Vector2(transform.position.x - 10, transform.position.y));
                    break;
            }
        }
        StartCoroutine(ExecuteMethodCorutine(0.5f, EndDown));
    }

    void Dung()
    {
        isBossDamagePossible = false;
        isPattern = false;
        dung.SetActive(true);
        StartCoroutine(ExecuteMethodCorutine(10f, EndDung));
    }

    void EndDung()
    {
        isBossDamagePossible = true;
        dung.SetActive(false);
    }

    void EndDown()
    {
        for (int i = 0; i < directionRenderer.Length; i++)
            directionRenderer[i].gameObject.SetActive(false);
        isPattern = false;
        gameObject.layer = 6;
    }

    void ReadyFeed()
    {
        for (int i = 0; i < feed.Length; i++)
            feed[i].SetActive(true);
        StartCoroutine(ExecuteMethodCorutine(5f, StartFeed));
    }

    void StartFeed()
    {
        isGoToFeed = true;
    }

    void GoToFeed(int index)
    {
        transform.position = Vector2.MoveTowards(transform.position, feed[index].transform.position, Time.deltaTime * speed);
    }
       
    void EndFeed()
    {
        isPattern = false;
        isGoToFeed = false;
        feedIndex = 0;
    }

    public void Recovery(int hp)
    {
        currentHp += hp;
        if (maxHp < currentHp)
            currentHp = maxHp;
    }

    IEnumerator PatternCooltime()
    {
        yield return new WaitForSecondsRealtime(3f);

        if (!playerScript.isDie)
        {
            playerPosition = playerScript.transform.position;

            int startindex = 0;

            if (currentHp == maxHp)
                startindex = 1;
            if (patternIndex != 3)
                patternIndex = Random.Range(startindex, 4);
            else
                patternIndex = Random.Range(startindex, 3);
            switch (patternIndex)
            {
                case 0:
                    StartFeed();
                    break;
                case 1:
                    CircleActive();
                    break;
                case 2:
                    DrawDashLine();
                    break;
                case 3:
                    Dung();
                    break;
            }
        }
    }
}
