using UnityEngine;

public class Hack : MonoBehaviour
{
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SaveData.Instance.Wallet.AddMoney(10000);
        }  
#endif
    }
}