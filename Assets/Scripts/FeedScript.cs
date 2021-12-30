using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedScript : BossBaseScript
{
    public CowScript cow;

    void Start()
    {
        currentHp = 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
            Ondamaged(1);
        else if (collision.CompareTag("Boss"))
        {
            cow.Recovery(2);
            gameObject.SetActive(false);
        }
    }
}
