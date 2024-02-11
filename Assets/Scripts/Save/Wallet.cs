using DG.Tweening;
using TMPro;
using UnityEngine;

public class Wallet
{
    private readonly TMP_Text textWallet;
    private int currentMoney;

    public Wallet(TMP_Text textWallet)
    {
        this.textWallet = textWallet;
    }

    public void AddMoney(int money)
    {
        currentMoney += money;
        textWallet.text = currentMoney.ToString();
        textWallet.transform.DOShakeScale(0.2f, 0.3f).OnComplete(() => textWallet.transform.DOScale(Vector3.one, 0.1f));
    }
}