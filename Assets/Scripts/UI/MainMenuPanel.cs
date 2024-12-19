using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] private Button _start;
    [SerializeField] private Button _quit;

    private void OnEnable()
    {
        _start.onClick.AddListener(OnStartButtonClick);
        _quit.onClick.AddListener(OnExitButtonClick);

    }

    private void OnDisable()
    {
        _start.onClick.RemoveListener(OnStartButtonClick);
        _quit.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnStartButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    private void OnExitButtonClick() 
    {
        Application.Quit();
    }
}
