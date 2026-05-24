using UnityEngine;
using DG.Tweening;

public class RewardEffectSimple : MonoBehaviour
{
    public RectTransform[] pearls;
    public RectTransform[] reputations;

    public RectTransform pearlTarget;
    public RectTransform reputationTarget;

    public void Play(RectTransform startBtn, int rewardAmount)
    {
        Debug.Log("PLAY EFFECT CALLED");

        int count = Mathf.Clamp(rewardAmount / 10, 3, 10);
        Vector2 startPos = GetLocalPos(startBtn);

        PlayCoins(pearls, startPos + new Vector2(0, 30), pearlTarget, count);
        PlayCoins(reputations, startPos + new Vector2(0, -30), reputationTarget, count);
    }

    void PlayCoins(RectTransform[] coins, Vector2 startPos, RectTransform target, int count)
    {
        float delay = 0f;

        for (int i = 0; i < coins.Length && i < count; i++)
        {
            var c = coins[i];

            c.DOKill(); // Ipt

            c.anchoredPosition = startPos;
            c.localScale = Vector3.zero;

            c.DOScale(1f, 0.25f).SetEase(Ease.OutBack).SetDelay(delay);

            c.DOAnchorPos(target.anchoredPosition, 0.6f).SetEase(Ease.InBack).SetDelay(delay + 0.2f);

            c.DOScale(0f, 0.3f).SetEase(Ease.InBack).SetDelay(delay + 1.2f);

            delay += 0.05f;
        }
    }

    Vector2 GetLocalPos(RectTransform target)
    {
        RectTransform canvasRect = transform.parent as RectTransform;

        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, target.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect,screenPos,null,out Vector2 localPoint);

        return localPoint;
    }
}