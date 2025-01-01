using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;
    private AudioSource _audioSource;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip newClip)
    {
        if (_audioSource.clip != newClip) // Проверяем, играет ли уже нужный трек
        {
            _audioSource.clip = newClip;
            _audioSource.Play();
        }
    }
}
