using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [SerializeField] private AudioClip levelMusic; // Трек для текущего уровня

    private void Start()
    {
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.PlayMusic(levelMusic);
        }
    }
}
