using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private GameObject loseScren;
    [SerializeField] private PlayerHealthBase playerHealthBase;

    [SerializeField] private Button[] menuButton;
    [SerializeField] private Button restart;

    private void OnEnable()
    {
        foreach (var button in menuButton)
        {
            button.onClick.AddListener(Menu);
        }

        playerHealthBase.HealthEmpty += LoseScreenShow;
        restart.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        foreach (var button in menuButton)
        {
            button.onClick.RemoveListener(Menu);
        }

        playerHealthBase.HealthEmpty -= LoseScreenShow;
        restart.onClick.RemoveListener(Restart);
    }

    private void LoseScreenShow()
    {
        loseScren.gameObject.SetActive(true);
    }

    private void Restart()
    {
        SaveData.Instance.Save();
        SceneManager.LoadScene(1);
    }

    private void Menu()
    {
        SaveData.Instance.Save();
        SceneManager.LoadScene(0);
    }
}