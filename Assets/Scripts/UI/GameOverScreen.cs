using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _gameOverGroup;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Rock _rock;
    [SerializeField] private RockController _rockSound;
    [SerializeField] private DeathZone _gameOver;

    private void OnEnable()
    {
        _gameOver.GameOver += OnDied;
        _rock.Died += OnDied;
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }
    private void OnDisable()
    {
        _rock.Died -= OnDied;
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _gameOver.GameOver -= OnDied;
    }

    private void Start()
    {
        _gameOverGroup.interactable = false;
        _gameOverGroup.blocksRaycasts = false;
        _gameOverGroup.alpha = 0;
    }

    private void OnDied()
    {
        _gameOverGroup.interactable = true;
        _gameOverGroup.blocksRaycasts = true;
        _gameOverGroup.alpha = 1;
        Time.timeScale = 0;
        _rockSound.PauseGame();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnRestartButtonClick()
    {
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}