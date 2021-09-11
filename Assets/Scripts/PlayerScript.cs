using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int tutorialIndex;

    public float horizontal, vertical;
    public float moveSpeed;

    public bool isHorizentalDown, isVerticalDown;

    public Vector2 moveDireciton;

    Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        tutorialIndex = 1;
    }

    private void Update()
    {
        MoveDirection();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = moveDireciton * moveSpeed;
    }

    //위, 아래 방향 설정
    void MoveDirection()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Horizontal"))
            isHorizentalDown = true;
        else if (Input.GetButtonUp("Horizontal"))
            isHorizentalDown = false;

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

    void attack()
    {

    }
}
