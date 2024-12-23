using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; // ������ �� AudioMixer
    [SerializeField] private Slider volumeSlider;   // ������ �� ������� ���������

    private const string VolumeParameter = "MasterVolume"; // ��� ��������� � AudioMixer
    private const string VolumePrefsKey = "MasterVolume";  // ��� ����� ��� ���������� � PlayerPrefs

    private void Start()
    {
        // ������������� ��������� �� ����������
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefsKey, 0.75f); // 0.75 - �������� �� ���������
        SetVolume(savedVolume);
        volumeSlider.value = savedVolume;

        // ������������� �� ��������� ��������
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void OnDestroy()
    {
        // ������������ �� ��������� ��������
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // ������������� ����������� �������� ���������
        float adjustedVolume = Mathf.Clamp(volume, 0.0001f, 1f);

        // ����������� ��������� � ��������������� �����
        audioMixer.SetFloat(VolumeParameter, Mathf.Log10(adjustedVolume) * 20);

        // ��������� ��������� � PlayerPrefs
        PlayerPrefs.SetFloat(VolumePrefsKey, volume);
        PlayerPrefs.Save();
    }
}
