using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;

    public AudioClip musicClip;
    public AudioClip coinClip;
    public AudioClip winclip;
    public AudioClip jumpClip;
    public AudioClip shootClip;

    private const string PREF_MUSIC = "music_volume";
    private const string PREF_SFX = "sfx_volume";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Load saved volumes (default = 1.0f)
            float musicVol = PlayerPrefs.HasKey(PREF_MUSIC) ? PlayerPrefs.GetFloat(PREF_MUSIC) : 1f;
            float sfxVol = PlayerPrefs.HasKey(PREF_SFX) ? PlayerPrefs.GetFloat(PREF_SFX) : 1f;

            SetMusicVolume(musicVol);
            SetSFXVolume(sfxVol);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (musicAudioSource != null && musicClip != null)
        {
            musicAudioSource.clip = musicClip;
            musicAudioSource.loop = true;
            if (!musicAudioSource.isPlaying)
                musicAudioSource.Play();
        }
    }

    public void SetMusicVolume(float value)
    {
        // value expected 0..1
        if (musicAudioSource != null)
            musicAudioSource.volume = Mathf.Clamp01(value);

        PlayerPrefs.SetFloat(PREF_MUSIC, Mathf.Clamp01(value));
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        // value expected 0..1
        if (vfxAudioSource != null)
            vfxAudioSource.volume = Mathf.Clamp01(value);

        PlayerPrefs.SetFloat(PREF_SFX, Mathf.Clamp01(value));
        PlayerPrefs.Save();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        if (vfxAudioSource != null && sfxClip != null)
        {
            vfxAudioSource.PlayOneShot(sfxClip, vfxAudioSource.volume);
        }
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += StopMusic;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= StopMusic;
    }

    private void StopMusic()
    {
        if (musicAudioSource != null && musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
            Debug.Log("Music stopped because player died.");
        }
    }
}
