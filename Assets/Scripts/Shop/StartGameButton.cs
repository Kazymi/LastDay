using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartGameButton : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(StarGame);
    }

    private void StarGame()
    {
        SaveData.Instance.IsGameStarted = true;
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, 1.3f).OnComplete(() => SceneManager.LoadScene(1));
    }
}