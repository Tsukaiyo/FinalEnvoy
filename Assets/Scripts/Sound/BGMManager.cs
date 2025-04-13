using System.Collections;
using UnityEngine;
using System;

public enum BGMTrack
{ 
    Track1,
    Track2,
    Track3
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class BGMManager : MonoBehaviour 
{
    [SerializeField] private AudioClip[] trackList;
    private static BGMManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayGameMusic(BGMTrack.Track1);
    }
    private void PlayGameMusic(BGMTrack track)
    {
        PlaySound(track);
        StartCoroutine(WaitForAudioToEnd(audioSource, () =>
        {
            PlayGameMusic((BGMTrack)(UnityEngine.Random.Range(0, trackList.Length)));
        }));
    }
    public static void PlaySound(BGMTrack track, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.trackList[(int)track], volume);
    }

    private IEnumerator WaitForAudioToEnd(AudioSource source, Action onComplete = null)
    {
        yield return new WaitWhile(() => source.isPlaying);
        onComplete?.Invoke();
    }

}
