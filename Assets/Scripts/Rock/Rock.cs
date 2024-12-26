using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class Rock : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private AudioSource _fullCrush; // Звук разрушения
    [SerializeField] private GameObject _crushPrefab; // Префаб груды камней

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
        _score++;
        ScoreChanged?.Invoke(_score);
    }
}