using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private const float pauseMenuPitch = 0.5f;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Loop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s.source.loop)
        {
            s.source.loop = false;
        }
        else
        {
            if (!s.source.isPlaying)
            {
                s.source.Play();
            }
            s.source.loop = true;
        }
        
    }
}
