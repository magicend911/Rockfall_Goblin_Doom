using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] private Transform _rock;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Slider _progressBar;

    private float totalDistance;

    private void Start()
    {
        totalDistance = Vector3.Distance(_startPoint.position, _endPoint.position);
    }

    private void Update()
    {
        if (_rock == null) return; // Проверяем, существует ли объект _rock

        float currentDistance = Vector3.Distance(_rock.position, _startPoint.position);

        float progress = Mathf.Clamp01(currentDistance / totalDistance);

        _progressBar.value = progress;
    }

    public void SetRock(Transform newRock)
    {
        _rock = newRock; // Метод для обновления ссылки на камень
    }
}
