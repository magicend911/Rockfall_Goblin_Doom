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
        // Устанавливаем минимальное значение громкости
        float adjustedVolume = Mathf.Clamp(volume, 0.0001f, 1f);

        // Преобразуем громкость в логарифмическую шкалу
        audioMixer.SetFloat(VolumeParameter, Mathf.Log10(adjustedVolume) * 20);

        // Сохраняем громкость в PlayerPrefs
        PlayerPrefs.SetFloat(VolumePrefsKey, volume);
        PlayerPrefs.Save();
    }
}
