using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlaySoundExit : StateMachineBehaviour
{
    [SerializeField] private SoundType soundType;
    [SerializeField] private string sound;
    [SerializeField, Range(0,1)] private float volume = 1;
    [SerializeField] private bool random;
    [SerializeField] private string[] randomClips;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!random) SoundManager.PlaySound(soundType, sound, volume);
        else if (randomClips.Length > 0) SoundManager.PlayRandomSoundFrom(soundType, randomClips, volume);
        else SoundManager.PlayRandomSoundFrom(soundType, volume);
    }
}
