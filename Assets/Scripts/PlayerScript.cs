using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : DamagedScript
{
    public int power, talkId;
    public float horizontal, vertical, moveSpeed, currentTime, startTime;
    public float attackSpeed = 0.5f;
    public float attackAnimationSpeed = 1f;
    public bool isHorizentalDown, isVerticalDown, isTalk, isDamaged, isAttackDelay, isAvoidanceDelay;
    public bool isAvoidancePossible, isFaint, isEnded = true;
    public Vector2 moveDireciton;
    public Image dashPossibleImage;
    public Text dashPossibleText;
    Rigidbody2D rigidBody;
    Animator animator;
    public GameObject attackObject;
    public BoxCollider2D attackCollider;
    UiManager uiManager;
    BossBaseScript bossBaseScript;
    AudioSource audioSource;
    public AudioClip[] audioClip;

    public void Awake()
    {
        startHpScript = FindObjectOfType<StartHpScript>();
        bossBaseScript = FindObjectOfType<BossBaseScript>();
        uiManager = FindObjectOfType<UiManager>();

        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        attackCollider = attackObject.GetComponent<BoxCollider2D>();

        audioSource.volume = EffectAudioScript.value;

        currentHp = startHpScript.playerHp;
    }

    private void Update()
    {
        if (!isTalk && !GameManager.isPause)
        {
            animator.SetBool("isState", false);

            if (!isDie && !isFaint)
            {
                if (isAvoidancePossible && !isAvoidanceDelay)
                {
                    if (Input.GetKey(KeyCode.Space))
                        Avoidance();
                }
                if (!isAttackDelay)
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                        Attack();
                    MoveDirection();
                    FlipChange();
                }
                else
                    Stop();
            }
            else
                Stop();
        }
        else
        {
            animator.SetBool("isState", true);
            Stop();
        }

        if (!isEnded)
            UpdateCoolTime();
    }

    void FixedUpdate()
    {
        if (!isTalk && !isAttackDelay && !isFaint && !isDie)
            Move();
    }

    void Stop()
    {
        animator.SetBool("isMove", false);
        rigidBody.velocity = new Vector2(0, 0);
    }

    public void PlayerDamaged(int power)
    {
        if (!isDamaged)
            Ondamaged(power);
        if (JudgeDie())
        {
            bossBaseScript.PlayerDiedAllStop();
            return;
        }

        isDamaged = true;
        spriteRenderer.color = Color.red;
        StartCoroutine(ExecuteMethodCorutine(2f, DamagedStop));
    }

    void DamagedStop()
    {
        spriteRenderer.color = Color.white;
        isDamaged = false;
    }

    void MoveDirection()
    {
        //위, 아래 방향 단축키 입력
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (vertical == 0 && horizontal != 0)
            isHorizentalDown = true;
        else if (vertical != 0 && horizontal == 0)
            isHorizentalDown = false;

        //이동 애니메이션 설정
        if (!isAttackDelay && (horizontal != 0 || vertical != 0))
            animator.SetBool("isMove", true);
        else
            animator.SetBool("isMove", false);

        //위, 아래 방향 설정
        if (isHorizentalDown)
            moveDireciton = new Vector2(horizontal, 0);
        else
            moveDireciton = new Vector2(0, vertical);
    }

    //대화 중 움직임 x
    void Move()
    {
        rigidBody.velocity = moveDireciton * moveSpeed;
    }

    //방향 전환
    void FlipChange()
    {
        if (isHorizentalDown)
        {
            if (horizontal > 0)
            {
                spriteRenderer.flipX = false;
                attackCollider.offset = new Vector2(1, (float)0.7);
            }
            else if (horizontal < 0)
            {
                spriteRenderer.flipX = true;
                attackCollider.offset = new Vector2(-1, (float)0.7);
            }
        }
    }

    //기본공격 함수
    void Attack()
    {
        isAttackDelay = true;
        animator.SetFloat("AttackSpeed", attackAnimationSpeed); //attackSpeed 0.1 감소 >> attackAnimationSpeed 0.2 증가
        animator.SetBool("isAttack", true);
        PlaySound("Attack");

        StartCoroutine(ExecuteMethodCorutine(0.01f, UnAttackAnimation));
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "Attack":
                audioSource.clip = audioClip[0];
                break;
            case "Avoidance":
                audioSource.clip = audioClip[1];
                break;
        }

        audioSource.Play();
    }

    void ActiveAttack()
    {
        attackObject.transform.position = transform.position;
    }

    void UnAttack()
    {
        isAttackDelay = false;
        attackObject.transform.position = new Vector3(100, 100);
    }

    void UnAttackAnimation()
    {
        animator.SetBool("isAttack", false);
    }

    //회피 함수
    void Avoidance()
    {
        PlaySound("Avoidance");
        isAvoidanceDelay = true;

        if (transform.position.x > 27 && horizontal == 1)
            transform.position = new Vector2(32.39501f, transform.position.y + vertical * 4);
        else if(transform.position.x < -30 && horizontal == -1)
            transform.position = new Vector2(-35.39499f, transform.position.y + vertical * 4);
        else if(transform.position.y > 25 && horizontal == 1)
            transform.position = new Vector2(transform.position.x + horizontal * 4, 30.34739f);
        else if (transform.position.y < -24 && horizontal == 1)
            transform.position = new Vector2(transform.position.x + horizontal * 4, -29.14738f);
        else
            this.gameObject.transform.position = new Vector2(transform.position.x + horizontal * 4, transform.position.y + vertical * 4);

        ResetTeleport();

        StartCoroutine(ExecuteMethodCorutine(5f, UnAvoidance));
    }

    void UnAvoidance()
    {
        isAvoidanceDelay = false;

        dashPossibleImage.gameObject.SetActive(true);
    }

    void UpdateCoolTime()
    {
        currentTime = Time.time - startTime;
        if (currentTime <= 5f)
            SetFillAmount(5f - currentTime);
        else
            EndCoolTime();
    }

    void ResetTeleport() 
    {
        currentTime = 5f;
        dashPossibleText.gameObject.SetActive(true);
        SetFillAmount(5f);
        startTime = Time.time;
        isEnded = false;
    }

    void SetFillAmount(float time)
    {
        dashPossibleImage.fillAmount = time / 5f;
        dashPossibleText.text = time.ToString("0.0");
    }

    void EndCoolTime()
    {
        SetFillAmount(0);
        dashPossibleText.gameObject.SetActive(false);
        isEnded = true;
    }


    public bool JudgeDie()
    {
        if (isDie)
        {
            uiManager.PlayerDiedShowText();
            rigidBody.velocity = new Vector2(0, 0);

            return true;
        }
        return false;
    }

    public void Faint()
    {
        isFaint = true;
        StartCoroutine(ExecuteMethodCorutine(4f, UnFaint));
    }

    void UnFaint()
    {
        isFaint = false;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!isDamaged)
        {
            switch (collision.gameObject.layer)
            {
                case 7:
                    PlayerDamaged(1);
                    break;
                case 8:
                    Faint();
                    break;
                case 9:
                    PlayerDamaged(2);
                    Destroy(collision.gameObject);
                    break;
            }
        }
    }
}