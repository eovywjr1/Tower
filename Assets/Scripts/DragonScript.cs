using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : BossBaseScript
{
    int dashSpeed = 5, dashDirection;
    bool isDash;
    public Vector3 originPosition;

    public GameObject fire, fireGround, fireGroundPrefab;
    public LineRenderer fireGroundLineRenderer;
    public BoxCollider2D fireGroundBoxCollider;

    private void Start()
    {
        StartCoroutine(PatternCooltime());
    }

    private void Update()
    {
        if (isDash)
        {
            Dash();

            //대쉬 후 위치 같으면 종료
            if (transform.position == playerPosition)
                EndDash();
        }
    }
    
    void PrePareDash()
    {
        //4방향 랜덤
        dashDirection = Random.Range(0, 2);
        switch (dashDirection)
        {
            case 0:
                playerPosition = new Vector2(Random.Range(-20, 20), transform.position.y);
                break;
            case 1:
                playerPosition = new Vector2(transform.position.x, Random.Range(-20, 20));
                break;
        }

        fireGround = Instantiate(fireGroundPrefab);
        fireGroundLineRenderer = fireGround.GetComponent<LineRenderer>();
        fireGroundBoxCollider = fireGround.GetComponent<BoxCollider2D>();

        originPosition = transform.position;
        fireGroundLineRenderer.SetPosition(0, originPosition);

        isDash = true;
    }

    //돌진
    void Dash()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, Time.deltaTime * dashSpeed);

        fireGroundLineRenderer.SetPosition(1, transform.position);

        //boxCollider 크기 및 위치 수정
        Vector3 center = (transform.position - originPosition) / 2;

        fireGroundBoxCollider.offset = center;
        fireGroundBoxCollider.size = new Vector2(Mathf.Abs(center.x - transform.position.x), Mathf.Abs(center.y - transform.position.y));

    }

    void EndDash()
    {
        isDash = false;
        StartCoroutine(PatternCooltime());
    }

    void StartFire()
    {
        fire.transform.position = transform.position;
        fire.SetActive(true);
        StartCoroutine(ExecuteMethodCorutine(2f, EndFire));
    }

    void EndFire()
    {
        fire.SetActive(false);
        StartCoroutine(PatternCooltime());
    }

    IEnumerator PatternCooltime()
    {
        yield return new WaitForSecondsRealtime(3f);

        patternIndex = Random.Range(0, 2);
        switch (patternIndex)
        {
            case 0:
                StartFire();
                break;
            case 1:
                PrePareDash();
                break;
        }
    }
}
