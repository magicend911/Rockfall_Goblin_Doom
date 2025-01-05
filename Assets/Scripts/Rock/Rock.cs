using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class Rock : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private AudioSource _fullCrush; 
    [SerializeField] private GameObject _crushPrefab; 

    public int CurrentHealth { get; private set; }
    public int Score { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> ScoreChanged;
    public event UnityAction Died;
    public event UnityAction StopMove;

    void Start()
    {
        CurrentHealth = _maxHealth;
        HealthChanged?.Invoke(CurrentHealth, _maxHealth);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        HealthChanged?.Invoke(CurrentHealth, _maxHealth);

        if (CurrentHealth <= 0)
        {
            _fullCrush.Play();
            StopMove?.Invoke();
            StartCoroutine(DestroyAndReplace());
        }
    }

    private IEnumerator DestroyAndReplace()
    {
        yield return new WaitForSeconds(1f); // Задержка перед заменой

        // Создаём груду камней на месте текущего объекта
        Instantiate(_crushPrefab, transform.position, transform.rotation);

        // Вызываем событие смерти
        Died?.Invoke();

        // Удаляем текущий объект
        Destroy(gameObject);
    }

    public void AddScore()
    {
        Score++;
        ScoreChanged?.Invoke(Score);
    }
}