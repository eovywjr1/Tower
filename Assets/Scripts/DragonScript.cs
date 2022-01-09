using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : BossBaseScript
{
    public GameObject fire;

    private void Start()
    {
        StartCoroutine(PatternCooltime());
    }

    void StartFire()
    {
        fire.transform.position = transform.position;
        fire.SetActive(true);
        StartCoroutine(ExecuteMethodCorutine(2f, EndFire));
    }

    void EndFire()
    {
        fire.SetActive(false);
        StartCoroutine(PatternCooltime());
    }

    IEnumerator PatternCooltime()
    {
        yield return new WaitForSecondsRealtime(3f);

        patternIndex = Random.Range(0, 1);
        switch (patternIndex)
        {
            case 0:
                StartFire();
                break;
        }
    }
}
