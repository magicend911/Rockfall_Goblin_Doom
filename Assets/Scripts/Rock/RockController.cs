using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class RockController : MonoBehaviour
{
    [SerializeField] private Rock _rock;
    [SerializeField] private DeathZone _deathZone;
    [SerializeField] private float _moveSpeed = 5f; // Скорость движения шара
    [SerializeField] private AudioClip _rollingSound; // Звук катящегося камня
    [SerializeField] private float _minVelocityToPlaySound = 0.1f; // Минимальная скорость для воспроизведения звука
    [SerializeField] private float _maxVelocity = 10f; // Скорость, при которой звук достигает максимальной громкости
    [SerializeField] private float _maxVolume = 1f; // Максимальная громкость звука
    [SerializeField] private float _minPitch = 0.8f; // Минимальный питч звука
    [SerializeField] private float _maxPitch = 1.5f; // Максимальный питч звука
    [SerializeField] private LayerMask _groundLayer; // Слой для проверки земли
    [SerializeField] private float _groundCheckDistance = 0.1f; // Расстояние для проверки земли

    private Rigidbody _rb;
    private AudioSource _audioSource;
    private bool isPaused = false; // Флаг для проверки состояния паузы
    private bool isGrounded = false; // Флаг для проверки нахождения на земле

    private void Start()
    {
        // Инициализация компонентов
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        // Настройка источника звука
        _audioSource.clip = _rollingSound;
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
        _rb.AddForce(movement * _moveSpeed);
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
        isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);
    }

    private void HandleRollingSound()
    {
        float speed = _rb.velocity.magnitude;

        if (speed > _minVelocityToPlaySound)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }

            // Нормализуем скорость и регулируем громкость и питч
            float normalizedSpeed = Mathf.Clamp01(speed / _maxVelocity);
            _audioSource.volume = Mathf.Lerp(0, _maxVolume, normalizedSpeed);
            _audioSource.pitch = Mathf.Lerp(_minPitch, _maxPitch, normalizedSpeed);
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
        _moveSpeed = 0;
        _rb.velocity = Vector3.zero; // Останавливаем движение
        _rb.angularVelocity = Vector3.zero; // Останавливаем вращение
    }

    private void TakeControl()
    {
        _moveSpeed = 0;
    }
}
