using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public bool mute;

    public float volume;
    public Sound[] sounds;
    void Awake()
    {
        if (instance == null)
            instance = GameObject.Find("GameManager").GetComponent<AudioManager>();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = volume;
            s.source.pitch = s.pitch;
        }
    }

    public static void GeneralPlay(string name)
    {
        instance.Play(name);
    }

    public void Play(string name)
    {
        if (!mute)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s.loop)
            {
                s.source.loop = true; ;
            }
            s.source.Play();
        }
    }


    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }
    public void ToggleMute()
    {
        mute = !mute;
    }
}
