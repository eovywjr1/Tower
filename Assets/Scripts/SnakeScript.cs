using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : BossBaseScript
{
    public GameObject findTarget, bite, tail, tighten;
    public bool isBite, isTail, isMove, isTighten;
    public int moveSpeed;
    public Vector3 direction;

    private void Start()
    {
        StartCoroutine(PatternCooltime());
    }

    private void Update()
    {
        if (isMove)
        {
            Move();

            if (transform.position == direction)
                EndMove();
        }
    }

    void FindTargetStart()
    {
        findTarget.SetActive(true);
        findTarget.transform.position = transform.position;
        StartCoroutine(PatternStartCorutine(Color.white, 1f, FindTargetStop));
    }

    void FindTargetStop()
    {
        findTarget.SetActive(false);

        StartCoroutine(PatternCooltime());
    }

    public void Bite()
    {
        findTarget.SetActive(false);
        bite.SetActive(true);
        StartCoroutine(ExecuteMethodCorutine(0.5f, BiteStop));
    }

    void BiteStop()
    {
        isBite = false;
        bite.SetActive(false);
    }

    public void Tail()
    {
        findTarget.SetActive(false);
        tail.SetActive(true);
        StartCoroutine(ExecuteMethodCorutine(0.5f, StopTail));
    }

    void StopTail()
    {
        isTail = false;
        tail.SetActive(false);
    }

    public void Tighten()
    {
        findTarget.SetActive(false);
        tighten.SetActive(true);
        StartCoroutine(ExecuteMethodCorutine(0.5f, StopTighten));
    }

    void StopTighten()
    {
        isTighten = false;
        tighten.SetActive(false);
    }

    void PrepareMove()
    {
        int x = Random.Range(-20, 20), y = Random.Range(-20, 20);

        direction = new Vector2(x, y);
        isMove = true;
        isBossDamagePossible = false;
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, Time.deltaTime * moveSpeed);
    }

    void EndMove()
    {
        isMove = false;
        isBossDamagePossible = true;

        StartCoroutine(PatternCooltime());
    }

    IEnumerator PatternCooltime()
    {
        yield return new WaitForSecondsRealtime(3f);

        playerPosition = playerScript.transform.position;

        patternIndex = Random.Range(0, 4);
        switch (patternIndex)
        {
            case 0:
                isBite = true;
                StartCoroutine(PatternStartCorutine(Color.red, 1f, FindTargetStart));
                break;
            case 1:
                isTail = true;
                StartCoroutine(PatternStartCorutine(Color.red, 1f, FindTargetStart));
                break;
            case 2:
                isTighten = true;
                StartCoroutine(PatternStartCorutine(Color.red, 1f, FindTargetStart));

                break;
            case 3:
                PrepareMove();
                break;
        }
    }
}
