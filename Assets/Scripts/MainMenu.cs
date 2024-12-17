using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button _start; // Приватная кнопка, видимая в инспекторе

    private void Awake()
    {
        // Привязываем метод к событию нажатия на кнопку
        _start.onClick.AddListener(LoadScene1);
    }

    private void LoadScene1()
    {
        SceneManager.LoadScene(1); // Загружаем сцену с индексом 1
    }
}
