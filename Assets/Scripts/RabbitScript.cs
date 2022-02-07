using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitScript : BossBaseScript
{
    public float moveSpeed;
    public Vector3 direction;
    public bool isMove;

    void Start()
    {
        StartMove();
    }

    void FixedUpdate()
    {
        if (isMove)
            Move();
    }

    void RandomDirection()
    {
        float x = transform.position.x, y = transform.position.y;
        int random = Random.Range(0, 4);

        switch (random)
        {
            case 0:
                direction = Vector3.up;
                break;
            case 1:
                direction = Vector3.down;
                break;
            case 2:
                direction = Vector3.left;
                break;
            case 3:
                direction = Vector3.right;
                break;
        }

        if (x > 25)
            direction = new Vector2(-1, direction.y);
        else if (x < -25)
            direction = new Vector2(1, direction.y);
        if (y > 25)
            direction = new Vector2(direction.x, -1);
        else if (y < -25)
            direction = new Vector2(direction.x, 1);
    }

    void Move()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void StartMove()
    {
        isMove = true;
        RandomDirection();
        StartCoroutine(ExecuteMethodCorutine(1f, EndMove));
    }

    void EndMove()
    {
        isMove = false;
        StartCoroutine(ExecuteMethodCorutine(1f, StartMove));
    }
}