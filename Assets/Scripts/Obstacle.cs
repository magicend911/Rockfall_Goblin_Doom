using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        Rock playerHealth = other.GetComponent<Rock>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
