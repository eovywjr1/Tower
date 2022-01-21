using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenScript : BossBaseScript
{
    public Vector3 direction;
    public Rigidbody2D rigidBody;
    public GameObject eggMinePrefab, eggMine, chickPrefab, chick;

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
        chick.transform.position = transform.position;

        StartCoroutine(PatternCooltime());
    }

    IEnumerator PatternCooltime()
    {
        yield return new WaitForSecondsRealtime(3f);

        patternIndex = Random.Range(0, 2);
        switch (patternIndex)
        {
            case 0:
                StartCoroutine(PatternStartCorutine(Color.red, 1f, PlantEggMine));
                break;
            case 1:
                StartCoroutine(PatternStartCorutine(Color.red, 1f, CreateChick));
                break;
        }
    }
}