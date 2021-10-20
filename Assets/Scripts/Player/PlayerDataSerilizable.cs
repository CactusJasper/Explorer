[System.Serializable]
public class PlayerDataSerilizable
{
    public decimal balance;

    public PlayerDataSerilizable(PlayerData playerData)
    {
        balance = playerData.GetBalance();
    }
}
