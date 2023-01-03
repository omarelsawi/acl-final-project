using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip[] musicClips;
    public AudioClip[] sfxClips;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(int clip)
    {
        musicSource.clip = musicClips[clip];
        musicSource.Play();
    }

    public void PlaySFX(int clip)
    {
        sfxSource.PlayOneShot(sfxClips[clip]);
    }

    public void StopBackgroundMusic()
    {
        // Stop the background music
        musicSource.Stop();
    }

    public void StopSFX()
    {
        // Stop the background music
        sfxSource.Stop();
    }

}
