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

    public Vector2 moveDireciton;

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public GameObject attackObject;
    BoxCollider2D attackCollider;

    public override void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        attackCollider = attackObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        OtherAnimation();
        if (!isTalk)
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
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Ondamaged(int power)
    {
        currentHp -= power;
        isDamaged = true;
        StartCoroutine(Damaged());
    }

    //함수에 있지 않은 애니메이션
    void OtherAnimation()
    {
        if (!isTalk)
        {
            animator.SetBool("isState", false);
            if(isAttackDelay)
                animator.SetBool("isAttack", false);
        }
        else
            animator.SetBool("isState", true);
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
        if (horizontal != 0 || vertical != 0 && !isAttackDelay)
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
        if (!isTalk && !isAttackDelay)
            rigidBody.velocity = moveDireciton * moveSpeed;
        else
            rigidBody.velocity = new Vector2(0, 0);
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
        StartCoroutine(AttackDelay());
    }

    //회피 함수
    void Avoidance()
    {
        float x = this.gameObject.transform.position.x, y = this.gameObject.transform.position.y;

        isAvoidanceDelay = true;
        this.gameObject.transform.position = new Vector2(x + moveDireciton.x * 2, y + moveDireciton.y * 2);
        StartCoroutine(AvoidanceDelay());
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
        spriteRenderer.color = Color.red;
        yield return new WaitForSecondsRealtime(0.2f);
        spriteRenderer.color = Color.white;
        isDamaged = false;
    }
}
