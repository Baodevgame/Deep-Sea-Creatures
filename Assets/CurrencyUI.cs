using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public Text pearlText;
    public Text pearlTicketText;
    public Text vipTicketText;
    public Text fisherReputationText;

    void Update()
    {
        pearlText.text = CurrencyManager.Instance.pearl.ToString();
        pearlTicketText.text = CurrencyManager.Instance.gachaTicket.ToString();
        vipTicketText.text = CurrencyManager.Instance.vipTicket.ToString();
        fisherReputationText.text = "Reputation Points: " + CurrencyManager.Instance.fisherReputation.ToString();
    }
}