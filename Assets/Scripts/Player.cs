using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;

    void Start()
    {
        currentHealth = maxHealth;
        HealthChanged?.Invoke(currentHealth);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Died?.Invoke();
        }
    }
}
