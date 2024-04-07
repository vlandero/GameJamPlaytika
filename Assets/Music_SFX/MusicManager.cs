using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayMusic();
    }

    void PlayMusic()
    {
        // Play your music track here
        audioSource.Play();
    }

    public void ChangeMusic(AudioClip newMusic)
    {
        // Change the music track
        audioSource.Stop();
        audioSource.clip = newMusic;
        audioSource.Play();
    }
}
