using UnityEngine;

[RequireComponent (typeof(AudioSource))]

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Rock playerHealth = other.GetComponent<Rock>();
        if (playerHealth != null)
        {
            _audioSource.Play();
            playerHealth.TakeDamage(_damage);
        }
    }
}
