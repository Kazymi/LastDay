using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AttachmentBuyButton : MonoBehaviour
{
    private IAttachment attachment;
    private AttachType attachType;

    private int price;

    public void Initialize(AttachmentImage attachmentImage)
    {
        attachment = ServiceLocator.GetService<IAttachment>();
        price = attachment.AttachmentConfigurationConstructor.AllAttachments
            .Where(t => t.AttachType == attachmentImage.AttachType).ToList()[0].Price;
        GetComponentInChildren<TMP_Text>().SetText(price.ToString());
        var attachSave = SaveData.Instance.AttachList.Where(t => t.WeaponType == attachment.CurrentWeapon).ToList()[0];
        attachType = attachmentImage.AttachType;
        if (attachSave.BoughtTypes.Contains(attachmentImage.AttachType))
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (SaveData.Instance.Wallet.IsCanBeReduce(price))
        {
            SaveData.Instance.Wallet.ReduceMoney(price);
            SaveData.Instance.AttachList.Where(t => t.WeaponType == attachment.CurrentWeapon).ToList()[0].BoughtTypes
                .Add(attachType);
            SaveData.Instance.Save();
            Destroy(gameObject);
        }
    }
}