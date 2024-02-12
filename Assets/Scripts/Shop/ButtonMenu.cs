using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonMenu : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] private GameObject Panel;

    [SerializeField] private CinemachineVirtualCamera cameraDisable;
    [SerializeField] private GameObject PanelDisable;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        button.onClick.AddListener(NextPanel);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(NextPanel);
    }

    private void NextPanel()
    {
        camera.gameObject.SetActive(true);
        Panel.gameObject.SetActive(true);

        cameraDisable.gameObject.SetActive(false);
        PanelDisable.gameObject.SetActive(false);
    }
}