using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseScript : DamagedScript
{
    public int maxHp;

    public bool isDie;

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
        }
    }
}