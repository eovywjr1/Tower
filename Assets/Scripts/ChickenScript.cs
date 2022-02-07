using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenScript : BossBaseScript
{
    public Vector3 direction;
    public Rigidbody2D rigidBody;
    public GameObject eggMinePrefab, eggMine, chickPrefab, chick, peck;
    public bool isPeck;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        StartCoroutine(PatternCooltime());
    }

    void PlantEggMine()
    {
        eggMine = Instantiate(eggMinePrefab);
        eggMine.transform.position = transform.position;

        Move();
    }

    void FindDirection()
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
        FindDirection();
        rigidBody.velocity = direction * 15;
        StartCoroutine(ExecuteMethodCorutine(0.25f, MoveStop));
    }

    void MoveStop()
    {
        rigidBody.velocity = new Vector2(0, 0);

        StartCoroutine(PatternCooltime());
    }

    void CreateChick()
    {
        chick = Instantiate(chickPrefab);
        chick.transform.position = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20));

        StartCoroutine(PatternCooltime());
    }

    void FindTargetStart()
    {
        findTarget.SetActive(true);
        findTarget.transform.position = transform.position;
        StartCoroutine(PatternStartCorutine(Color.white, 0.5f, FindTargetStop));
    }

    void FindTargetStop()
    {
        findTarget.SetActive(false);

        StartCoroutine(PatternCooltime());
    }

    public void Peck()
    {
        findTarget.SetActive(false);
        peck.SetActive(true);
        StartCoroutine(ExecuteMethodCorutine(0.5f, PeckStop));
    }

    void PeckStop()
    {
        peck.SetActive(false);
    }

    IEnumerator PatternCooltime()
    {
        yield return new WaitForSeconds(1.5f);

        patternIndex = Random.Range(0, 3);
        switch (patternIndex)
        {
            case 0:
                StartCoroutine(PatternStartCorutine(Color.red, 1f, PlantEggMine));
                break;
            case 1:
                StartCoroutine(PatternStartCorutine(Color.red, 1f, CreateChick));
                break;
            case 2:
                StartCoroutine(PatternStartCorutine(Color.red, 1f, FindTargetStart));
                break;
        }
    }
}