using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class RockController : MonoBehaviour
{
    [SerializeField] private Rock _rock;
    [SerializeField] private DeathZone _deathZone;
    [SerializeField] private float _moveSpeed = 5f; // �������� �������� ����
    [SerializeField] private AudioClip _rollingSound; // ���� ���������� �����
    [SerializeField] private float _minVelocityToPlaySound = 0.1f; // ����������� �������� ��� ��������������� �����
    [SerializeField] private float _maxVelocity = 10f; // ��������, ��� ������� ���� ��������� ������������ ���������
    [SerializeField] private float _maxVolume = 1f; // ������������ ��������� �����
    [SerializeField] private float _minPitch = 0.8f; // ����������� ���� �����
    [SerializeField] private float _maxPitch = 1.5f; // ������������ ���� �����
    [SerializeField] private LayerMask _groundLayer; // ���� ��� �������� �����
    [SerializeField] private float _groundCheckDistance = 0.1f; // ���������� ��� �������� �����

    private Rigidbody _rb;
    private AudioSource _audioSource;
    private bool isPaused = false; // ���� ��� �������� ��������� �����
    private bool isGrounded = false; // ���� ��� �������� ���������� �� �����

    private void Start()
    {
        // ������������� �����������
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        // ��������� ��������� �����
        _audioSource.clip = _rollingSound;
        _audioSource.loop = true;
    }

    private void OnEnable()
    {
        _rock.StopMove += StopingMove;
        _deathZone.TakeMove += TakeControl;
    }

    private void OnDisable()
    {
        _rock.StopMove -= StopingMove;
        _deathZone.TakeMove += TakeControl;
    }

    private void FixedUpdate()
    {
        if (isPaused) return; // ���� ���� �� �����, ��������� ����������

        CheckGround(); // ���������, ��������� �� ��� �� �����

        // �������� ���� �� ����
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // ������ ������ ��������
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // ��������� ���� � Rigidbody
        _rb.AddForce(movement * _moveSpeed);
    }

    private void Update()
    {
        if (!isPaused && isGrounded) // ������������ ���� ������ ���� ������ �� �����
        {
            HandleRollingSound();
        }
        else
        {
            StopSound();
        }
    }

    private void CheckGround()
    {
        // ���������, ��������� �� ������ �� �����������
        isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);
    }

    private void HandleRollingSound()
    {
        float speed = _rb.velocity.magnitude;

        if (speed > _minVelocityToPlaySound)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }

            // ����������� �������� � ���������� ��������� � ����
            float normalizedSpeed = Mathf.Clamp01(speed / _maxVelocity);
            _audioSource.volume = Mathf.Lerp(0, _maxVolume, normalizedSpeed);
            _audioSource.pitch = Mathf.Lerp(_minPitch, _maxPitch, normalizedSpeed);
        }
        else
        {
            StopSound();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        StopSound();
    }

    public void ResumeGame()
    {
        isPaused = false;
    }

    private void StopSound()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

    private void StopingMove()
    {
        _moveSpeed = 0;
        _rb.velocity = Vector3.zero; // ������������� ��������
        _rb.angularVelocity = Vector3.zero; // ������������� ��������
    }

    private void TakeControl()
    {
        _moveSpeed = 0;
    }
}
