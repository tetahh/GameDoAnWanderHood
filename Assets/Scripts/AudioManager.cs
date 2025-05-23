using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //biến lưu trữ audio source

    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;

    //biến lưu trữ audio clip (file audio)
    public AudioClip musicClip;
    public AudioClip coinClip;
    public AudioClip winclip;
    public AudioClip jumpClip;
    void Start()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
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
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
            Debug.Log("Music stopped because player died.");
        }
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        vfxAudioSource.clip = sfxClip;
        vfxAudioSource.PlayOneShot(sfxClip);
    }
    // Update is called once per frame
    
}
