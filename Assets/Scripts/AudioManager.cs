using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Main.Game;

public class AudioManager : MonoBehaviour
{

    public  AudioMixer audioMixer;
    public AudioSource audioSource;
    public AudioClip _whiteNoise;
    public AudioClip _ambienceTrack;
    public AudioClip _chaseSequenceTrack;
    public AudioClip doorOpen;

    public void SetTrack(AudioClip desiredTrack)
    {
        audioSource.Stop();
        audioSource.clip = desiredTrack;
        audioSource.Play();
    }
}
