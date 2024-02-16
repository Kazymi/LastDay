using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Cheat : MonoBehaviour
{
    public void AddMoney()
    {
        SaveData.Instance.Wallet.AddMoney(10000);
    }
}
