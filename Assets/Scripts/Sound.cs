using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public enum AudioTypes { soundEffect, music}
    public AudioTypes audioType;

    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume = 0.5f;
    [Range(.1f,3f)]
    public float pitch;
    [Range(0.0f, 1.0f)]
    public float spatialBlend = 0.0f;
    [HideInInspector]
    public AudioSource source;
    public bool loop;

}
