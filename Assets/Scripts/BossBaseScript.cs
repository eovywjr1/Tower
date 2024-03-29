using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseScript : DamagedScript
{
    public int maxHp, patternIndex;
    public bool isBossDamagePossible = true, isPause;
    public UiManager uiManager;
    public PlayerScript playerScript;
    public GameObject skillObject, findTarget;
    public Vector3 playerPosition;
    public AudioSource audioSource;


    public void Awake()
    {
        startHpScript = FindObjectOfType<StartHpScript>();
        uiManager = FindObjectOfType<UiManager>();
        playerScript = FindObjectOfType<PlayerScript>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        maxHp = startHpScript.bossHp;
        currentHp = maxHp;
    }

    public void JudgeDie()
    {
        if (isDie)
        {
            uiManager.FirstTalkShow();

            if (skillObject != null)
                skillObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBossDamagePossible && collision.CompareTag("PlayerAttack"))
        {
            Ondamaged(playerScript.power);
            JudgeDie();
        }
    }

    public IEnumerator PatternStartCorutine(Color startColor, float time, System.Action action)
    {
        spriteRenderer.color = startColor;

        yield return new WaitForSeconds(time);

        spriteRenderer.color = Color.white;
        action();
    }

    public void PlayerDiedAllStop()
    { 
        StopAllCoroutines();
    }
}