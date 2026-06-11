using UnityEngine;

public class TreasureCtrl : MonoBehaviour
{
    [Header("Reward")]
    [SerializeField] private int reward;

    public CollectionType collectionType;
    private bool isHooked = false;
    private bool isCaught = false;

    public bool IsCaught => isCaught;

    public void SetHooked(bool value)
    {
        isHooked = value;
    }

    public void MarkCaught()
    {
        isCaught = true;
    }
    public int GetReward()
    {
        return reward;
    }
}