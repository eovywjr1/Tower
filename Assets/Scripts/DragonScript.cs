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
    public AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = EffectAudioScript.value;

        StartCoroutine(PatternCooltime());
    }

    private void Update()
    {
        if (isDash && !GameManager.isPause)
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
        fireGroundLineRenderer.SetPosition(1, originPosition);

        isDash = true;
    }

    //돌진
    void Dash()
    {
        //boxCollider 크기 및 위치 수정
        Vector3 center = originPosition + (transform.position - originPosition) / 2;
        Vector3 differ = new Vector3(Mathf.Abs(transform.position.x - originPosition.x), Mathf.Abs(transform.position.y - originPosition.y));

        transform.position = Vector2.MoveTowards(transform.position, playerPosition, Time.deltaTime * dashSpeed);

        fireGroundLineRenderer.SetPosition(1, transform.position);
        fireGroundBoxCollider.offset = center;
        if (differ.x < 1)
            differ.x = 1;
        if (differ.y < 1)
            differ.y = 1;
        fireGroundBoxCollider.size = differ;
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

        PlaySound("Fire");

        StartCoroutine(ExecuteMethodCorutine(2f, EndFire));
    }

    void EndFire()
    {
        fire.SetActive(false);
        StartCoroutine(PatternCooltime());
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "Fire":
                audioSource.clip = audioClips[0];
                break;
        }
        audioSource.Play();
    }
    IEnumerator PatternCooltime()
    {
        yield return new WaitForSeconds(3f);

        patternIndex = Random.Range(0, 2);
        switch (patternIndex)
        {
            case 0:
                StartCoroutine(PatternStartCorutine(Color.red, 1f, StartFire));
                break;
            case 1:
                PrePareDash();
                break;
        }
    }
}