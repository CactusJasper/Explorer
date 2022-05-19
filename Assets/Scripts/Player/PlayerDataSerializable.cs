using Explorer.Player;

namespace Explorer.Player {
    [System.Serializable]
    public class PlayerDataSerializable {
        public decimal balance;

        public PlayerDataSerializable(PlayerData playerData) {
            balance = playerData.GetBalance();
        }
    }
}
