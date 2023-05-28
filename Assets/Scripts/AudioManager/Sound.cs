using UnityEngine.Audio;
using UnityEngine;

  [System.Serializable]
public class Sound 
{
    public string name; //this sound's name
    public AudioClip clip;

    [HideInInspector]
    public AudioSource source;

    public bool bLoop = false;
    [Range(0f, 1f)]
    public float volume = 1; // this sound's volume

}
