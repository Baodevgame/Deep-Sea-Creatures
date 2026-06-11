using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollision : MonoBehaviour
{
    private int impactLimit = 3;
    private int currentImpact;

    private HookCtrl hookCtrl;

    private void Awake()
    {
        hookCtrl = GetComponentInParent<HookCtrl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Fish")) return;

        currentImpact++;

        if (currentImpact >= impactLimit)
        {
            BreakBubble();
        }
    }

    public void BreakBubble()
    {
        hookCtrl.StopFallingAndPullUp();
        hookCtrl.canCatchFish = true;
        gameObject.SetActive(false);
    }
}
