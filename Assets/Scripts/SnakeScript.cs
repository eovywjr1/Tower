using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : BossBaseScript
{
    public GameObject bite, biteFindTarget;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BiteFindTargetStart()
    {
        biteFindTarget.SetActive(true);
        biteFindTarget.transform.position = transform.position;
        StartCoroutine(PatternStartCorutine(Color.red, 0.5f, BiteFindTargetStop));
    }

    void BiteFindTargetStop()
    {
        biteFindTarget.SetActive(false);

        StartCoroutine(PatternCooltime());
    }

    public void Bite()
    {
        biteFindTarget.SetActive(false);
        bite.SetActive(true);
        StartCoroutine(ExecuteMethodCorutine(0.5f, BiteStop));
    }

    void BiteStop()
    {
        bite.SetActive(false);
    }
}
