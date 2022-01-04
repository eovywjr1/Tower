using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerScript : BossBaseScript
{
    public int random;

    public GameObject wind;
    public GameObject scratch;
    public GameObject bite;
    public GameObject biteFindTarget;
    public Vector3 reverseDirection;
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(PatternCooltime());
    }

    void FindReverseDirection()
    {
        Vector3 target = transform.position - playerPosition;

        if (Mathf.Abs(target.x) >= Mathf.Abs(target.y))
        {
            if (target.x >= 0)
                reverseDirection = Vector3.right;
            else
                reverseDirection = Vector3.left;
        }
        else
        {
            if (target.y >= 0)
                reverseDirection = Vector3.up;
            else
                reverseDirection = Vector3.down;
        }
    }

    void BackStep()
    {
        FindReverseDirection();
        rigidBody.velocity = reverseDirection * 5;
        StartCoroutine(PatternStopCorutine(0.25f, BackStepStop));
    }

    void BackStepStop()
    {
        rigidBody.velocity = new Vector2(0, 0);

        StartCoroutine(PatternStartCorutine(Color.white, 2f, Cry));
    }

    void ObjectArrange(GameObject gameObject, int addPositionMax, int addPositionmin, int addRotationZ)
    {
        if (reverseDirection == Vector3.right)
        {
            gameObject.transform.position = new Vector2(transform.position.x - addPositionMax, transform.position.y + addPositionmin);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0 + addRotationZ);
        }
        else if (reverseDirection == Vector3.up)
        {
            gameObject.transform.position = new Vector2(transform.position.x - addPositionmin, transform.position.y - addPositionMax);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 90 + addRotationZ);
        }
        else if (reverseDirection == Vector3.left)
        {
            gameObject.transform.position = new Vector2(transform.position.x + addPositionMax, transform.position.y - addPositionmin);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 180 + addRotationZ);
        }
        else
        {
            gameObject.transform.position = new Vector2(transform.position.x + addPositionmin, transform.position.y + addPositionMax);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 270 + addRotationZ);
        }
    }

    void Cry()
    {
        wind.SetActive(true);
        ObjectArrange(wind, 6, 1, 0);

        StartCoroutine(PatternStopCorutine(1f, CryStop));
    }

    void CryStop()
    {
        wind.SetActive(false);

        StartCoroutine(PatternCooltime());
    }

    void Scratch()
    {
        scratch.SetActive(true);
        FindReverseDirection();
        ObjectArrange(scratch, 3, 0, 220);
        StartCoroutine(PatternStopCorutine(0.5f, ScratchStop));
    }

    void ScratchStop()
    {
        scratch.SetActive(false);

        StartCoroutine(PatternCooltime());
    }

    void BiteFindTargetStart()
    {
        biteFindTarget.SetActive(true);
        StartCoroutine(PatternStartCorutine(Color.red, 0.5f, BiteFindTargetStop));
    }
    
    void BiteFindTargetStop()
    {
        biteFindTarget.SetActive(false);

        StartCoroutine(PatternCooltime());
    }

    public void Bite()
    {
        biteFindTarget.SetActive(false);
        bite.SetActive(true);
        bite.transform.position = new Vector3(playerPosition.x, playerPosition.y + 1.5f);
        StartCoroutine(PatternStopCorutine(0.5f, BiteStop));
    }

    void BiteStop()
    {
        bite.SetActive(false);
    }

    IEnumerator PatternCooltime()
    {
        yield return new WaitForSecondsRealtime(3f);

        playerPosition = playerScript.transform.position;

        random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                BackStep();
                break;
            case 1:
                StartCoroutine(PatternStartCorutine(Color.red, 1f, Scratch));
                break;
            case 2:
                StartCoroutine(PatternStartCorutine(Color.red, 1f, BiteFindTargetStart));
                break;
        }
    }

    IEnumerator PatternStopCorutine(float time, System.Action action)
    {
        yield return new WaitForSecondsRealtime(time);

        action();
    }

    IEnumerator PatternStartCorutine(Color startColor, float time, System.Action action)
    {
        spriteRenderer.color = startColor;

        yield return new WaitForSecondsRealtime(time);

        spriteRenderer.color = Color.white;
        action();
    }
}