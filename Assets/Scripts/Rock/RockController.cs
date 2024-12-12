using UnityEngine;

public class RockController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // �������� �������� ����
    private Rigidbody rb;

    private void Start()
    {
        // �������� ��������� Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // �������� ���� �� ����
        float moveHorizontal = Input.GetAxis("Horizontal"); // ���������� A/D ��� ��������� �����/������
        float moveVertical = Input.GetAxis("Vertical");     // ���������� W/S ��� ��������� �����/����

        // ������ ������ ��������
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // ��������� ���� � Rigidbody
        rb.AddForce(movement * moveSpeed);
    }
}
