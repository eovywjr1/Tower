using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseScript : MonoBehaviour
{
    public int hp;

    public bool isDie;

    PlayerScript playerScript;

    private void Awake()
    {
        playerScript = FindObjectOfType<PlayerScript>();
    }

    private void Update()
    {
        if (hp < 0)
            isDie = true;
    }

    public void Ondamaged(int power)
    {
        hp -= power;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        if (collision.CompareTag("PlayerAttack"))
        {
            Ondamaged(playerScript.power);
        }
    }
}
