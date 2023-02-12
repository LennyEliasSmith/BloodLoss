using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Game;
public class SequenceBehaviour : MonoBehaviour
{
    public First_ChaseSequence sequence;
    public RespawnController respawnController;
    public bool isExitTrigger;

    private bool hasPlayed = false;

    private void Start()
    {
        Reset.CallReset += ResetValues;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == GameConstants.PlayerTag && !isExitTrigger && !sequence.room1Init)
        {
            sequence.InitRoom1();
        }
        else if (other.tag == GameConstants.PlayerTag && isExitTrigger)
        {
            sequence.CloseExit();

            var audioManager = FindObjectOfType<AudioManager>();

            if (!hasPlayed)
            {
                audioManager.audioSource.PlayOneShot(audioManager._stinger);  
            }
            
            
            respawnController.currentRespawnLocation = 2;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == GameConstants.PlayerTag)
        {
            hasPlayed = true;
        }

    }

    void ResetValues()
    {
        hasPlayed = false;
    }
}
