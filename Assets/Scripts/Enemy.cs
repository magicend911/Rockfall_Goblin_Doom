using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _destroyDelay = 2f; // �������� ����� ������������ �������
    [SerializeField] private AudioClip[] _deathSounds; // ������ ������ ��� ������
    [SerializeField] private float _minPitch = 0.8f; // ����������� ���� �����
    [SerializeField] private float _maxPitch = 1.2f; // ������������ ���� �����

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

        // ������������� �������� ������
        _animator.SetTrigger("Die");

        // �������� ��������� ����
        if (_deathSounds.Length > 0)
        {
            AudioClip randomClip = _deathSounds[Random.Range(0, _deathSounds.Length)];
            _audioSource.clip = randomClip;

            // ������ ��������� ����
            _audioSource.pitch = Random.Range(_minPitch, _maxPitch);

            // ������������� ����
            _audioSource.Play();
        }

        // ���������� ������ � ���������
        Destroy(gameObject, _destroyDelay);
    }
}
