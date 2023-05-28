using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //store all sounds in an array
    public Sound[] sounds;

    [Range(0f, 1f)]
    public float volume = 1;//global effects volume adjustable in the settings


    
    void Awake()
    {
        foreach (Sound s in sounds) //for each sound add audiosource and set the volume
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.bLoop;

            s.source.volume = s.volume* volume; 

        }

    }



    public void Play(string name) 
    {
      Sound s =  Array.Find(sounds, sound => sound.name == name); //find the sound in the array
        if(s == null) { Debug.Log("Sound" + s + "not found"); return; } // if not found debug message
        s.source.Play(); // if found play it
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);  //find the sound in the array
        if (s == null) { Debug.Log("Sound" + s + "not found"); return; } // if not found debug message
        s.source.Stop(); // if found stop playing it
    }

}
