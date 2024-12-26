using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class RockController : MonoBehaviour
{
    [SerializeField] private Rock _rock;
    [SerializeField] private DeathZone _deathZone;
    [SerializeField] private float moveSpeed = 5f; // Скорость движения шара
    [SerializeField] private AudioClip rollingSound; // Звук катящегося камня
    [SerializeField] private float minVelocityToPlaySound = 0.1f; // Минимальная скорость для воспроизведения звука
    [SerializeField] private float maxVelocity = 10f; // Скорость, при которой звук достигает максимальной громкости
    [SerializeField] private float maxVolume = 1f; // Максимальная громкость звука
    [SerializeField] private float minPitch = 0.8f; // Минимальный питч звука
    [SerializeField] private float maxPitch = 1.5f; // Максимальный питч звука
    [SerializeField] private LayerMask groundLayer; // Слой для проверки земли
    [SerializeField] private float groundCheckDistance = 0.1f; // Расстояние для проверки земли

    private Rigidbody rb;
    private AudioSource _audioSource;
    private bool isPaused = false; // Флаг для проверки состояния паузы
    private bool isGrounded = false; // Флаг для проверки нахождения на земле

    private void Start()
    {
        // Инициализация компонентов
        rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        // Настройка источника звука
        _audioSource.clip = rollingSound;
        _audioSource.loop = true;
    }

    private void OnEnable()
    {
        _rock.StopMove += StopingMove;
        _deathZone.TakeMove += TakeControl;
    }

    private void OnDisable()
    {
        _rock.StopMove -= StopingMove;
        _deathZone.TakeMove += TakeControl;
    }

    private void FixedUpdate()
    {
        if (isPaused) return; // Если игра на паузе, отключаем управление

        CheckGround(); // Проверяем, находится ли шар на земле

        // Получаем ввод по осям
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Создаём вектор движения
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // Применяем силу к Rigidbody
        rb.AddForce(movement * moveSpeed);
    }

    private void Update()
    {
        if (!isPaused && isGrounded) // Обрабатываем звук только если объект на земле
        {
            HandleRollingSound();
        }
        else
        {
            StopSound();
        }
    }

    private void CheckGround()
    {
        // Проверяем, находится ли объект на поверхности
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    private void HandleRollingSound()
    {
        float speed = rb.velocity.magnitude;

        if (speed > minVelocityToPlaySound)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }

            // Нормализуем скорость и регулируем громкость и питч
            float normalizedSpeed = Mathf.Clamp01(speed / maxVelocity);
            _audioSource.volume = Mathf.Lerp(0, maxVolume, normalizedSpeed);
            _audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedSpeed);
        }
        else
        {
            StopSound();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        StopSound();
    }

    public void ResumeGame()
    {
        isPaused = false;
    }

    private void StopSound()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

    private void StopingMove()
    {
        moveSpeed = 0;
        rb.velocity = Vector3.zero; // Останавливаем движение
        rb.angularVelocity = Vector3.zero; // Останавливаем вращение
    }

    private void TakeControl()
    {
        moveSpeed = 0;
    }
}
