using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] private bool setNewPosition;
    [SerializeField] private Vector3 position;
    [SerializeField] private MainMenuPanel nextPanel;
    [SerializeField] private GameObject panel;
    private IMainMenuController mainMenuController;

    private void Start()
    {
        mainMenuController = ServiceLocator.GetService<IMainMenuController>();
        mainMenuController.RegistrationPanel(this);
        GetComponent<Button>().onClick.AddListener(() => mainMenuController.ActivatePanel(nextPanel));
    }

    public void DisablePanel()
    {
        panel.gameObject.SetActive(false);
    }

    public void ActivatePanel()
    {
        panel.gameObject.SetActive(true);
        if (setNewPosition)
        {
            GameObject.FindWithTag("PlayerRotate").transform.localPosition = position;
        }
    }
}