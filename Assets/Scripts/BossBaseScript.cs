using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseScript : DamagedScript
{
    public int maxHp;

    public bool isDie;
    public bool isStart;

    public UiManager uiManager;

    public virtual void Update()
    {
        JudgeDie();
    }

    public void JudgeDie()
    {
        if (currentHp <= 0)
        {
            isDie = true;
            gameObject.SetActive(false);
            uiManager.StartEndCoolTime();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBossDamagePossible && collision.CompareTag("PlayerAttack"))
            Ondamaged(playerScript.power);
    }
}