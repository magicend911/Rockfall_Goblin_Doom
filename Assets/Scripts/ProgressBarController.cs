using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] private Transform _rock; // Игрок (шар)
    [SerializeField] private Transform _startPoint; // Начальная точка трассы
    [SerializeField] private Transform _endPoint; // Конечная точка трассы
    [SerializeField] private Slider _progressBar; // Ссылка на слайдер

    private float totalDistance;

    private void Start()
    {
        // Рассчитываем полное расстояние между начальной и конечной точками
        totalDistance = Vector3.Distance(_startPoint.position, _endPoint.position);
    }

    private void Update()
    {
        // Текущее расстояние от игрока до начальной точки
        float currentDistance = Vector3.Distance(_rock.position, _startPoint.position);

        // Рассчитываем прогресс как долю от общего расстояния
        float progress = Mathf.Clamp01(currentDistance / totalDistance);

        // Обновляем значение слайдера
        _progressBar.value = progress;
    }
}
