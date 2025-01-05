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
            if (_audioSource == null)
            {
                Debug.LogError("AudioSource is missing on MusicManager!");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip newClip)
    {
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource is not initialized!");
            return;
        }

        if (_audioSource.clip != newClip)
        {
            _audioSource.clip = newClip;
            _audioSource.Play();
        }
    }
}
