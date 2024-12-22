using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Button _back;
    [SerializeField] private Button _exit;
    [SerializeField] private Button _restart;

    private void OnEnable()
    {
        _restart.onClick.AddListener(OnRestartButtonClick);
        _exit.onClick.AddListener(OnExitButtonClick);
        _back.onClick.AddListener(OnBackButtonClick);
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        _restart.onClick.RemoveListener(OnRestartButtonClick);
        _exit.onClick.RemoveListener(OnExitButtonClick);
        _back.onClick.RemoveListener(OnBackButtonClick);
    }

    private void OnBackButtonClick()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
    }

    private void OnRestartButtonClick()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    private void OnExitButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
