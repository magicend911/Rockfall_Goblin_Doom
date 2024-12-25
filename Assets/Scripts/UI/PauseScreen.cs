using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Button _back;
    [SerializeField] private Button _exit;
    [SerializeField] private Button _restart;
    [SerializeField] private Rock _rock; // ������ �� ������ Rock

    private void OnEnable()
    {
        _restart.onClick.AddListener(OnRestartButtonClick);
        _exit.onClick.AddListener(OnExitButtonClick);
        _back.onClick.AddListener(OnBackButtonClick);
        PauseGame(); // ������������� ���� � ���������� Rock
    }

    private void OnDisable()
    {
        _restart.onClick.RemoveListener(OnRestartButtonClick);
        _exit.onClick.RemoveListener(OnExitButtonClick);
        _back.onClick.RemoveListener(OnBackButtonClick);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;

        if (_rock != null)
        {
            _rock.PauseGame(); // ������������� ���� � Rock
        }
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;

        if (_rock != null)
        {
            _rock.ResumeGame(); // ������������ ���� � Rock
        }
    }

    private void OnBackButtonClick()
    {
        ResumeGame(); // ������������ ����
        _pausePanel.SetActive(false);
    }

    private void OnRestartButtonClick()
    {
        ResumeGame(); // ������������ ����� ����� ������������
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    private void OnExitButtonClick()
    {
        ResumeGame(); // ������������ ����� ����� �������
        SceneManager.LoadScene(0);
    }
}
