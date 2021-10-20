using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    public decimal balance = 0;

    public decimal GetBalance()
    {
        return balance;
    }
}
