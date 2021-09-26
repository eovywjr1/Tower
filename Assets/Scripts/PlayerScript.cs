using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int power;

    public int tutorialIndex;

    public float horizontal, vertical;
    public float moveSpeed;

    public bool isHorizentalDown, isVerticalDown;
    public bool isTalk;
    public bool isAttackDelay, isAvoidanceDelay;
    public bool isAvoidancePossible;

    public Vector2 moveDireciton;

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public GameObject attackObject;
    BoxCollider2D attackCollider;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        attackCollider = attackObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!isTalk)
        {
            animator.SetBool("isState",false);

            MoveDirection();
            FlipChange();

            if (!isAttackDelay)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                    Attack();
            }
            else
                animator.SetBool("isAttack", false);

            if (!isAvoidanceDelay && isAvoidancePossible)
            {
                if (Input.GetKey(KeyCode.Space))
                    Avoidance();
            }
        }
        else
            animator.SetBool("isState", true);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void MoveDirection()
    {
        //��, �Ʒ� ���� ����Ű �Է�
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (vertical == 0 && horizontal != 0)
            isHorizentalDown = true;
        else if (vertical != 0 && horizontal == 0)
            isHorizentalDown = false;

        //�̵� �ִϸ��̼� ����
        if (horizontal != 0 || vertical != 0 && !isAttackDelay)
            animator.SetBool("isMove", true);
        else
            animator.SetBool("isMove", false);

        //��, �Ʒ� ���� ����
        if (isHorizentalDown)
            moveDireciton = new Vector2(horizontal, 0);
        else
            moveDireciton = new Vector2(0, vertical);
    }

    //��ȭ �� ������ x
    void Move()
    {
        if (!isTalk && !isAttackDelay)
            rigidBody.velocity = moveDireciton * moveSpeed;
        else
            rigidBody.velocity = new Vector2(0, 0);
    }

    //���� ��ȯ
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

    //�⺻���� �Լ�
    void Attack()
    {
        isAttackDelay = true;
        animator.SetBool("isAttack", true);
        attackObject.SetActive(true);
        StartCoroutine(AttackDelay());
    }

    //ȸ�� �Լ�
    void Avoidance()
    {
        float x = this.gameObject.transform.position.x, y = this.gameObject.transform.position.y;

        isAvoidanceDelay = true;
        this.gameObject.transform.position = new Vector2(x + moveDireciton.x * 2, y + moveDireciton.y * 2);
        StartCoroutine(AvoidanceDelay());
    }

    //�⺻���� ������ �ڷ�ƾ
    IEnumerator AttackDelay()
    {
        yield return new WaitForSecondsRealtime(0.7f);

        isAttackDelay = false;
        attackObject.SetActive(false);
    }

    IEnumerator AvoidanceDelay()
    {
        yield return new WaitForSecondsRealtime(5f);

        isAvoidanceDelay = false;
    }
}
