using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button _start; // ��������� ������, ������� � ����������

    private void Awake()
    {
        // ����������� ����� � ������� ������� �� ������
        _start.onClick.AddListener(LoadScene1);
    }

    private void LoadScene1()
    {
        SceneManager.LoadScene(1); // ��������� ����� � �������� 1
    }
}
