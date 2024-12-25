using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class Rock : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private AudioClip rollingSound;
    [SerializeField] private float minVelocityToPlaySound = 0.1f;
    [SerializeField] private float maxVelocity = 10f;
    [SerializeField] private float maxVolume = 1f;
    [SerializeField] private float minPitch = 0.8f;
    [SerializeField] private float maxPitch = 1.5f;

    private int currentHealth;
    public int _score;

    private AudioSource _audioSource;
    private Rigidbody _rigidbody;
    private bool isDead = false; // Флаг для состояния смерти
    private bool isPaused = false; // Флаг для остановки игры

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> ScoreChanged;
    public event UnityAction Died;

    void Start()
    {
        currentHealth = maxHealth;
        HealthChanged?.Invoke(currentHealth, maxHealth);

        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();

        _audioSource.clip = rollingSound;
        _audioSource.loop = true;
    }

    void Update()
    {
        if (!isPaused) // Звук обновляется только если объект жив и игра не на паузе
        {
            HandleRollingSound();
        }
    }

    private void HandleRollingSound()
    {
        float speed = _rigidbody.velocity.magnitude;

        if (speed > minVelocityToPlaySound)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }

            float normalizedSpeed = Mathf.Clamp01(speed / maxVelocity);
            _audioSource.volume = Mathf.Lerp(0, maxVolume, normalizedSpeed);
            _audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedSpeed);
        }
        else
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Pause();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0 && !isDead)
        {
            StartCoroutine(DelayedDeath());
        }
    }

    private IEnumerator DelayedDeath()
    {
        yield return new WaitForSeconds(2f); // Задержка в 2 секунды.
        Died?.Invoke();
    }

    public void AddScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }

    public void PauseGame()
    {
        isPaused = true; // Устанавливаем флаг паузы
        StopSound(); // Останавливаем звук
    }

    public void ResumeGame()
    {
        isPaused = false; // Снимаем флаг паузы
    }

    private void StopSound()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}