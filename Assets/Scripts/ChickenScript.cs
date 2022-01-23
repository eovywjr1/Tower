using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenScript : BossBaseScript
{
    public Vector3 direction;
    public Rigidbody2D rigidBody;
    public GameObject eggMinePrefab, eggMine, chickPrefab, chick, findTarget, peck;
    public bool isPeck;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        StartCoroutine(PatternCooltime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlantEggMine()
    {
        eggMine = Instantiate(eggMinePrefab);
        eggMine.transform.position = transform.position;

        Move();
    }

    void FindDirection()
    {
        int directionIndex = Random.Range(0, 5);

        switch (directionIndex)
        {
            case 0:
                direction = Vector3.left;
                break;
            case 1:
                direction = Vector3.right;
                break;
            case 2:
                direction = Vector3.down;
                break;
            case 3:
                direction = Vector3.up;
                break;
        }
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
        yield return new WaitForSecondsRealtime(1.5f);

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