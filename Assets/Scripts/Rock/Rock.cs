using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class Rock : MonoBehaviour
{
    [Header("General Parameters")]
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private AudioSource _fullCrush; 
    [SerializeField] private GameObject _crushPrefab;

    [Header("Invulnerable Parameters")]
    [SerializeField] private float _invulnerableTime = 5f; // LEAZY врем€ неу€звимости
    [SerializeField] private ParticleSystem _sparks; // LEAZY искры частицы
    [SerializeField] private Color _color = new Color(209, 0, 207); // LEAZY цвет валуна во врем€ неу€звимости

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> ScoreChanged;
    public event UnityAction Died;
    public event UnityAction StopMove;

    public int CurrentHealth { get; private set; }
    public int Score { get; private set; }
    public bool IsInvulnerable { get; private set; } // LEAZY проверка на наличие неу€звимости в конкретный момент

    private Renderer _rockRenderer;

    private void Awake()
    {
        _rockRenderer = GetComponent<Renderer>();
    }

    void Start()
    {
        CurrentHealth = _maxHealth;
        HealthChanged?.Invoke(CurrentHealth, _maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !IsInvulnerable)
        {
            StartCoroutine(BecomeInvulnerable());
        }
    }

    public void TakeDamage(int damage)
    {
        if (!IsInvulnerable) // LEAZY добавила проверку на неу€звимость
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
    }

    public void AddScore()
    {
        Score++;
        ScoreChanged?.Invoke(Score);
    }

    private IEnumerator DestroyAndReplace()
    {
        yield return new WaitForSeconds(1f);

        Instantiate(_crushPrefab, transform.position, transform.rotation);
        Died?.Invoke();
        Destroy(gameObject);
    }

    private IEnumerator BecomeInvulnerable() // LEAZY корутина дл€ неу€звимости
    {
        IsInvulnerable = true;
        _rockRenderer.material.color = _color;
        _sparks.Play();

        yield return new WaitForSeconds(_invulnerableTime);

        _sparks.Stop();
        _rockRenderer.material.color = Color.white;
        IsInvulnerable = false;
    }
}