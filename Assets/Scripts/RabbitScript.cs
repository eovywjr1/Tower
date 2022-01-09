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