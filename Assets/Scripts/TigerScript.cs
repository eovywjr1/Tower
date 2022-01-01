using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerScript : BossBaseScript
{
    public GameObject wind;
    public Vector2 direction;
    public Rigidbody2D rigidBody;

    private void Start()
    {
        BackStep();
    }

    void BackStep()
    {
        playerPosition = playerScript.transform.position;
        if (Mathf.Abs(transform.position.x - playerPosition.x) > 1)
        {
            if (transform.position.x > playerPosition.x)
                direction = new Vector2(1, 0);
            else
                direction = new Vector2(-1, 0);
        }
        else
            direction = new Vector2(0, 0);

        if (Mathf.Abs(transform.position.y - playerPosition.y) > 1)
        {
            if (transform.position.y > playerPosition.y)
                direction = new Vector2(direction.x, 1);
            else
                direction = new Vector2(direction.x, -1);
        }
        else
            direction = new Vector2(direction.x, 0);

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = direction * 5;
        StartCoroutine(StopCorutine());

        //transform.position = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
    }

    void Cry()
    {
        wind.SetActive(true);

        if (direction.x == 1 && direction.y == 0)
        {
            wind.transform.position = new Vector2(transform.position.x - 6, transform.position.y + 1);
            wind.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.x == 0 && direction.y == 1)
        {
            wind.transform.position = new Vector2(transform.position.x - 1, transform.position.y - 6);
            wind.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (direction.x == -1 && direction.y == 0)
        {
            wind.transform.position = new Vector2(transform.position.x + 6, transform.position.y - 1);
            wind.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            wind.transform.position = new Vector2(transform.position.x + 1, transform.position.y + 6);
            wind.transform.rotation = Quaternion.Euler(0, 0, 270);
        }

        StartCoroutine(CryStopCorutine());
    }

    void CryStop()
    {
        wind.SetActive(false);
    }

    IEnumerator StopCorutine()
    {
        yield return new WaitForSecondsRealtime(0.25f);

        rigidBody.velocity = new Vector2(0, 0);

        StartCoroutine(CryCorutine());
    }

    IEnumerator CryCorutine()
    {
        yield return new WaitForSecondsRealtime(2f);

        Cry();
    }

    IEnumerator CryStopCorutine()
    {
        yield return new WaitForSecondsRealtime(1f);

        CryStop();
    }
}
