using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WinScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup gameWinGroup;
    [SerializeField] private FinishLine _finish;
    [SerializeField] private Button _next;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _quit;
    [SerializeField] private RockController _rock;

    private void OnEnable()
    {
        _finish.RockFinished += OnWin;
        _next.onClick.AddListener(OnNextLevelButtonClick);
        _restart.onClick.AddListener(OnRestartButtonClick);
        _quit.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _finish.RockFinished -= OnWin;
        _next.onClick.RemoveListener(OnNextLevelButtonClick);
        _restart.onClick.RemoveListener(OnRestartButtonClick);
        _quit.onClick.RemoveListener(OnExitButtonClick);
    }

    private void Start()
    {
        gameWinGroup.alpha = 0;
        gameWinGroup.interactable = false;
        gameWinGroup.blocksRaycasts = false;
        Time.timeScale = 1;
    }

    private void OnWin()
    {
        gameWinGroup.alpha = 1;
        gameWinGroup.interactable = true; // �������� ��������������
        gameWinGroup.blocksRaycasts = true; // �������� �������� ������
        Time.timeScale = 0;
        _rock.PauseGame();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnNextLevelButtonClick()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
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
