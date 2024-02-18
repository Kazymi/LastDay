using DG.Tweening;
using TMPro;
using UnityEngine;

public class Wallet
{
    private readonly TMP_Text textWallet;
    private Save saveData;

    public Wallet(TMP_Text textWallet, Save saveData)
    {
        this.textWallet = textWallet;
        this.saveData = saveData;
        textWallet.text = saveData.Money.ToString();
        textWallet.transform.DOShakeScale(0.2f, 0.3f).OnComplete(() => textWallet.transform.DOScale(Vector3.one, 0.1f));
    }

    public void AddMoney(int money)
    {
        saveData.Money += money;
        textWallet.text = saveData.Money.ToString();
        textWallet.transform.DOShakeScale(0.2f, 0.3f).OnComplete(() => textWallet.transform.DOScale(Vector3.one, 0.1f));
    }

    public void ReduceMoney(int money)
    {
        saveData.Money -= money;
        textWallet.text = saveData.Money.ToString();
        textWallet.transform.DOShakeScale(0.2f, 0.3f).OnComplete(() => textWallet.transform.DOScale(Vector3.one, 0.1f));
    }

    public void SetMoney(int money)
    {
        saveData.Money = money;
        textWallet.text = saveData.Money.ToString();
        textWallet.transform.DOShakeScale(0.2f, 0.3f).OnComplete(() => textWallet.transform.DOScale(Vector3.one, 0.1f));
    }
    public bool IsCanBeReduce(int money)
    {
        return saveData.Money >= money;
    }
}