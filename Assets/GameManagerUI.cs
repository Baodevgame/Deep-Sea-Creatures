using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public class CardData
{
    public CollectionType type;
    public GameObject cardPrefab;
}

public class GameManagerUI : MonoBehaviour
{
    [SerializeField] private GachaWheel gachaWheel;
    public FishingGearManagerUI fishingGearUI;

    [Header("MainMenu GameObject")]
    public VideoPlayer videoPlayer;
    public GameObject game_BG;
    public GameObject advance_Spawn;
    public GameObject normal_Spawn;

    [Header("Collection GameObject")]
    public GameObject fishPanel;
    public GameObject diverPanel;
    public GameObject treasureChestPanel;
    public GameObject antiquePanel;

    [Header("Panel Of MainMenu")]
    public GameObject upgradePanel;
    public GameObject tidalTreasurePanel;
    public GameObject dailyTaskPanel;

    [Header("Text Of MainMenu")]
    public Text notice;
    Coroutine noticeRoutine;

    [Header("UI Of Game")]
    public Transform content;
    public GameObject pausePanel;
    public GameObject gameOverPanel;


    [Header("Canvas")]
    public GameObject canvas_MainMenu;
    public GameObject canvas_Collection;
    public GameObject canvas_Tutorial;
    public GameObject canvas_Game;

    [Header("UI Of Tutorial")]
    public ScrollRect scrollRect;
    public CanvasGroup canvasGroup;

    [Header("Card Mapping")]
    [SerializeField] private List<CardData> cards;

    [Header("Countdown")]
    public GameObject countdownPanel;
    public Text countdownText;

    private Dictionary<CollectionType, GameObject> cardDictionary;

    private void Awake()
    {
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        cardDictionary = new Dictionary<CollectionType, GameObject>();

        foreach (var data in cards)
        {
            if (!cardDictionary.ContainsKey(data.type))
            {
                cardDictionary.Add(data.type, data.cardPrefab);
            }
        }
    }

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += OnVideoReady;
        }
    }
    void Update()
    {
        if (scrollRect.verticalNormalizedPosition > 0.1f)
        {
            canvasGroup.alpha =
                Mathf.Lerp(
                    0.3f,
                    1f,
                    Mathf.PingPong(
                        Time.unscaledTime * 2f,
                        1
                    )
                );
        }
        else
        {
            canvasGroup.alpha = 0f;
        }
    }
    IEnumerator CountdownRoutine()
    {
        countdownPanel.SetActive(true);

        for (int i = 5; i > 0; i--)
        {
            countdownText.text = i.ToString();
            AudioManager.Instance.PlayCountdownTick();

            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "START!";
        AudioManager.Instance.PlayStartSFX();

        yield return new WaitForSeconds(1f);

        countdownPanel.SetActive(false);

        HookCtrl hook = FindObjectOfType<HookCtrl>();

        if (hook != null)
        {
            hook.StartFishing();
        }
    }
    void OnVideoReady(VideoPlayer vp)
    {
        AudioManager.Instance.PlayOceanAmbient();
        vp.Play();
    }
    void SwitchCanvas(GameObject target)
    {
        GameObject current = GetCurrentCanvas();

        CameraFader.Instance
            .FadeSwitchCanvas(current, target);
    }

    GameObject GetCurrentCanvas()
    {
        if (canvas_MainMenu.activeSelf)
            return canvas_MainMenu;

        if (canvas_Collection.activeSelf)
            return canvas_Collection;

        if (canvas_Tutorial.activeSelf)
            return canvas_Tutorial;

        if (canvas_Game.activeSelf)
            return canvas_Game;

        return null;
    }

    // Button of mainmenu
    public void AdvancedMapBtn()
    {
        if (CurrencyManager.Instance.vipTicket > 0)
        {
            CurrencyManager.Instance.UseVipTicket(1);

            if (videoPlayer != null)
            {
                videoPlayer.Pause();
                AudioManager.Instance.PauseAmbient();
            }

            AudioManager.Instance.PlayClick();

            GameObject current = GetCurrentCanvas();

            StartCoroutine(EnterGameRoutine(current, true));
        }
        else
        {
            ShowNotice("Not enough Vip ticket!", 2f);
        }
    }
    public void ShowNotice(string msg, float time)
    {
        if (noticeRoutine != null)
            StopCoroutine(noticeRoutine);

        noticeRoutine = StartCoroutine(NoticeRoutine(msg, time));
    }

    IEnumerator NoticeRoutine(string msg, float time)
    {
        notice.text = msg;

        yield return new WaitForSeconds(time);

        notice.text = "";
    }
    public void NormalMapBtn()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
            AudioManager.Instance.PauseAmbient();
        }

        AudioManager.Instance.PlayClick();

        GameObject current = GetCurrentCanvas();

        StartCoroutine(EnterGameRoutine(current, false));
    }
    IEnumerator EnterGameRoutine(GameObject current, bool isAdvance)
    {
        yield return CameraFader.Instance.FadeOut();

        current.SetActive(false);
        canvas_Game.SetActive(true);

        game_BG.SetActive(true);

        if (isAdvance)
        {
            advance_Spawn.SetActive(true);
            normal_Spawn.SetActive(false);
        }
        else
        {
            advance_Spawn.SetActive(false);
            normal_Spawn.SetActive(true);
        }

        yield return CameraFader.Instance.FadeIn();
        AudioManager.Instance.PlayGameMusic();
        StartCoroutine(CountdownRoutine());
    }
    public void TutorialBtn()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
            AudioManager.Instance.PauseAmbient();
        }

        game_BG.SetActive(false);
        normal_Spawn.SetActive(false);
        advance_Spawn.SetActive(false);
        AudioManager.Instance.PlayClick();
        SwitchCanvas(canvas_Tutorial);
    }
    public void OnUpgradePanel()
    {
        AudioManager.Instance.PlayClick();
        upgradePanel.GetComponent<PanelFader>().FadeIn();
        fishingGearUI.UpdateUI();
    }
    public void ColectionBtn()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
            AudioManager.Instance.PauseAmbient();
        }

        game_BG.SetActive(false);
        normal_Spawn.SetActive(false);
        advance_Spawn.SetActive(false);
        AudioManager.Instance.PlayClick();

        SwitchCanvas(canvas_Collection);
    }
    public void OnTidalTreasurePanel()
    {
        AudioManager.Instance.PlayClick();
        tidalTreasurePanel.GetComponent<PanelFader>().FadeIn();
    }
    public void XButton()
    {
        StartCoroutine(ClosePanelRoutine());
    }

    IEnumerator ClosePanelRoutine()
    {
        if (gachaWheel != null)
            gachaWheel.StopSpin();

        AudioManager.Instance.PlayClick();

        yield return StartCoroutine(
            AudioManager.Instance.FadeOutSpin(0.2f));

        FadeOutIfActive(upgradePanel);
        FadeOutIfActive(tidalTreasurePanel);
    }

    public void OnDailyTask()
    {
        AudioManager.Instance.PlayClick();
        dailyTaskPanel.GetComponent<PanelFader>().FadeIn();
    }
    public void CloseDailyTask()
    {
        AudioManager.Instance.PlayClick();
        dailyTaskPanel.GetComponent<PanelFader>().FadeOut();
    }
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;

        if (videoPlayer != null)
        {
            videoPlayer.Play();
            AudioManager.Instance.ResumeAmbient();
        }

        game_BG.SetActive(false);
        normal_Spawn.SetActive(false);
        advance_Spawn.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayClick();

        SwitchCanvas(canvas_MainMenu);
    }
    public void Home()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlayClick();
        AudioManager.Instance.StopMusic();
        CameraFader.Instance.FadeToScene("SplashScene");
    }


    // Button of collection

    void SwitchPanel(GameObject target)
    {
        FadeOutIfActive(fishPanel);
        FadeOutIfActive(diverPanel);
        FadeOutIfActive(treasureChestPanel);
        FadeOutIfActive(antiquePanel);

        target.SetActive(true);

        target.GetComponent<PanelFader>().FadeIn();
    }

    void FadeOutIfActive(GameObject obj)
    {
        if (obj.activeSelf)
        {
            obj.GetComponent<PanelFader>().FadeOut();
        }
    }

    public void FishBtn()
    {
        AudioManager.Instance.PlayClick();
        SwitchPanel(fishPanel);
    }
    public void DiverBtn()
    {
        AudioManager.Instance.PlayClick();
        SwitchPanel(diverPanel);
    }
    public void TreasureChestBtn()
    {
        AudioManager.Instance.PlayClick();
        SwitchPanel(treasureChestPanel);
    }
    public void AntiqueBtn()
    {
        AudioManager.Instance.PlayClick();
        SwitchPanel(antiquePanel);
    }
    // Button of Game

    public void OnGameOverPanel()
    {
        HookCtrl hook = FindObjectOfType<HookCtrl>();
        HookCatchFish catcher = hook.GetComponent<HookCatchFish>();

        List<CollectionType> items = catcher.GetAllCaughtItems();

        int totalReward = catcher.GetTotalReward();

        foreach (Transform child in content)
            Destroy(child.gameObject);

        foreach (CollectionType type in items)
        {
            // Spawn card dung loai
            if (cardDictionary.TryGetValue(type, out GameObject prefab))
            {
                Instantiate(prefab, content);
            }

            CollectionManager.MarkDiscovered(type);
        }

        // Reward
        CurrencyManager.Instance.AddPearl(totalReward);
        CurrencyManager.Instance.AddFisherReputation(totalReward);

        Debug.Log("Total reward: " + totalReward);

        gameOverPanel.GetComponent<PanelFader>().FadeIn();

        Time.timeScale = 0;
    }
    public void OnPausePanel()
    {
        AudioManager.Instance.PlayClick();
        pausePanel.GetComponent<PanelFader>().FadeIn();

        Time.timeScale = 0;
    }
    public void ClickContinueButton()
    {
        AudioManager.Instance.PlayClick();
        pausePanel.GetComponent<PanelFader>().FadeOut();

        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        AudioManager.Instance.PlayClick();

        Application.Quit();

        Debug.Log("Quit Game");
    }
}

