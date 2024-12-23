using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    [SerializeField] private float destroyDelay = 2f;

    private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        Rock rockScore = collision.GetComponent<Rock>();

        if (rockScore != null && !isDead)
        {
            Die(); 
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        
        Destroy(gameObject, destroyDelay);
    }
}
