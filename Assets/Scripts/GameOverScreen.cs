using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup gameOverGroup;
    [SerializeField] private Button restartButton;
    [SerializeField] private Rock _rock;

    private void OnEnable()
    {
        _rock.Died += OnDied;
        restartButton.onClick.AddListener(OnRestartButtonClick);
    }
    private void OnDisable()
    {
        _rock.Died -= OnDied;
        restartButton.onClick.RemoveListener(OnRestartButtonClick);
    }

    private void Start()
    {
        gameOverGroup.alpha = 0;
    }

    private void OnDied()
    {
        gameOverGroup.alpha = 1;
        Time.timeScale = 0;
    }

    private void OnRestartButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
