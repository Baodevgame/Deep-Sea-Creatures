using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAdManager : MonoBehaviour
{
    public static BannerAdManager Instance;
    private bool isLoaded;
    private bool isLoading;
    [SerializeField]
    private AdsSettings adsSettings;

    private BannerView bannerView;

    private float retryDelay = 5f;
    private const float MAX_RETRY_DELAY = 300f;

    private string BannerId
    {
        get
        {
#if UNITY_ANDROID
            return adsSettings.BannerId;
#elif UNITY_IOS
            return adsSettings.IOSBannerId;
#else
            return "unused";
#endif
        }
    }

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
        }
    }

    //private void Start()
    //{
    //    InvokeRepeating(nameof(CheckInitialization), 0f, 1f);
    //}

    //private void CheckInitialization()
    //{
    //    if (!AdsInitializer.IsInitialized)
    //        return;

    //    CancelInvoke(nameof(CheckInitialization));

    //    LoadBanner();
    //}

    //==================== LOAD ====================

    private void LoadBanner()
    {
        if (isLoading)
            return;

        isLoading = true;

        DestroyBanner();

        AdSize adSize =
            AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(
                AdSize.FullWidth);

        bannerView =
            new BannerView(
                BannerId,
                adSize,
                AdPosition.Bottom);

        RegisterEvents();

        AdRequest request = new AdRequest();

        Debug.Log("Loading Banner...");

        bannerView.LoadAd(request);
    }

    private void RegisterEvents()
    {
        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner Loaded");

            isLoading = false;
            isLoaded = true;
            retryDelay = 5f;

            bannerView.Show();
        };

        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner Failed : " + error);

            isLoading = false;
            isLoaded = false;

            Invoke(nameof(LoadBanner), retryDelay);

            retryDelay =
                Mathf.Min(
                    retryDelay * 2f,
                    MAX_RETRY_DELAY);
        };
    }

    //==================== SHOW ====================

    public void ShowBanner()
    {
        if (!AdsInitializer.IsInitialized)
        {
            Debug.Log("AdMob Not Initialized");
            return;
        }

        if (bannerView == null)
        {
            Debug.Log("Load First Banner");
            LoadBanner();
            return;
        }

        if (isLoaded)
        {
            Debug.Log("Show Banner");

            bannerView.Show();
        }
    }

    //==================== HIDE ====================

    public void HideBanner()
    {
        if (bannerView != null)
        {
            Debug.Log("Hide Banner");

            bannerView.Hide();
        }
    }

    //==================== DESTROY ====================

    public void DestroyBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }

        isLoaded = false;
        isLoading = false;
    }

    private void OnDestroy()
    {
        DestroyBanner();
    }
}
