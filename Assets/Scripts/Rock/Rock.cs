using UnityEngine;
using UnityEngine.Events;

public class Rock : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    public int _score;

    public event UnityAction<int,int> HealthChanged;
    public event UnityAction<int> ScoreChanged; 
    public event UnityAction Died;

    void Start()
    {
        currentHealth = maxHealth;
        HealthChanged?.Invoke(currentHealth,maxHealth);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealthChanged?.Invoke(currentHealth,maxHealth);

        if (currentHealth <= 0)
        {
            Died?.Invoke();
        }
    }

    public void AddScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }
}
