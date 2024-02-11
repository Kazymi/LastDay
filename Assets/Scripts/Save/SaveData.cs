using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText; //todo
    
    public static SaveData Instance;
    
    public int CurrentLevel;

    public Wallet Wallet { get; private set; }

    private void Awake()
    {
        Instance = this;
        Wallet = new Wallet(moneyText);
    }
}