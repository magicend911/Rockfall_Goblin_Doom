using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _destroyDelay = 2f; // Задержка перед уничтожением объекта
    [SerializeField] private AudioClip[] _deathSounds; // Массив звуков для смерти
    [SerializeField] private float _minPitch = 0.8f; // Минимальный питч звука
    [SerializeField] private float _maxPitch = 1.2f; // Максимальный питч звука

    private Animator _animator;
    private AudioSource _audioSource;
    private bool _isDead = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Rock rockScore = collision.GetComponent<Rock>();
        Enemy enemy = collision.GetComponent<Enemy>();

        if (rockScore != null || enemy != null && !_isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;

        // Устанавливаем анимацию смерти
        _animator.SetTrigger("Die");

        // Выбираем случайный звук
        if (_deathSounds.Length > 0)
        {
            AudioClip randomClip = _deathSounds[Random.Range(0, _deathSounds.Length)];
            _audioSource.clip = randomClip;

            // Задаем случайный питч
            _audioSource.pitch = Random.Range(_minPitch, _maxPitch);

            // Воспроизводим звук
            _audioSource.Play();
        }

        // Уничтожаем объект с задержкой
        Destroy(gameObject, _destroyDelay);
    }
}
