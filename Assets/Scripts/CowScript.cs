using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowScript : BossBaseScript
{
    public int random;
    public int feedIndex;

    public float speed;

    public GameObject line;
    public GameObject circle;
    public GameObject dung;
    public GameObject[] feed;
    public LineRenderer[] directionRenderer;
    public LineRenderer lineRenderer;
    public Vector2 playerPosition;

    public bool isPattern;
    public bool isDash;
    public bool isPushDown;
    public bool isGoToFeed;

    public override void Update()
    {
        if (isStart && !isPattern)
        {
            isPattern = true;
            StartCoroutine(PatternCooltime());
        }

        JudgeDie();
        if (isDash)
            Dash();

        //대쉬 후 위치 같으면 종료
        if (isDash && transform.position.x == playerPosition.x
            && transform.position.y == playerPosition.y)
        {
            isPattern = false;
            isDash = false;
            gameObject.layer = 6;
        }

        if(isPattern && isGoToFeed)
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

    //돌진 전 선 긋기
    void DrawDashLine()
    {
        line.SetActive(true);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, playerPosition);
        StartCoroutine(StartDashDelay());
    }

    void CircleActive()
    {
        circle.transform.position = playerPosition;
        circle.SetActive(true);
        StartCoroutine(PushDownCoolTime());
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
        StartCoroutine(PushDownEndTime());
    }

    void Dung()
    {
        isBossDamagePossible = false;
        isPattern = false;
        dung.SetActive(true);
        StartCoroutine(DungEndTime());
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

    void StartFeed()
    {
        for (int i = 0; i < feed.Length; i++)
            feed[i].SetActive(true);
        StartCoroutine(FeedStartTime());
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

    //선 그은 후 돌진 쿨타임
    IEnumerator StartDashDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        line.SetActive(false);
        isDash = true;
    }

    IEnumerator PatternCooltime()
    {
        yield return new WaitForSecondsRealtime(3f);

        playerPosition = playerScript.transform.position;

        if (random != 3)
            random = Random.Range(0, 4);
        else
            random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                DrawDashLine();
                break;
            case 1:
                CircleActive();
                break;
            case 2:
                StartFeed();
                break;
            case 3:
                Dung();
                break;
        }
    }

    IEnumerator PushDownCoolTime()
    {
        yield return new WaitForSecondsRealtime(1f);

        PushDown();
    }

    IEnumerator PushDownEndTime()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        EndDown();
    }

    IEnumerator DungEndTime()
    {
        yield return new WaitForSecondsRealtime(10f);

        EndDung();
    }

    IEnumerator FeedStartTime()
    {
        yield return new WaitForSecondsRealtime(5f);

        isGoToFeed = true;
    }
}
