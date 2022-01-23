using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggMineScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
            Destroy(this.gameObject);
    }
}
