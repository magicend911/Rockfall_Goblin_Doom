using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class DeathZone : MonoBehaviour
{
    private AudioSource _audioSource;

    public event UnityAction TakeMove;
    public event UnityAction GameOver;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Rock playerHealth = other.GetComponent<Rock>();
        if (playerHealth != null)
        {
            TakeMove?.Invoke();
            StartCoroutine(DelayedDeath());
        }
    }
    private IEnumerator DelayedDeath()
    {
        yield return new WaitForSeconds(2f);
        GameOver?.Invoke();
    }
}
