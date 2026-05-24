using UnityEngine;
using System.Collections;

public class FishingGearManager : MonoBehaviour
{
    public static FishingGearManager Instance;

    [Header("Rod")]
    public float currentDepth;
    public float maxLineLength = 100f;
    public int rodLevel = 1;

    [Header("Sinker")]
    public float fallSpeed = 2f;
    public int sinkerLevel = 1;

    [Header("Hook")]
    public int hookSlots = 11;
    public int hookLevel = 1;

    [Header("Cost")]
    public int baseRodCost = 1500;
    public int baseHookCost = 1500;
    public int baseSinkerCost = 1500;

    bool rodProcessing = false;
    bool sinkerProcessing = false;
    bool hookProcessing = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //PlayerPrefs.DeleteAll(); 
            FishingSaveSystem.Load();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetRodCost() => baseRodCost * rodLevel;
    public int GetHookCost() => baseHookCost * hookLevel;
    public int GetSinkerCost() => baseSinkerCost * sinkerLevel;

    // ================= ROD =================
    public void UpgradeRod()
    {
        if (rodProcessing) return;
        StartCoroutine(UpgradeRodRoutine());
    }

    IEnumerator UpgradeRodRoutine()
    {
        rodProcessing = true;

        int cost = GetRodCost();

        if (!CurrencyManager.Instance.UsePearl(cost))
        {
            Debug.Log("Not enough pearl!");
            rodProcessing = false;
            yield break;
        }

        rodLevel++;
        maxLineLength += 50f;

        FishingSaveSystem.Save();

        yield return null; // chan double click cung frame

        rodProcessing = false;
    }

    // ================= SINKER =================
    public void UpgradeSinker()
    {
        if (sinkerProcessing) return;
        StartCoroutine(UpgradeSinkerRoutine());
    }

    IEnumerator UpgradeSinkerRoutine()
    {
        sinkerProcessing = true;

        int cost = GetSinkerCost();

        if (!CurrencyManager.Instance.UsePearl(cost))
        {
            Debug.Log("Not enough pearl!");
            sinkerProcessing = false;
            yield break;
        }

        sinkerLevel++;
        fallSpeed += 0.25f;

        FishingSaveSystem.Save();

        yield return null;

        sinkerProcessing = false;
    }

    // ================= HOOK =================
    public void UpgradeHook()
    {
        if (hookProcessing) return;
        StartCoroutine(UpgradeHookRoutine());
    }

    IEnumerator UpgradeHookRoutine()
    {
        hookProcessing = true;

        int cost = GetHookCost();

        if (!CurrencyManager.Instance.UsePearl(cost))
        {
            Debug.Log("Not enough pearl!");
            hookProcessing = false;
            yield break;
        }

        hookLevel++;
        hookSlots += 1;

        FishingSaveSystem.Save();

        yield return null;

        hookProcessing = false;
    }
}