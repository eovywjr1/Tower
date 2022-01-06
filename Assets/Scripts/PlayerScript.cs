using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : DamagedScript
{
    public int power;
    public int talkId;

    public float horizontal, vertical;
    public float moveSpeed;
    public float attackSpeed = 0.5f;
    public float attackAnimationSpeed = 1f;

    public bool isHorizentalDown, isVerticalDown;
    public bool isTalk, isDamaged;
    public bool isAttackDelay, isAvoidanceDelay;
    public bool isAvoidancePossible;
    public bool isFaint;

    public Vector2 moveDireciton;

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public GameObject attackObject;
    BoxCollider2D attackCollider;

    public UiManager uiManager;

    public void Awake()
    {
        startHpScript = FindObjectOfType<StartHpScript>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        attackCollider = attackObject.GetComponent<BoxCollider2D>();

        currentHp = startHpScript.playerHp;
    }

    private void Update()
    {
        if (!isTalk)
        {
            animator.SetBool("isState", false);

            if (!isDie && !isFaint)
            {
                if (!isAttackDelay)
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                        Attack();
                    MoveDirection();
                    FlipChange();
                }

                if (!isAvoidanceDelay && isAvoidancePossible)
                {
                    if (Input.GetKey(KeyCode.Space))
                        Avoidance();
                }
            }
            else
                Stop();
        }
        else
            animator.SetBool("isState", true);
    }

    private void FixedUpdate()
    {
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
            return;

        isDamaged = true;
        spriteRenderer.color = Color.red;
        StartCoroutine(PatternStopCorutine(1f, DamagedStop));
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
        if (!isTalk && !isAttackDelay && !isFaint)
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
        attackObject.SetActive(true);

        StartCoroutine(AttackAnimationFalseCorutine());
        StartCoroutine(AttackDelay());
    }

    //회피 함수
    void Avoidance()
    {
        isAvoidanceDelay = true;
        this.gameObject.transform.position = new Vector2(transform.position.x + horizontal * 2, transform.position.y + vertical * 2);
        StartCoroutine(AvoidanceDelay());
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
        StartCoroutine(PatternStopCorutine(1f, UnFaint));
    }

    void UnFaint()
    {
        isFaint = false;
    }

    IEnumerator PatternStopCorutine(float time, System.Action action)
    {
        yield return new WaitForSecondsRealtime(time);

        action();
    }

    //기본공격 딜레이 코루틴
    IEnumerator AttackDelay()
    {
        yield return new WaitForSecondsRealtime(attackSpeed);

        isAttackDelay = false;
        attackObject.SetActive(false);
    }

    IEnumerator AvoidanceDelay()
    {
        yield return new WaitForSecondsRealtime(5f);

        isAvoidanceDelay = false;
    }

    IEnumerator Damaged()
    {
        yield return new WaitForSecondsRealtime(1f);
    }

    IEnumerator AttackAnimationFalseCorutine()
    {
        yield return new WaitForSecondsRealtime(0.01f);

        animator.SetBool("isAttack", false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDamaged)
        {
            if (collision.gameObject.layer == 7)
            {
                PlayerDamaged(1);
            }
            else if (collision.gameObject.layer == 8)
                Faint();
        }
    }
}
