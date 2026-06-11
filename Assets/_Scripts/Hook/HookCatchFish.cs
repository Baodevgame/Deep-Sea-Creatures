using System.Collections.Generic;
using UnityEngine;

public class HookCatchFish : MonoBehaviour
{
    public Transform catchPoint;

    private List<FishCtrl> caughtFishes = new List<FishCtrl>();
    private List<TreasureCtrl> caughtTreasures = new List<TreasureCtrl>();
    private List<DiverCtrl> caughtDivers = new List<DiverCtrl>();

    void CatchFish(FishCtrl fish)
    {
        if (fish.IsCaught) return;

        fish.MarkCaught();
        fish.SetHooked(true);

        caughtFishes.Add(fish);

        Rigidbody2D rb = fish.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        Transform hookPoint = fish.transform.Find("HookPoint");

        if (hookPoint == null)
        {
            Debug.LogError("khong tim thay HookPoint!");
            return;
        }

        // reset scale
        fish.transform.localScale = new Vector3(1, 1, 1);

        // rotate
        fish.transform.rotation = Quaternion.Euler(0, 0, 90f);

        // tinh offset sau khi rotate
        Vector3 offset = fish.transform.position - hookPoint.position;

        // gan vao hook
        fish.transform.SetParent(catchPoint, true);

        // dat lai vi tri
        fish.transform.position = catchPoint.position + offset;

        SpriteRenderer sr = fish.GetComponent<SpriteRenderer>();

        if (sr != null)
        {
            sr.sortingOrder = caughtFishes.Count ;
        }

        AutoDisable ad = fish.GetComponent<AutoDisable>();
        if (ad != null) ad.enabled = false;

        CheckHookFull();
    }

    void CatchDiver(DiverCtrl diver)
    {
        if (diver.IsCaught) return;

        diver.MarkCaught();
        diver.SetHooked(true);

        caughtDivers.Add(diver);

        Rigidbody2D rb = diver.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        diver.transform.SetParent(catchPoint);
        diver.transform.localPosition = Vector3.zero;

        CheckHookFull();
    }

    void CatchTreasure(TreasureCtrl treasure)
    {
        if (treasure.IsCaught) return;

        treasure.MarkCaught();
        treasure.SetHooked(true);

        caughtTreasures.Add(treasure);

        Rigidbody2D rb = treasure.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        treasure.transform.SetParent(catchPoint);
        treasure.transform.localPosition = Vector3.zero;

        CheckHookFull();
    }

    void CheckHookFull()
    {
        int total = caughtFishes.Count + caughtDivers.Count + caughtTreasures.Count;

        if (total >= FishingGearManager.Instance.hookSlots)
        {
            HookCtrl hookCtrl = GetComponent<HookCtrl>();
            hookCtrl.StopFallingAndPullUp();
            hookCtrl.DisableCatch();
        }
    }
    public List<CollectionType> GetAllCaughtItems()
    {
        List<CollectionType> items = new List<CollectionType>();

        foreach (var fish in caughtFishes) items.Add(fish.collectionType);

        foreach (var diver in caughtDivers) items.Add(diver.collectionType);

        foreach (var treasure in caughtTreasures) items.Add(treasure.collectionType);

        return items;
    }
    public int CurrentCatchCount =>caughtFishes.Count + caughtDivers.Count + caughtTreasures.Count;
    public List<FishCtrl> GetAllCaughtFish()
    {
        return caughtFishes;
    }

    public void ClearAllFish()
    {
        foreach (var fish in caughtFishes) if (fish != null) Destroy(fish.gameObject);

        foreach (var diver in caughtDivers) if (diver != null) Destroy(diver.gameObject);

        foreach (var treasure in caughtTreasures) if (treasure != null) Destroy(treasure.gameObject);

        caughtFishes.Clear();
        caughtDivers.Clear();
        caughtTreasures.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Fish")) return;

        HookCtrl hookCtrl = GetComponent<HookCtrl>();
        if (!hookCtrl.canCatchFish) return;

        FishCtrl fish = other.GetComponent<FishCtrl>();
        if (fish != null)
        {
            CatchFish(fish);
            return;
        }

        DiverCtrl diver = other.GetComponent<DiverCtrl>();
        if (diver != null)
        {
            CatchDiver(diver);
            return;
        }

        TreasureCtrl treasure = other.GetComponent<TreasureCtrl>();
        if (treasure != null)
        {
            CatchTreasure(treasure);
            return;
        }
    }
    public int GetTotalReward()
    {
        int total = 0;

        foreach (var fish in caughtFishes) total += fish.GetReward();

        foreach (var diver in caughtDivers) total += diver.GetReward();

        foreach (var treasure in caughtTreasures) total += treasure.GetReward();

        return total;
    }
}