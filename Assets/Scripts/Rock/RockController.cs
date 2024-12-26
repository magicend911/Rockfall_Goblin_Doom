using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class RockController : MonoBehaviour
{
    [SerializeField] private Rock _rock;
    [SerializeField] private DeathZone _deathZone;
    [SerializeField] private float moveSpeed = 5f; // �������� �������� ����
    [SerializeField] private AudioClip rollingSound; // ���� ���������� �����
    [SerializeField] private float minVelocityToPlaySound = 0.1f; // ����������� �������� ��� ��������������� �����
    [SerializeField] private float maxVelocity = 10f; // ��������, ��� ������� ���� ��������� ������������ ���������
    [SerializeField] private float maxVolume = 1f; // ������������ ��������� �����
    [SerializeField] private float minPitch = 0.8f; // ����������� ���� �����
    [SerializeField] private float maxPitch = 1.5f; // ������������ ���� �����
    [SerializeField] private LayerMask groundLayer; // ���� ��� �������� �����
    [SerializeField] private float groundCheckDistance = 0.1f; // ���������� ��� �������� �����

    private Rigidbody rb;
    private AudioSource _audioSource;
    private bool isPaused = false; // ���� ��� �������� ��������� �����
    private bool isGrounded = false; // ���� ��� �������� ���������� �� �����

    private void Start()
    {
        // ������������� �����������
        rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        // ��������� ��������� �����
        _audioSource.clip = rollingSound;
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
        rb.AddForce(movement * moveSpeed);
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
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    private void HandleRollingSound()
    {
        float speed = rb.velocity.magnitude;

        if (speed > minVelocityToPlaySound)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }

            // ����������� �������� � ���������� ��������� � ����
            float normalizedSpeed = Mathf.Clamp01(speed / maxVelocity);
            _audioSource.volume = Mathf.Lerp(0, maxVolume, normalizedSpeed);
            _audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedSpeed);
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
        moveSpeed = 0;
        rb.velocity = Vector3.zero; // ������������� ��������
        rb.angularVelocity = Vector3.zero; // ������������� ��������
    }

    private void TakeControl()
    {
        moveSpeed = 0;
    }
}
