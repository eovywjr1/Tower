using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowScript : BossBaseScript
{
    public float speed;

    public GameObject line;
    public LineRenderer lineRenderer;
    public Vector2 playerPosition;

    public bool isDash;

    private void Start()
    {
        DrawDashLine();
    }

    private void Update()
    {
        if (isDash)
            Dash();

        //대쉬 후 위치 같으면 종료
        if (transform.position.x == playerPosition.x
            && transform.position.y == playerPosition.y)
        {
            isDash = false;
            this.gameObject.layer = 6;
        }
    }

    //돌진
    void Dash()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, Time.deltaTime * speed);
        this.gameObject.layer = 7;
    }

    //돌진 전 선 긋기
    void DrawDashLine()
    {
        playerPosition = playerScript.transform.position;
        line.SetActive(true);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, playerPosition);
        StartCoroutine(StartDashDelay());
    }

    //선 그은 후 돌진 쿨타임
    IEnumerator StartDashDelay()
    {
        yield return new WaitForSecondsRealtime(1f);

        line.SetActive(false);
        isDash = true;
    }
}
