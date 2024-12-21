using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lomai : MonoBehaviour
{
    public GameObject newHouse;
    public GameObject oldHouse;
    public Rigidbody[] oldHouseRigidbodies;

    public float disableCollisionTime = 2f;
    public float explosionForce = 1000f;
    public float explosionRadius = 5f;

    private void Start() 
    {
        newHouse.SetActive(true);
        oldHouse.SetActive(false);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            newHouse.SetActive(false);
            oldHouse.SetActive(true);
            StartCoroutine(ManageCollisionsAndGravity());
        }
    }

    private IEnumerator ManageCollisionsAndGravity()
    {
        Collider oldHouseCollider = oldHouse.GetComponent<Collider>();
        oldHouseCollider.enabled = false;

        
        yield return new WaitForSeconds(disableCollisionTime);
        
        oldHouseCollider.enabled = true;

        
        foreach (Rigidbody rb in oldHouseRigidbodies)
        {
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(explosionForce, oldHouse.transform.position, explosionRadius);
            }
        }

        yield return new WaitForSeconds(disableCollisionTime);
        
        
        foreach (Rigidbody rb in oldHouseRigidbodies)
        {
            if (rb != null)
            {
                rb.useGravity = true;
            }
        }
    }
}
