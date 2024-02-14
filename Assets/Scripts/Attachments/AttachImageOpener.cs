using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AttachImageOpener : MonoBehaviour
{
    [SerializeField] private GameObject[] activateObjects;

    private bool IsActive = false;
    private AttachmentList parent;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        foreach (var activateObject in activateObjects)
        {
            activateObject.gameObject.SetActive(!IsActive);
        }

        parent ??= GetComponentInParent<AttachmentList>();
        IsActive = !IsActive;
        if (IsActive)
        {
            ServiceLocator.GetService<IAttachment>().EnableOnlyCurrentList(parent);
        }
        else
        {
            ServiceLocator.GetService<IAttachment>().EnableAllList();
        }
    }
}