using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; // Ссылка на AudioMixer
    [SerializeField] private Slider volumeSlider;   // Ссылка на слайдер громкости

    private const string VolumeParameter = "MasterVolume"; // Имя параметра в AudioMixer
    private const string VolumePrefsKey = "MasterVolume";  // Имя ключа для сохранения в PlayerPrefs

    private void Start()
    {
        // Устанавливаем громкость из сохранений
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefsKey, 0.75f); // 0.75 - значение по умолчанию
        SetVolume(savedVolume);
        volumeSlider.value = savedVolume;

        // Подписываемся на изменения слайдера
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void OnDestroy()
    {
        // Отписываемся от изменений слайдера
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(VolumeParameter, Mathf.Log10(volume) * 20); // Логарифмическая шкала
        PlayerPrefs.SetFloat(VolumePrefsKey, volume); // Сохраняем громкость
        PlayerPrefs.Save();
    }
}
