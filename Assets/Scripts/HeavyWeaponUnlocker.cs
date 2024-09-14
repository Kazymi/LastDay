using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HeavyWeaponUnlocker : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Initialize());
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    private void Click()
    {
        if (SaveData.Instance.Wallet.IsCanBeReduce(300))
        {
            SaveData.Instance.IsHeaveWeaponUnlock = true;
            SaveData.Instance.Wallet.ReduceMoney(300);
            Destroy(gameObject);
            SaveData.Instance.Save();
        }
    }

    private IEnumerator Initialize()
    {
        yield return null;
        if (SaveData.Instance.IsHeaveWeaponUnlock) Destroy(gameObject);
    }
}