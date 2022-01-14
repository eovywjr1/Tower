using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : BossBaseScript
{
    int dashSpeed = 5;
    bool isDash;

    public GameObject fire, fireGround, fireGroundPrefab;
    public LineRenderer fireGroundLineRenderer;

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
        playerPosition = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20));

        fireGround = Instantiate(fireGroundPrefab);
        fireGroundLineRenderer = fireGround.GetComponent<LineRenderer>();
        fireGroundLineRenderer.SetPosition(0, transform.position);

        isDash = true;
    }

    //돌진
    void Dash()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, Time.deltaTime * dashSpeed);

        fireGroundLineRenderer.SetPosition(1, transform.position);
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
