using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int tutorialIndex;

    public float horizontal, vertical;
    public float moveSpeed;

    public bool isHorizentalDown, isVerticalDown;
    public bool isTalk;

    public Vector2 moveDireciton;

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveDirection();
        FlipChange();

        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
        else
            animator.SetBool("isAttack", false);
    }

    private void FixedUpdate()
    {
        Move();
    }

    //위, 아래 방향 설정
    void MoveDirection()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Horizontal"))
        {
            isHorizentalDown = true;
            animator.SetBool("isMove", true);
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            isHorizentalDown = false;
            animator.SetBool("isMove", true);
        }
        else
            animator.SetBool("isMove", false);

        if (Input.GetButton("Vertical"))
            isVerticalDown = true;
        else if (Input.GetButtonUp("Vertical"))
            isVerticalDown = false;

        if (isHorizentalDown)
            moveDireciton = new Vector2(horizontal, 0);
        else if (isVerticalDown)
            moveDireciton = new Vector2(0, vertical);
        else
            moveDireciton = new Vector2(0, 0);
    }

    //대화 중 움직임 x
    void Move()
    {
        if (!isTalk)
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
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;
        }
    }

    void Attack()
    {
        animator.SetBool("isAttack", true);
    }
}
