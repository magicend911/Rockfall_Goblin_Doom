using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class Rock : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;
    private int _score;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> ScoreChanged;
    public event UnityAction Died;
    public event UnityAction StopMove;

    void Start()
    {
        currentHealth = maxHealth;
        HealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            StopMove?.Invoke();
            StartCoroutine(DelayedDeath());
        }
    }

    private IEnumerator DelayedDeath()
    {
        yield return new WaitForSeconds(2f);
        Died?.Invoke();
    }

    public void AddScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }
}