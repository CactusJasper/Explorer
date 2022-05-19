using System.Globalization;
using Explorer.Player;
using UnityEngine;
using TMPro;

namespace Explorer.UI {
    public class HUD : MonoBehaviour {
        [SerializeField]
        private TMP_Text balanceText;
        [SerializeField]
        private TMP_Text inventoryUsageText;
        [SerializeField]
        private PlayerData playerData;

        private void Update() {
            UpdateBalance();
            UpdateInventoryUsage();
        }

        private void UpdateBalance() {
            decimal balance = playerData.GetBalance();
            CultureInfo cultureRef = new CultureInfo("en-GB");
            balanceText.text = $"£{balance.ToString(cultureRef)}";
        }

        private void UpdateInventoryUsage() {
            inventoryUsageText.text = $"{playerData.GetInventoryUsage()}/{playerData.GetCurrentInventorySize()}";
        }
    }
}

