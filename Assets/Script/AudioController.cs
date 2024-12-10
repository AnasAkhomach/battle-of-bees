using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{

    public bool menu;
    public List<AudioSource> music;
    public List<AudioSource> sound;
    public float volumeSound { get; set; }
    public float volumeMusic { get; set; }

    void Awake()
    {
        isMusic = MyPref.getMusic();
        isSound = MyPref.getSound();
        volumeSound = MyPref.getVolumeSound();
        volumeMusic = MyPref.getVolumeMusic();
    }

    void Update()
    {
        if (isMusic)
        {
            if (!MyPref.getMusic())
            {
                MyPref.setMusic(isMusic);
                for (int i = 0; i < music.Count; i++)
                    StopMusic(i);
                if (!menu)
                    playMusic(Random.Range(0, 2));
                else
                    playMusic(0);
            }
        }
        else
        {
            if (MyPref.getMusic())
            {
                for (int i = 0; i < music.Count; i++)
                    StopMusic(i);
                MyPref.setMusic(isMusic);
            }
        }
        if (isSound)
        {
            if (!MyPref.getSound())
            {
                MyPref.setSound(isSound);
            }
        }
        else
        {
            if (MyPref.getSound())
            {
                MyPref.setSound(isSound);
            }
        }
        foreach (AudioSource ms in music)
        {
            ms.volume = volumeMusic;
        }
        foreach (AudioSource sd in sound)
        {
            sd.volume = volumeSound;
        }
    }

    public void playSound(int index)
    {
        if (MyPref.getSound())
            sound[index].Play();
    }

    public void playMusic(int index)
    {
        if (MyPref.getMusic())
            if (!music[index].isPlaying)
                music[index].Play();
    }

    public void StopMusic(int index)
    {
        if (MyPref.getMusic())
            if (music[index].isPlaying)
                music[index].Stop();
    }

    bool isMusic;
    public bool OnMusic
    {
        get { return isMusic; }
        set { isMusic = value; }
    }

    bool isSound;
    public bool OnSound
    {
        get { return isSound; }
        set { isSound = value; }
    }

    void OnDestroy()
    {
        MyPref.setVolumeSound(volumeSound);
        MyPref.setVolumeMusic(volumeMusic);
    }
}
