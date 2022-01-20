using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : DamagedScript
{
    public int power, talkId;

    public float horizontal, vertical, moveSpeed;
    public float attackSpeed = 0.5f;
    public float attackAnimationSpeed = 1f;

    public bool isHorizentalDown, isVerticalDown, isTalk, isDamaged, isAttackDelay, isAvoidanceDelay;
    public bool isAvoidancePossible, isFaint;

    public Vector2 moveDireciton;

    Rigidbody2D rigidBody;
    Animator animator;

    public GameObject attackObject;
    public BoxCollider2D attackCollider;

    public UiManager uiManager;
    BossBaseScript bossBaseScript;

    public void Awake()
    {
        startHpScript = FindObjectOfType<StartHpScript>();
        bossBaseScript = FindObjectOfType<BossBaseScript>();

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
    }

    private void FixedUpdate()
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
        attackObject.transform.position = transform.position;

        StartCoroutine(ExecuteMethodCorutine(0.01f, UnAttackAnimation));
    }

    void UnAttack()
    {
        isAttackDelay = false;
    }

    public void ChangeAttackPosition()
    {
        attackObject.transform.position = new Vector3(100, 100);
    }

    void UnAttackAnimation()
    {
        animator.SetBool("isAttack", false);
    }

    //회피 함수
    void Avoidance()
    {
        isAvoidanceDelay = true;
        this.gameObject.transform.position = new Vector2(transform.position.x + horizontal * 4, transform.position.y + vertical * 4);
        StartCoroutine(ExecuteMethodCorutine(5f, UnAvoidance));
    }

    void UnAvoidance()
    {
        isAvoidanceDelay = false;
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
        Debug.Log(collision.name);
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