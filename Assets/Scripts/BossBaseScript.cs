using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseScript : MonoBehaviour
{
    public int maxHp;
    public int currentHp;

    public bool isDie;

    public PlayerScript playerScript;

    private void Awake()
    {
        playerScript = FindObjectOfType<PlayerScript>();
    }

    private void Update()
    {
        if (currentHp <= 0)
            isDie = true;
    }

    public void Ondamaged(int power)
    {
        currentHp -= power;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
            Ondamaged(playerScript.power);
    }
}