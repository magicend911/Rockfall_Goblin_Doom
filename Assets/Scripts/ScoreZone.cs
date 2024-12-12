using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Rock rockScore = collision.GetComponent<Rock>();

        if (rockScore != null)
        {
            rockScore.AddScore();
            Destroy(gameObject); 
        }
    }
}
