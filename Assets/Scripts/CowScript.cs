using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowScript : BossBaseScript
{
    public float speed;

    public GameObject line;
    public LineRenderer lineRenderer;
    public Vector2 playerPosition;

    public bool isPattern;
    public bool isDash;

    public override void Update()
    {
        if(isStart && !isPattern)
            StartCoroutine(PatternCooltime());

        JudgeDie();
        if (isDash)
            Dash();

        //�뽬 �� ��ġ ������ ����
        if (transform.position.x == playerPosition.x
            && transform.position.y == playerPosition.y)
        {
            isPattern = false;
            isDash = false;
            this.gameObject.layer = 6;
        }
    }

    //����
    void Dash()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, Time.deltaTime * speed);
        this.gameObject.layer = 7;
    }

    //���� �� �� �߱�
    void DrawDashLine()
    {
        Debug.Log("1");
        playerPosition = playerScript.transform.position;
        line.SetActive(true);
        lineRenderer.SetPosition(0, new Vector2(transform.position.x, transform.position.y - 4));
        lineRenderer.SetPosition(1, playerPosition);
        StartCoroutine(StartDashDelay());
    }

    //�� ���� �� ���� ��Ÿ��
    IEnumerator StartDashDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        line.SetActive(false);
        isDash = true;
    }

    IEnumerator PatternCooltime()
    {
        isPattern = true;

        yield return new WaitForSecondsRealtime(3f);

        DrawDashLine();
    }
}
