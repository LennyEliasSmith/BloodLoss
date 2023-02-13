using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Game;
public class SequenceBehaviour : MonoBehaviour
{
    public First_ChaseSequence sequence;
    public RespawnController respawnController;
    public bool isExitTrigger;

    private bool isPlaying = false;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == GameConstants.PlayerTag && !isExitTrigger && !sequence.room1Init)
        {
            sequence.InitRoom1();
        }
        else if (other.tag == GameConstants.PlayerTag && isExitTrigger && !isPlaying)
        {
            sequence.CloseExit();
    
            StartCoroutine(PlaySound());

            respawnController.currentRespawnLocation = 2;
        }
    }

    IEnumerator PlaySound()
    {
        isPlaying = true;
        audioManager.audioSource.PlayOneShot(audioManager._stinger);
        yield return new WaitForSeconds(5);
        isPlaying = false;
    }
}
