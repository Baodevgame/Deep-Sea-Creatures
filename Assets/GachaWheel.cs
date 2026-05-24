using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaWheel : MonoBehaviour
{
    public Text noticeText;
    public GachaItem[] items;
    public int segments = 8;

    public float spinDuration = 2f;
    public float currentAngle = -90f;
    public float pointerOffset = -90f;

    public int costPearlX1 = 1500;
    public int costPearlX10 = 15000;

    bool spinning = false;
    public bool skipSpin = false;

    List<GachaItem> rewardsX10 = new List<GachaItem>();

    [Header("Reward Popup")]
    public GameObject rewardPopup;
    public Transform rewardContent;
    public GameObject rewardItemPrefab;

    [Header("Fade Settings")]
    public CanvasGroup popupCanvasGroup;
    public float fadeDuration = 0.3f;

    public void SetSkipSpin(bool value)
    {
        skipSpin = value;
    }

    // ================= RANDOM =================
    int GetRandomIndex()
    {
        float totalWeight = 0f;

        foreach (var item in items)
            totalWeight += item.weight;

        float rand = Random.Range(0f, totalWeight);
        float current = 0f;

        for (int i = 0; i < items.Length; i++)
        {
            current += items[i].weight;

            if (rand <= current)
                return i;
        }

        return items.Length - 1;
    }
    // ================= SPIN X1 =================
    public void SpinX1()
    {
        if (spinning) return;

        if (CurrencyManager.Instance.UseGachaTicket(1))
            Debug.Log("Use 1 ticket");

        else if (!CurrencyManager.Instance.UsePearl(costPearlX1))
        {
            ShowNotice("Not enough pearls and spin tickets!");
            return;
        }

        int rewardIndex = GetRandomIndex();
        AudioManager.Instance.PlaySpinOnce();

        StartCoroutine(SpinWheel(rewardIndex));
    }

    IEnumerator SpinWheel(int rewardIndex)
    {
        spinning = true;

        yield return StartCoroutine(SpinWheelToIndex(rewardIndex));

        GiveReward(rewardIndex);

        spinning = false;
    }

    // ================= SPIN X10 =================
    public void SpinX10()
    {
        if (spinning) return;

        if (CurrencyManager.Instance.UseGachaTicket(10))
            Debug.Log("Use 10 tickets");

        else if (!CurrencyManager.Instance.UsePearl(costPearlX10))
        {
            ShowNotice("Not enough pearls and spin tickets!");
            return;
        }

        if (skipSpin)
        {
            rewardsX10.Clear();

            for (int i = 0; i < 10; i++)
            {
                int rewardIndex = GetRandomIndex();

                GiveReward(rewardIndex);

                rewardsX10.Add(items[rewardIndex]);
            }

            ShowRewardPopup();
        }
        else
        {
            StartCoroutine(Spin10());
        }
    }

    IEnumerator Spin10()
    {
        spinning = true;

        rewardsX10.Clear();
        AudioManager.Instance.PlaySpinLoop();

        for (int i = 0; i < 10; i++)
        {
            int rewardIndex = GetRandomIndex();

            yield return StartCoroutine(SpinWheelToIndex(rewardIndex));

            GiveReward(rewardIndex);

            rewardsX10.Add(items[rewardIndex]);

            yield return new WaitForSeconds(0.1f);
        }
        yield return StartCoroutine(AudioManager.Instance.FadeOutSpin(0.2f));

        spinning = false;

        ShowRewardPopup();
    }

    // ================= ROTATE =================
    IEnumerator SpinWheelToIndex(int index)
    {
        float anglePerSegment = 360f / segments;
        float targetSlotAngle = index * anglePerSegment + pointerOffset;

        float startAngle = currentAngle;
        float endAngle = 360f * 5 + targetSlotAngle;

        float t = 0f;

        while (t < spinDuration)
        {
            t += Time.deltaTime;

            float lerp = Mathf.SmoothStep(0, 1, t / spinDuration);

            float angle = Mathf.Lerp(startAngle, endAngle, lerp);

            transform.eulerAngles = new Vector3(0, 0, angle);

            yield return null;
        }

        currentAngle = endAngle % 360f;

        transform.eulerAngles =new Vector3(0, 0, currentAngle);
    }

    // ================= REWARD =================
    void GiveReward(int index)
    {
        GachaItem reward = items[index];

        Debug.Log($"YOU GOT: {reward.itemName} +{reward.amount}");

        switch (reward.rewardType)
        {
            case RewardType.Pearl:
                CurrencyManager.Instance.AddPearl(reward.amount);
                break;

            case RewardType.GachaTicket:
                CurrencyManager.Instance.AddGachaTicket(reward.amount);
                break;

            case RewardType.VipTicket:
                CurrencyManager.Instance.AddVipTicket(reward.amount);
                break;
        }
    }

    // ================= SHOW POPUP =================
    void ShowRewardPopup()
    {
        foreach (Transform child in rewardContent)
            Destroy(child.gameObject);

        foreach (var reward in rewardsX10)
        {
            GameObject obj = Instantiate(rewardItemPrefab,rewardContent);

            RewardItemUI ui = obj.GetComponent<RewardItemUI>();

            ui.Setup(reward);
        }

        StartCoroutine(FadeInPopup());
    }

    public void CloseRewardPopup()
    {
        StartCoroutine(FadeOutPopup());
    }

    // ================= FADE =================
    IEnumerator FadeInPopup()
    {
        rewardPopup.SetActive(true);

        float t = 0f;

        popupCanvasGroup.alpha = 0f;
        popupCanvasGroup.interactable = false;
        popupCanvasGroup.blocksRaycasts = false;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            popupCanvasGroup.alpha =Mathf.Lerp(0f, 1f, t / fadeDuration);

            yield return null;
        }

        popupCanvasGroup.alpha = 1f;
        popupCanvasGroup.interactable = true;
        popupCanvasGroup.blocksRaycasts = true;
    }

    IEnumerator FadeOutPopup()
    {
        float t = 0f;

        popupCanvasGroup.interactable = false;
        popupCanvasGroup.blocksRaycasts = false;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            popupCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);

            yield return null;
        }

        popupCanvasGroup.alpha = 0f;

        rewardPopup.SetActive(false);
    }

    // ================= NOTICE =================
    public void ShowNotice(string message)
    {
        noticeText.text = message;
        noticeText.gameObject.SetActive(true);

        StartCoroutine(HideNoticeAfterTime(3f));
    }

    IEnumerator HideNoticeAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        noticeText.gameObject.SetActive(false);
    }
    public void StopSpin()
    {
        StopAllCoroutines();
        spinning = false;
    }
    void OnDisable()
    {
        StopAllCoroutines();
        spinning = false;
    }
}