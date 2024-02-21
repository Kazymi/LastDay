using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    private bool isOpen;

    private void Start()
    {
        FindObjectOfType<PlayerHealthBase>().HealthEmpty += DisableUI;
    }
    public void UpdateState()
    {
        isOpen = !isOpen;
        menu.gameObject.SetActive(isOpen);
    }

    private void DisableUI()
    {
        menu.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    
    public void FinishGame()
    {
        FindObjectOfType<PlayerDamageTaker>().TakeDamage(999);
        UpdateState();
        gameObject.SetActive(false);
    }
}