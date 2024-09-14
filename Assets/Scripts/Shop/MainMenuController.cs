using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour, IMainMenuController
{
    private List<MainMenuPanel> _mainMenuPanels = new List<MainMenuPanel>();

    private void OnEnable()
    {
        ServiceLocator.Subscribe<IMainMenuController>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<IMainMenuController>();
    }

    public void DisableAllPanel()
    {
        foreach (var mainMenuPanel in _mainMenuPanels)
        {
            mainMenuPanel.DisablePanel();
        }
    }

    public void ActivatePanel(MainMenuPanel mainMenuPanel)
    {
        DisableAllPanel();
        mainMenuPanel.ActivatePanel();
    }

    public void RegistrationPanel(MainMenuPanel mainMenuPanel)
    {
        _mainMenuPanels.Add(mainMenuPanel);
    }
}

[RequireComponent(typeof(Button))]
public class CloseShopButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(UpdateCharacter);
    }

    private void UpdateCharacter()
    {
        
    }
}