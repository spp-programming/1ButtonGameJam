using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    //persist between scenes
    //when loading into new scene, accept new scriptable object holding new scene's sounds
    //when exiting a scene, remove all sounds that came from the scene's scriptable object

    public Sound[] SoundEffects;
    public Sound[] Music;

    public AudioMixerGroup SFXMaster;
    public AudioMixerGroup MusicMaster;

    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        UpdateSounds();
    }

    private void Start()
    {
    }

    void UpdateSounds()
    {
        foreach (Sound s in SoundEffects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = SFXMaster;
        }

        foreach (Sound s in Music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = MusicMaster;
        }
    }

    public void PlaySFX(string name)
    {
        Sound soundEffect = Array.Find(SoundEffects, sound => sound.name == name);
        if (soundEffect == null)
        {
            Debug.LogWarning("Sound: " + name + " does not exist!");
            return;
        }
        soundEffect.source.Play();
    }

    public void PlayMusic(string name)
    {
        Sound music = Array.Find(Music, sound => sound.name == name);
        if (music == null)
        {
            Debug.LogWarning("Track: " + name + " does not exist!");
            return;
        }
        music.source.Play();
    }

    public void StopSFX()
    {
        foreach (Sound s in SoundEffects)
            if (s.source.isPlaying)
                s.source.Stop();
    }

    public void StopMusic(float duration = 1)
    {
        foreach (Sound s in Music)
        {
            if (s.source.isPlaying)
            {
                StartCoroutine(Diminuendo(s, duration / 8));
                Invoke(nameof(StopMusicCoroutine), duration);
            }
        }
    }

    public IEnumerator Diminuendo(Sound track, float duration)
    {
        for (float vol = track.source.volume; vol >= 0; vol -= 0.05f)
        {
            track.source.volume = vol;
            yield return new WaitForSecondsRealtime(duration);
        }
        track.source.Stop();
    }

    void StopMusicCoroutine() => StopCoroutine(nameof(Diminuendo));

    public bool IsSoundPlaying(string name, Sound[] array)
    {
        bool soundPlaying = false;
        foreach (Sound s in array)
        {
            if ((s.name == name) && s.source.isPlaying)
            {
                soundPlaying = true;
                Debug.Log("Detected Sound Playing...");
                break;
            }
        }
        return soundPlaying;
    }

    public Sound GetSound(string name, Sound[] array) => Array.Find(array, sound => sound.name == name);
}