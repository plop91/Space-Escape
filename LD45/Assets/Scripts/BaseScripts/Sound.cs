﻿using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public bool loop;
    public AudioClip clip;
    [Range(0f, 2f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;
}
