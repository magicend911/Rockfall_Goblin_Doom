using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WinScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _gameWinGroup;
    [SerializeField] private FinishLine _finish;
    [SerializeField] private Button _next;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _quit;
    [SerializeField] private RockController _rockController;
    [SerializeField] private Rock _rock;
    [SerializeField] private GameObject _oneStar;
    [SerializeField] private GameObject _twoStars;
    [SerializeField] private GameObject _threeStars;

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
        _gameWinGroup.alpha = 0;
        _gameWinGroup.interactable = false;
        _gameWinGroup.blocksRaycasts = false;
        Time.timeScale = 1;
    }

    private void OnWin()
    {
        Time.timeScale = 0;
        _rockController.PauseGame();
        CursorTrue();
        InteractableWinGroup();
        CurrentStarsForWin();
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

    private void CursorTrue()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void InteractableWinGroup()
    {
        _gameWinGroup.alpha = 1;
        _gameWinGroup.interactable = true;
        _gameWinGroup.blocksRaycasts = true;
    }

    private void CurrentStarsForWin()
    {
        if (_rock.Score >= 50 && _rock.CurrentHealth >= 75)
        {
            _threeStars.SetActive(true);
            _twoStars.SetActive(false);
            _oneStar.SetActive(false);
        }
        else if (_rock.CurrentHealth >= 75)
        {
            _threeStars.SetActive(false);
            _twoStars.SetActive(true);
            _oneStar.SetActive(false);
        }
        else
        {
            _threeStars.SetActive(false);
            _twoStars.SetActive(false);
            _oneStar.SetActive(true);
        }
    }
}

