using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SoundEffect
{
    TitleSound,//
    Button, //
    Slied, //
    Count,//
    Start,//
    Success,//
    Fail,//
    Clear,//
    Menu, //
    Star,//
}

public class SoundManager : Singleton<SoundManager>
{
    private Dictionary<SoundEffect, AudioClip> Sounds = new Dictionary<SoundEffect, AudioClip>();
    public AudioClip[] soundClips;
    public AudioClip[] Bgms;

    [SerializeField] public AudioSource BGM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<SoundManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        foreach(AudioClip audioClip in soundClips)
        {
            int index = System.Array.IndexOf(soundClips, audioClip);
            Sounds.Add((SoundEffect)index, audioClip);
        }
    }

    public void PlaySound(SoundEffect sound, float volume = 1f)
    {
        AudioSource audioSource = new GameObject("sound").AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.playOnAwake = false;
        audioSource.clip = Sounds[sound];

        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void PlayBGM(float volum = 1f)
    {
        BGM.volume = volum;
        if(SceneManager.GetActiveScene().name == "Title")
        {
            BGM.clip = Bgms[0];
            BGM.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Ingame")
        {
            BGM.clip = Bgms[1];
            BGM.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Stage")
        {
            BGM.clip = Bgms[2];
            BGM.Play();
        }
    }
}
