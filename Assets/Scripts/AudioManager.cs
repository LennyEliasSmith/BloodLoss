using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Main.Game;

public class AudioManager : MonoBehaviour
{

    public  AudioMixer audioMixer;
    public AudioSource audioSource;
    public AudioSource heartBeatAudioSource;
    public AudioClip _whiteNoise;
    public AudioClip _ambienceTrack;
    public AudioClip _chaseSequenceTrack;
    public AudioClip _doorOpen;
    public AudioClip _pickup;
    public AudioClip _scare;
    public AudioClip _terminalButton;
    public AudioClip _heartbeat;
    public AudioClip _stinger;

    public void SetTrack(AudioClip desiredTrack)
    {
        audioSource.Stop();
        audioSource.clip = desiredTrack;
        audioSource.Play();
    }
}
