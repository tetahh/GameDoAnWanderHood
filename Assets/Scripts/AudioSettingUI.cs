using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Nếu không gán qua Inspector, có thể tìm tự động (không bắt buộc)
        // musicSlider = musicSlider ?? GameObject.Find("MusicSlider")?.GetComponent<Slider>();
        // sfxSlider   = sfxSlider   ?? GameObject.Find("SfxSlider")?.GetComponent<Slider>();

        // Initialize slider values from AudioManager (nếu đã có instance)
        if (AudioManager.Instance != null)
        {
            if (musicSlider != null)
            {
                musicSlider.value = AudioManager.Instance.musicAudioSource != null ? AudioManager.Instance.musicAudioSource.volume : 1f;
                musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
            }

            if (sfxSlider != null)
            {
                sfxSlider.value = AudioManager.Instance.vfxAudioSource != null ? AudioManager.Instance.vfxAudioSource.volume : 1f;
                sfxSlider.onValueChanged.AddListener(OnSfxSliderChanged);
            }
        }
    }

    private void OnMusicSliderChanged(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMusicVolume(value);
    }

    private void OnSfxSliderChanged(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetSFXVolume(value);
    }

    private void OnDestroy()
    {
        if (musicSlider != null)
            musicSlider.onValueChanged.RemoveListener(OnMusicSliderChanged);
        if (sfxSlider != null)
            sfxSlider.onValueChanged.RemoveListener(OnSfxSliderChanged);
    }
}
