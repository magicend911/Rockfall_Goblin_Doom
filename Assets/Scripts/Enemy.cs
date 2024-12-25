using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    [SerializeField] private float _destroyDelay = 2f;

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

        if (rockScore != null && !_isDead)
        {
            Die(); 
        }
    }

    private void Die()
    {
        _isDead = true;
        _animator.SetTrigger("Die");

        _audioSource.Play();
        Destroy(gameObject, _destroyDelay);
    }
}
