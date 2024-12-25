using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class RockController : MonoBehaviour
{
    [SerializeField] private Rock _rock;
    [SerializeField] private float moveSpeed = 5f; // �������� �������� ����
    [SerializeField] private AudioClip rollingSound; // ���� ���������� �����
    [SerializeField] private float minVelocityToPlaySound = 0.1f; // ����������� �������� ��� ��������������� �����
    [SerializeField] private float maxVelocity = 10f; // ��������, ��� ������� ���� ��������� ������������ ���������
    [SerializeField] private float maxVolume = 1f; // ������������ ��������� �����
    [SerializeField] private float minPitch = 0.8f; // ����������� ���� �����
    [SerializeField] private float maxPitch = 1.5f; // ������������ ���� �����

    private Rigidbody rb;
    private AudioSource _audioSource;
    private bool isPaused = false; // ���� ��� �������� ��������� �����

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
        _rock.StopMove += StopingMone;
    }

    private void OnDisable()
    {
        _rock.StopMove -= StopingMone;
    }

    private void FixedUpdate()
    {
        if (isPaused) return; // ���� ���� �� �����, ��������� ����������

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
        if (!isPaused)
        {
            HandleRollingSound(); // ������������ ���� ���������� �����
        }
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
            if (_audioSource.isPlaying)
            {
                _audioSource.Pause();
            }
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

    private void StopingMone()
    {
        moveSpeed = 0;
    }
}
