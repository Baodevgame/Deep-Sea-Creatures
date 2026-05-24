using UnityEngine;
using GoogleMobileAds.Api;

public class RewardedAdManager : MonoBehaviour
{
    public static RewardedAdManager Instance;

    private RewardedAd rewardedAd;

    private string adUnitId = "ca-app-pub-2359977462573228/3954097309";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
            LoadRewardedAd();
        });
    }

    public void LoadRewardedAd()
    {
        AdRequest request = new AdRequest();

        RewardedAd.Load(adUnitId, request,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.Log("Failed to load rewarded ad");
                    return;
                }

                rewardedAd = ad;

                // Khi quảng cáo đóng
                rewardedAd.OnAdFullScreenContentClosed += () =>
                {
                    AudioManager.Instance.musicSource.UnPause();
                    AudioManager.Instance.ambientSource.UnPause();

                    LoadRewardedAd();
                };

                Debug.Log("Rewarded ad loaded");
            });
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            // Pause audio game
            AudioManager.Instance.musicSource.Pause();
            AudioManager.Instance.ambientSource.Pause();

            rewardedAd.Show(reward =>
            {
                CurrencyManager.Instance.AddPearl(100);
            });
        }
        else
        {
            Debug.Log("Rewarded ad not ready");
        }
    }
}