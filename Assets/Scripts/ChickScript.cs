using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickScript : DamagedScript
{
    public PlayerScript playerScript;

    void Start()
    {
        playerScript = FindObjectOfType<PlayerScript>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerScript.transform.position, Time.deltaTime * 1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("PlayerAttack"))
            Destroy(this);
    }
}