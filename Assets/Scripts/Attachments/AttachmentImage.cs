using EventBusSystem;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class AttachmentImage : MonoBehaviour
{
    
    [SerializeField] private AttachmentBuyButton attachmentBuyButton;
    private Image image;
    private AttachType attachType;
    private AttachmentList attachmentList;

    private IAttachment attachment;
    private Button button;

    public AttachType AttachType => attachType;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnClick);
        if (attachType == AttachType.Default)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        attachment = ServiceLocator.GetService<IAttachment>();
        attachment.ConnectAttachment(attachmentList, attachType);
        EventBus.RaiseEvent<IAttachmentUpdate>(t => t.AttachmentUpdated());
    }

    public void Setup(Sprite sprite, AttachType attachType, AttachmentList attachmentList)
    {
        attachment = null;
        this.attachType = attachType;
        image ??= GetComponent<Image>();
        image.sprite = sprite;
        this.attachmentList = attachmentList;
        if (attachmentBuyButton != null)
        {
            attachmentBuyButton.Initialize(this);
        }
    }
}