using UnityEngine;

public class Hack : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SaveData.Instance.Wallet.AddMoney(999);
        }
    }
}