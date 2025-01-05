using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; // Привязать Audio Mixer
    [SerializeField] private Slider volumeSlider;   // Привязать слайдер громкости

    private const string VolumeParameter = "MasterVolume"; // Имя параметра громкости в Audio Mixer
    private const string VolumePrefsKey = "MasterVolume";  // Имя ключа для PlayerPrefs

    private void Start()
    {
        // Загружаем сохраненное значение громкости
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefsKey, 0.75f); // 0.75 - значение по умолчанию
        SetVolume(savedVolume);
        volumeSlider.value = savedVolume;

        // Подписываем слайдер на изменение громкости
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void OnDestroy()
    {
        // Отписываем слайдер от изменения громкости
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
