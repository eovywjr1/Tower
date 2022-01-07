using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitScript : BossBaseScript
{
    public float moveSpeed;

    public Vector3 direction;

    void Start()
    {
        StartCoroutine(PatternCoolTime());
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
        RandomDirection();

        transform.Translate(direction * moveSpeed * Time.deltaTime);

        StartCoroutine(PatternCoolTime());
    }

    IEnumerator PatternCoolTime()
    {
        yield return new WaitForSecondsRealtime(1f);

        Move();
    }
}
