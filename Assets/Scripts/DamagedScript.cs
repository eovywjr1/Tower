using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedScript : MonoBehaviour
{
    public int currentHp;
    public PlayerScript playerScript;

    public virtual void Awake()
    {
        playerScript = FindObjectOfType<PlayerScript>();
    }

    public virtual void Ondamaged(int power)
    {
        currentHp -= power;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Boss") && collision.CompareTag("PlayerAttack"))
            Ondamaged(playerScript.power);
        if(gameObject.CompareTag("Player") && collision.gameObject.layer == 7)
            Ondamaged(1);
    }
}
