using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Attachment : MonoBehaviour, IAttachment
{
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private AttachmentList[] attachList;
    [SerializeField] private AttachmentConfigurationConstructor attachConfigurations;

    public WeaponType CurrentWeapon => weaponType;
    public AttachmentConfigurationConstructor AttachmentConfigurationConstructor => attachConfigurations;

    private void OnEnable()
    {
        ServiceLocator.Subscribe<IAttachment>(this);
        Initialize();
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<IAttachment>();
    }

    private void Initialize()
    {
        ClearOldAttach();
        LoadAttach();
        EnableAllList();
        InitializeSubImage();
    }

    private void InitializeSubImage()
    {
        foreach (var attachmentList in attachList)
        {
            foreach (var attachmentImage in attachmentList.AttachmentImages)
            {
                attachmentImage.gameObject.SetActive(false);
            }

            for (int i = 0; i < attachmentList.Types.Length; i++)
            {
                attachmentList.AttachmentImages[i]
                    .Setup(
                        attachConfigurations.AllAttachments.Where(t => t.AttachType == attachmentList.Types[i])
                            .ToList()[0].Icon, attachmentList.Types[i], attachmentList);
            }
        }
    }

    private void ClearOldAttach()
    {
        foreach (var attachList in attachList)
        {
            ClearAttachment(attachList);
        }
    }

    private void LoadAttach()
    {
        var saveData = SaveData.Instance.AttachList;
        foreach (var attachmentList in attachList)
        {
            foreach (var attachType in attachmentList.Types)
            {
                foreach (var save in saveData)
                {
                    if (save.WeaponType == weaponType)
                    {
                        if (save.AttachTypes.Contains(attachType))
                        {
                            ConnectAttachment(attachmentList, attachType);
                        }
                    }
                }
            }
        }
    }

    public void DisableAllList()
    {
        foreach (var attachmentList in attachList)
        {
            attachmentList.gameObject.SetActive(false);
        }
    }

    public void EnableAllList()
    {
        foreach (var attachmentList in attachList)
        {
            attachmentList.gameObject.SetActive(true);
        }
    }

    public void EnableOnlyCurrentList(AttachmentList attachmentList)
    {
        DisableAllList();
        attachmentList.gameObject.SetActive(true);
    }

    public void ConnectAttachment(AttachmentList attachmentList, AttachType attachType)
    {
        Debug.Log("123");
        ClearAttachment(attachmentList, true);
        attachmentList.attachType = attachType;
        var currentConfiguration =
            attachConfigurations.AllAttachments.Where(t => t.AttachType == attachType).ToList()[0];
        attachmentList.MainImage.sprite = currentConfiguration.Icon;
        attachmentList.CanBeAttachImage.gameObject.SetActive(false);
        var newAttachObject = Instantiate(currentConfiguration.attachObject, attachmentList.Center);
        newAttachObject.transform.localPosition = Vector3.zero;
        newAttachObject.transform.localRotation = Quaternion.identity;
        attachmentList.Center.transform.DOShakeScale(0.2f, 0.3f)
            .OnComplete(() => attachmentList.Center.transform.DOScale(Vector3.one, 0.1f));
        var save = SaveData.Instance.AttachList.Where(t => t.WeaponType == weaponType).ToList()[0];
        if (save.AttachTypes.Contains(attachType) == false)
        {
            save.AttachTypes.Add(attachType);
        }

        SaveData.Instance.Save();
    }


    private void ClearAttachment(AttachmentList attachList, bool needClearSave = false)
    {
        if (needClearSave)
        {
            SaveData.Instance.AttachList.Where(t => t.WeaponType == weaponType).ToList()[0].AttachTypes
                .Remove(attachList.attachType);
        }

        var parent = attachList.Center;
        var childs = new List<GameObject>();
        for (int i = 0; i < parent.childCount; i++)
        {
            childs.Add(parent.GetChild(i).gameObject);
        }

        DestroyObjectList(childs);
    }

    private void DestroyObjectList(List<GameObject> objects)
    {
        if (objects.Count == 0)
        {
            return;
        }

        var destroyObject = objects[0];
        objects.Remove(destroyObject);
        Destroy(destroyObject.gameObject);
        DestroyObjectList(objects);
    }
}