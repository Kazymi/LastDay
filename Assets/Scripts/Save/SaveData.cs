using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] private bool hack;
    [SerializeField] private TMP_Text moneyText; //todo

    public bool isInfinitBullet;

    public bool lastHit;
    public bool IsTutorialOpen;
    public bool IsGameStarted;

    public int SpawnedZombie;

    public bool WeaponFound;

    public bool IsInvinsible
    {
        get => save.IsInvinsible;
        set => save.IsInvinsible = value;
    }

    private const string SaveKey = "TheDayLast";
    private Save save;

    public WeaponVersion CurrentWeaponVersion;
    public WeaponType FreeWeapon = WeaponType.PP;

    public List<WeaponSave> WeaponTypes => save.WeaponTypes;

    public bool IsTutorialLocationCompleted
    {
        get => save.IsTutorialLocationCompleted;
        set => save.IsTutorialLocationCompleted = value;
    }

    public bool IsTutorialMenuCompleted
    {
        get => save.IsTutorialMenuCompleted;
        set => save.IsTutorialMenuCompleted = value;
    }

    public static SaveData Instance;

    public LocalizationType LocalizationType
    {
        get => save.LocalizationType;
        set => save.LocalizationType = value;
    }

    public List<WeaponType> BuyWeapon
    {
        get => save.BuyWeapon;
        set => save.BuyWeapon = value;
    }

    public bool isSoundAcivated
    {
        get => save.IsSoundAcivated;
        set => save.IsSoundAcivated = value;
    }

    public int CurrentLevel
    {
        get => save.CurrentLevel;
        set => save.CurrentLevel = value;
    }

    public bool IsHeaveWeaponUnlock
    {
        get => save.IsHeavyWeaponUnlocker;
        set => save.IsHeavyWeaponUnlocker = value;
    }

    public List<AttachmentsSave> AttachList
    {
        get => save.AttachmentsSaves;
        set => save.AttachmentsSaves = value;
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

        // if (IsTutorialMenuCompleted == false)
        // {
        //     var isFirstMap = save.IsTutorialLocationCompleted;
        //     save = new Save();
        //     save.Initialize();
        //     save.IsTutorialLocationCompleted = isFirstMap;
        //     save.BuyWeapon = new List<WeaponType>();
        // }
        Instance = this;
        Wallet = new Wallet(moneyText, save);
        Save();
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

    public bool IsTutorialLocationCompleted;
    public bool IsInvinsible;
    public bool IsHeavyWeaponUnlocker;
    public bool IsTutorialMenuCompleted;

    public LocalizationType LocalizationType = LocalizationType.Ru;

    public bool IsSoundAcivated = true;
    public WeaponType SelectedWeapon = WeaponType.PP;
    public List<WeaponSave> WeaponTypes = new List<WeaponSave>();

    public void Initialize()
    {
        AttachmentsSaves = new List<AttachmentsSave>();
        foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
        {
            AttachmentsSaves.Add(new AttachmentsSave()
                {WeaponType = weaponType, AttachTypes = new List<AttachType>(), BoughtTypes = new List<AttachType>()});
        }

        WeaponTypes.Add(new WeaponSave() {WeaponType = WeaponType.PP, WeaponVersion = WeaponVersion.PP});
        WeaponTypes.Add(new WeaponSave() {WeaponType = WeaponType.AR, WeaponVersion = WeaponVersion.AR});
        WeaponTypes.Add(new WeaponSave() {WeaponType = WeaponType.MAuto, WeaponVersion = WeaponVersion.Heavy});
        WeaponTypes.Add(new WeaponSave() {WeaponType = WeaponType.Shotgun, WeaponVersion = WeaponVersion.ShotGun});
        WeaponTypes.Add(new WeaponSave() {WeaponType = WeaponType.Clear, WeaponVersion = WeaponVersion.Bonus});
        WeaponTypes.Add(new WeaponSave() {WeaponType = WeaponType.Clear, WeaponVersion = WeaponVersion.BonusSecond});
        BuyWeapon.Add(WeaponType.PP);
        BuyWeapon.Add(WeaponType.AR);
        BuyWeapon.Add(WeaponType.Shotgun);
        BuyWeapon.Add(WeaponType.MAuto);
    }
}

[Serializable]
public class AttachmentsSave
{
    public WeaponType WeaponType;
    public List<AttachType> AttachTypes;
    public List<AttachType> BoughtTypes;
}

[Serializable]
public class WeaponSave
{
    public WeaponType WeaponType;
    public WeaponVersion WeaponVersion;
}