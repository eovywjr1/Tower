using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseScript : DamagedScript
{
    public int maxHp;

    public bool isStart;
    public bool isBossDamagePossible = true;

    public Vector3 playerPosition;

    public UiManager uiManager;
    public PlayerScript playerScript;

    public void Awake()
    {
        startHpScript = FindObjectOfType<StartHpScript>();
        playerScript = FindObjectOfType<PlayerScript>();

        maxHp = startHpScript.bossHp;
        currentHp = maxHp;
    }

    public void JudgeDie()
    {
        if (isDie)
        {
            gameObject.SetActive(false);
            uiManager.StartEndCoolTime();
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (isBossDamagePossible && collision.CompareTag("PlayerAttack"))
        {
            playerScript.attackCollider.enabled = false;
            Ondamaged(playerScript.power);
            JudgeDie();
        }
    }
}