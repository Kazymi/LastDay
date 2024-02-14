using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] private bool hack;
    [SerializeField] private TMP_Text moneyText; //todo

    private const string SaveKey = "TheDayLast";
    private Save save;

    public static SaveData Instance;

    public List<WeaponType> BuyWeapon
    {
        get => save.BuyWeapon;
        set => save.BuyWeapon = value;
    }

    public int CurrentLevel
    {
        get => save.CurrentLevel;
        set => save.CurrentLevel = value;
    }

    public List<AttachmentsSave> AttachList
    {
        get => save.AttachmentsSaves;
        set => save.AttachmentsSaves = value;
    }

    public WeaponType FreeWeapon
    {
        get => save.FreeWeapon;
        set => save.FreeWeapon = value;
    }

    public WeaponType SelectedWeapon
    {
        get => save.SelectedWeapon;
        set => save.SelectedWeapon = value;
    }

    public Wallet Wallet { get; private set; }

    private void Awake()
    {
        save = Load();
        if (save == null)
        {
            save = new Save();
            save.Initialize();
        }

        Instance = this;
        Wallet = new Wallet(moneyText, save);
    }

    public void Save()
    {
        Save(save);
    }

    private void Save(Save saveData)
    {
        var saveString = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SaveKey, saveString);
        PlayerPrefs.Save();
    }

    private Save Load()
    {
        var loadedDate = PlayerPrefs.GetString(SaveKey);
        var saveDate = JsonUtility.FromJson<Save>(loadedDate);
        return saveDate;
    }
}

[Serializable]
public class Save
{
    public int Money;
    public List<WeaponType> BuyWeapon = new List<WeaponType>() {WeaponType.PP};
    public List<AttachmentsSave> AttachmentsSaves;
    public int CurrentLevel;

    public WeaponType FreeWeapon;
    public WeaponType SelectedWeapon = WeaponType.PP;

    public void Initialize()
    {
        AttachmentsSaves = new List<AttachmentsSave>();
        foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
        {
            AttachmentsSaves.Add(new AttachmentsSave()
                {WeaponType = weaponType, AttachTypes = new List<AttachType>(), BoughtTypes = new List<AttachType>()});
        }
    }
}

[Serializable]
public class AttachmentsSave
{
    public WeaponType WeaponType;
    public List<AttachType> AttachTypes;
    public List<AttachType> BoughtTypes;
}