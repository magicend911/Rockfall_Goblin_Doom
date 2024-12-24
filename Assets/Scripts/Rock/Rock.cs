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
    private bool isDead = false; // Флаг, указывающий на состояние смерти

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
        if (!isDead) // Звук обновляется только если объект не мертв
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
            isDead = true; // Устанавливаем состояние смерти
            _audioSource.Stop(); // Останавливаем звук
            Died?.Invoke();
        }
    }

    public void AddScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }
}