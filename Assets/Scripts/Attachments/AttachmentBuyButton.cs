using System.Linq;
using CrazyGames;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AttachmentBuyButton : MonoBehaviour
{
    [SerializeField] private Sprite money;
    [SerializeField] private Sprite reward;
    private IAttachment attachment;
    private AttachType attachType;

    private int price;

    public void Initialize(AttachmentImage attachmentImage)
    {
        attachment = ServiceLocator.GetService<IAttachment>();
        var config = attachment.AttachmentConfigurationConstructor.AllAttachments
            .Where(t => t.AttachType == attachmentImage.AttachType).ToList()[0];
        if (config.isReward == false)
        {
            price = config.Price;
            GetComponentInChildren<TMP_Text>().SetText(price.ToString());
            for (int i = 0; i < transform.childCount; i++)
            {
                var image = transform.GetChild(i).GetComponent<Image>();
                if (image)
                {
                    image.sprite = money;
                }
            }
        }
        else
        {
            GetComponentInChildren<TMP_Text>().SetText("Free");
            for (int i = 0; i < transform.childCount; i++)
            {
                var image = transform.GetChild(i).GetComponent<Image>();
                if (image)
                {
                    image.sprite = reward;
                }
            }
        }

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
        if (price == 0)
        {
            CrazyAds.Instance.beginAdBreakRewarded(() =>
            {
                SaveData.Instance.AttachList.Where(t => t.WeaponType == attachment.CurrentWeapon).ToList()[0]
                    .BoughtTypes
                    .Add(attachType);
                SaveData.Instance.Save();
                Destroy(gameObject);
            });
        }
        else if (SaveData.Instance.Wallet.IsCanBeReduce(price))
        {
            SaveData.Instance.Wallet.ReduceMoney(price);
            SaveData.Instance.AttachList.Where(t => t.WeaponType == attachment.CurrentWeapon).ToList()[0].BoughtTypes
                .Add(attachType);
            SaveData.Instance.Save();
            Destroy(gameObject);
        }
    }
}