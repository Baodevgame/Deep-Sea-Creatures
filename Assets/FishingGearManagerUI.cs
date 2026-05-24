using UnityEngine;
using UnityEngine.UI;

public class FishingGearManagerUI : MonoBehaviour
{
    [Header("Buttons GameObject")]
    public GameObject rodButton;
    public GameObject hookButton;
    public GameObject sinkerButton;

    [Header("Button")]
    public Button upgradeRodButton;
    public Button upgradeHookButton;
    public Button upgradeSinkerButton;

    [Header("Level Text")]
    public Text rodLevel;
    public Text hookLevel;
    public Text sinkerLevel;

    [Header("Parameter Text")]
    public Text rodParameters;
    public Text hookParameters;
    public Text sinkerParameters;

    private const int MAX_LEVEL = 10;

    [Header("Cost Text")]
    public Text rodCostText;
    public Text hookCostText;
    public Text sinkerCostText;

    private void Start()
    {
        upgradeRodButton.onClick.AddListener(OnUpgradeRod);
        upgradeHookButton.onClick.AddListener(OnUpgradeHook);
        upgradeSinkerButton.onClick.AddListener(OnUpgradeSinker);

        UpdateUI();
    }

    void OnUpgradeRod()
    {
        AudioManager.Instance.PlayClick();
        FishingGearManager.Instance.UpgradeRod();
        UpdateUI();
    }

    void OnUpgradeHook()
    {
        AudioManager.Instance.PlayClick();
        FishingGearManager.Instance.UpgradeHook();
        UpdateUI();
    }

    void OnUpgradeSinker()
    {
        AudioManager.Instance.PlayClick();
        FishingGearManager.Instance.UpgradeSinker();
        UpdateUI();
    }

    public void UpdateUI()
    {
        var gear = FishingGearManager.Instance;
        var currency = CurrencyManager.Instance;

        int rodLv = gear.rodLevel;
        int hookLv = gear.hookLevel;
        int sinkerLv = gear.sinkerLevel;

        int rodCost = gear.GetRodCost();
        int hookCost = gear.GetHookCost();
        int sinkerCost = gear.GetSinkerCost();

        // ===== LEVEL =====
        rodLevel.text = "Rod Level " + rodLv;
        hookLevel.text = "Hook Level " + hookLv;
        sinkerLevel.text = "Sinker Level " + sinkerLv;

        // ===== PARAM =====
        rodParameters.text = gear.maxLineLength + " Meters";
        hookParameters.text = gear.hookSlots + " Slots";
        sinkerParameters.text = gear.fallSpeed + " G";

        // ===== COST =====
        rodCostText.text = rodCost.ToString();
        hookCostText.text = hookCost.ToString();
        sinkerCostText.text = sinkerCost.ToString();

        // =====  =====
        rodCostText.color = currency.pearl >= rodCost ? Color.white : Color.red;
        hookCostText.color = currency.pearl >= hookCost ? Color.white : Color.red;
        sinkerCostText.color = currency.pearl >= sinkerCost ? Color.white : Color.red;

        // ===== ?N KHI MAX =====
        rodButton.SetActive(rodLv < MAX_LEVEL);
        hookButton.SetActive(hookLv < MAX_LEVEL);
        sinkerButton.SetActive(sinkerLv < MAX_LEVEL);
    }
}
