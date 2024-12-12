using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f; // Скорость движения вперед (по X)
    [SerializeField] private float laneDistance = 3f; // Расстояние между полосами
    [SerializeField] private Rock player;

    private Rigidbody _rb;
    private int targetLane = 1; // Текущая полоса (0, 1, 2)
    private Vector3 targetPosition;

    private void OnEnable()
    {
        player.Died += OnDied;
    }

    private void OnDisable()
    {
        player.Died -= OnDied;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>(); // Получаем компонент Rigidbody
    }

    void Start()
    {
        targetPosition = transform.position; // Инициализация начальной позиции
    }

    void FixedUpdate()
    {
        // Движение вперед по X
        _rb.AddForce(Vector3.right * forwardSpeed, ForceMode.Force);

        // Рассчет целевой позиции для смены полос
        float targetZ = (targetLane - 1) * laneDistance; // Смещение по Z
        Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, targetZ);

        // Плавное смещение по Z
        Vector3 movementDelta = desiredPosition - transform.position;
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, movementDelta.z * 10f);

    }

    void Update()
    {
        // Смена полос (по Y)
        if (Input.GetKeyDown(KeyCode.A) && targetLane < 2)
        {
            targetLane++;
        }
        else if (Input.GetKeyDown(KeyCode.D) && targetLane > 0)
        {
            targetLane--;
        }
    }

    private void OnDied()
    {
        forwardSpeed = 0; // Остановка движения
        _rb.velocity = Vector3.zero; // Остановка физического объекта
    }
}
