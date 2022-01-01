using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedScript : MonoBehaviour
{
    public int currentHp;

    public bool isDie;

    public virtual void Ondamaged(int power)
    {
        currentHp -= power;
        if (currentHp <= 0)
        {
            currentHp = 0;
            isDie = true;
        }
    }
}
