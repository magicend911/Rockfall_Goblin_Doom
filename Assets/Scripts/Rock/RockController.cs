using UnityEngine;

public class RockController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Скорость движения шара
    private Rigidbody rb;

    private void Start()
    {
        // Получаем компонент Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Получаем ввод по осям
        float moveHorizontal = Input.GetAxis("Horizontal"); // Управление A/D или стрелками влево/вправо
        float moveVertical = Input.GetAxis("Vertical");     // Управление W/S или стрелками вверх/вниз

        // Создаём вектор движения
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // Применяем силу к Rigidbody
        rb.AddForce(movement * moveSpeed);
    }
}
