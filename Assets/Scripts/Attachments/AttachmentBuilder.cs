using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttachmentBuilder : MonoBehaviour
{
    [SerializeField] private bool isDestoryOutline;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private AttachmentConfigurationConstructor attachConfiguration;
    [SerializeField] private AttachmentBulderList[] attachmentBulderList;

    private void OnEnable()
    {
        Show();
    }

    private void Show()
    {
        ClearChild();
        var saveData = SaveData.Instance.AttachList.Where(t => t.WeaponType == weaponType).ToList()[0];
        foreach (var attach in saveData.AttachTypes)
        {
            Debug.Log("Show");
            var foundBuildList = GetList(GetConfigBuild(attach));
            var foundPrefab = attachConfiguration.AllAttachments.Where(t => t.AttachType == attach).ToList()[0]
                .attachObject;
            var newObject = Instantiate(foundPrefab, foundBuildList.position);
            newObject.transform.localPosition = Vector3.zero;
            newObject.transform.localRotation = Quaternion.identity;
            if (isDestoryOutline)
            {
                var outline = newObject.GetComponent<Outline>();
                if (outline)
                {
                    Destroy(outline);
                }
            }
        }
    }

    private AttachmentBulderConfig GetConfigBuild(AttachType attachType)
    {
        switch (attachType)
        {
            case AttachType.Default:
                break;
            case AttachType.Scope_1:
                return AttachmentBulderConfig.Scope;
                break;
            case AttachType.Scope_2:
                return AttachmentBulderConfig.Scope;
                break;
            case AttachType.Scope_3:
                return AttachmentBulderConfig.Scope;
                break;
            case AttachType.Scope_4:
                return AttachmentBulderConfig.Scope;
                break;
            case AttachType.Scope_5:
                return AttachmentBulderConfig.Scope;
                break;
            case AttachType.Scope_6:
                return AttachmentBulderConfig.Scope;
                break;
            case AttachType.Scope_7:
                return AttachmentBulderConfig.Scope;
                break;
            case AttachType.Flash_1:
                return AttachmentBulderConfig.Suppressor;
                break;
            case AttachType.Flash_2:
                return AttachmentBulderConfig.Suppressor;
                break;
            case AttachType.Flash_3:
                return AttachmentBulderConfig.Suppressor;
                break;
            case AttachType.Flash_4:
                return AttachmentBulderConfig.Suppressor;
                break;
            case AttachType.Grid_1:
                return AttachmentBulderConfig.Grid;
                break;
            case AttachType.Grid_2:
                return AttachmentBulderConfig.Grid;
                break;
            case AttachType.Grid_3:
                return AttachmentBulderConfig.Grid;
                break;
            case AttachType.Grid_4:
                return AttachmentBulderConfig.Grid;
                break;
            case AttachType.Grid_5:
                return AttachmentBulderConfig.Grid;
                break;
            case AttachType.Lase_1:
                break;
            case AttachType.M249Back_1:
                break;
            case AttachType.M249Back_2:
                break;
            case AttachType.M249Back_3:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(attachType), attachType, null);
        }

        return AttachmentBulderConfig.Grid;
    }

    private AttachmentBulderList GetList(AttachmentBulderConfig attachmentBulderConfig)
    {
        foreach (var attachmentBulderList in attachmentBulderList)
        {
            if (attachmentBulderConfig == attachmentBulderList.attachmentBulderConfig) return attachmentBulderList;
        }

        return null;
    }

    private void ClearChild()
    {
        foreach (var attachmentBulderList in attachmentBulderList)
        {
            var child = new List<GameObject>();
            for (int i = 0; i < attachmentBulderList.position.childCount; i++)
            {
                child.Add(attachmentBulderList.position.GetChild(i).gameObject);
            }

            DestroyObjectList(child);
        }
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