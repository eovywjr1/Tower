using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerScript : BossBaseScript
{
    public GameObject wind, scratch, bite;
    public Vector3 reverseDirection;
    public Rigidbody2D rigidBody;
    public AudioClip[] audioClips;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = FindObjectOfType<BackGroundAudioScript>().GetComponent<AudioSource>().volume;

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
        StartCoroutine(ExecuteMethodCorutine(0.25f, BackStepStop));
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

        PlaySound("Cry");

        StartCoroutine(ExecuteMethodCorutine(1f, CryStop));
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
        StartCoroutine(ExecuteMethodCorutine(0.5f, ScratchStop));
    }

    void ScratchStop()
    {
        scratch.SetActive(false);

        StartCoroutine(PatternCooltime());
    }

    void BiteFindTargetStart()
    {
        findTarget.SetActive(true);
        findTarget.transform.position = transform.position;
        StartCoroutine(PatternStartCorutine(Color.white, 0.4f, BiteFindTargetStop));
    }
    
    void BiteFindTargetStop()
    {
        findTarget.SetActive(false);

        StartCoroutine(PatternCooltime());
    }

    public void Bite()
    {
        findTarget.SetActive(false);
        bite.SetActive(true);
        StartCoroutine(ExecuteMethodCorutine(0.5f, BiteStop));
    }

    void BiteStop()
    {
        bite.SetActive(false);
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "Cry":
                audioSource.clip = audioClips[0];
                break;
        }
        audioSource.Play();
    }

    IEnumerator PatternCooltime()
    {
        yield return new WaitForSeconds(3f);

        playerPosition = playerScript.transform.position;

        patternIndex = Random.Range(0, 3);
        switch (patternIndex)
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
}