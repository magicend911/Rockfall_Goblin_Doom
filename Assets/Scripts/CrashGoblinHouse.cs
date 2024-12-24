using UnityEngine;

public class CrashGoblinHouse : MonoBehaviour
{
    public class Lomai : MonoBehaviour
    {
        [SerializeField] private GameObject newHouse;
        [SerializeField] private GameObject oldHouse;

        private void Start()
        {
            newHouse.SetActive(true);
            oldHouse.SetActive(false);
        }

        private void OnTriggerEnter(Collider collision)
        {
            Rock rock = collision.GetComponent<Rock>();

            if (rock != null)
            {
                newHouse.SetActive(false);
                oldHouse.SetActive(true);
            }
        }
    }
}
