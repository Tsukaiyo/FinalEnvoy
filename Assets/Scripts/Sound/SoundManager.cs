using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

// This entire script is pretty much copied from this video lol
// Update: nevermind I changed a good amount but most of the core principles are from that video
// https://youtu.be/g5WT91Sn3hg?si=pKatMBVM9lZKzb0X

public enum SoundType
{
    HITS,
    DOORS,
    MOB,
    ENVIRONMENTAL,
    FIRE
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    // soundList is an array of SoundList, where SoundList represents one of the above enums and contains
    // an array of sounds
    [SerializeField] private SoundList[] soundList; 
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;   
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays a random sound from a given sound type in the sound list.
    /// </summary>
    /// <param name="sound">The enum for the sound type that you want to pull from.</param>
    /// <param name="volume">Volume for playback.</param>
    public static void PlayRandomSoundFrom(SoundType sound, float volume = 1)
    {
        List<SoundEntry> soundEntries = instance.soundList[(int)sound].Sounds;
        if (soundEntries.Count != 0)
        {
            AudioClip clip = soundEntries[UnityEngine.Random.Range(0, soundEntries.Count)].Clip;
            instance.audioSource.PlayOneShot(clip, volume);
        } else Debug.Log("Category "+sound.ToString()+" has no sounds, random selection failed.");
    }

    /// <summary>
    /// Plays a random sound from a given sound type that also is one of the given strings in the array passed in.
    /// </summary>
    /// <param name="soundType">The enum for the sound type that you want to pull from.</param>
    /// <param name="sounds">String array denoting which sounds you want to randomly choose from from the sound type.</param>
    /// <param name="volume">Volume for playback.</param>
    public static void PlayRandomSoundFrom(SoundType soundType, string[] sounds, float volume = 1)
    {
        List<SoundEntry> soundEntries = instance.soundList[(int)soundType].Sounds;
        List<AudioClip> soundList = new List<AudioClip>();
        
        foreach (SoundEntry entry in soundEntries)
        {
            if (sounds.Contains(entry.Name))
            {
                soundList.Add(entry.Clip);
            }
        }

        if (soundList.Count != 0) instance.audioSource.PlayOneShot(soundList[UnityEngine.Random.Range(0, soundList.Count)], volume);
        else Debug.Log("Random sound from array did NOT play");
    }

    /// <summary>
    /// Plays a sound given a type and name.
    /// </summary>
    /// <param name="soundType">The enum for the sound type that you want to pull from.</param>
    /// <param name="sound">String representing a name corresponding to the sound.</param>
    /// <param name="volume">Volume for playback.</param>
    public static void PlaySound(SoundType soundType, string sound, float volume = 1)
    {
        AudioClip clip = null;
        List<SoundEntry> list = instance.soundList[(int)soundType].Sounds;
        foreach (SoundEntry entry in list)
        {
            if (entry.Name == sound)
            {
                clip = entry.Clip;
                break;
            }
        }
        if (clip != null) instance.audioSource.PlayOneShot(clip, volume);
        else Debug.Log("Uh oh sound "+sound+" did not play");
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < names.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif
}

[Serializable]
public struct SoundList
{
    public List<SoundEntry> Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private List<SoundEntry> sounds;
}

[Serializable]
public struct SoundEntry
{
    public string Name { get => name; }
    public AudioClip Clip { get => clip; }
    [SerializeField] private string name;
    [SerializeField] private AudioClip clip;
}