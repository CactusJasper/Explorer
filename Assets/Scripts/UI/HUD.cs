using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TMP_Text balanceText;
    [SerializeField]
    private PlayerData playerData;

    private void Update()
    {
        balanceText.text = $"£{string.Format("{0:n}", playerData.GetBalance())}";
    }
}
