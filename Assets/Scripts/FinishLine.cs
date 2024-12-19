using UnityEngine;
using UnityEngine.Events;

public class FinishLine : MonoBehaviour
{
    public event UnityAction RockFinished;
    private void OnTriggerEnter(Collider other)
    {
        Rock rock = other.GetComponent<Rock>();
        if (rock != null)
        {
            RockFinished?.Invoke();
        }
    }
}
