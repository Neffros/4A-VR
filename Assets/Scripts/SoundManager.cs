using System;
using UnityEngine;



public class SoundManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.SoundManager = this;
        Init();
        Play("music");

    }

    //take game setting volume with gamesingleton.instance.gamesettings.volume 
    public Sound[] sounds;
    public void Init()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.Log("no source found");
            return;
        }

        //Debug.Log("try playing source:" + soundName);
        if (s.loop) //is a music
            s.source.volume = s.volume;
        else
            s.source.volume = s.volume;
        s.source.Play();
    }

    public void StopPlayingAllMusics()
    {
        foreach(var s in sounds)
        {
            if (s.loop)
            {
                s.source.Stop();
            }
        }
        //s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        //s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
    }

    public void StopPlayingAllAudioSources()
    {
        foreach (var s in sounds)
        {
            s.source.Stop();
        }
    }

    public void StopPlayingAllSounds()
    {
        foreach (var s in sounds)
        {
            if (!s.loop)
                StopPlayingSource(s);
        }
    }

    public void StopPlayingSource(Sound s)
    {
        s.source.Stop();
    }

    public void UpdateVolume() {
        for (int i = sounds.Length - 1; i >= 0; --i) {
            Sound s = sounds[i];
            if (s.loop) //is a music
                s.source.volume = s.volume;
            else
                s.source.volume = s.volume;
        }
    }

}
