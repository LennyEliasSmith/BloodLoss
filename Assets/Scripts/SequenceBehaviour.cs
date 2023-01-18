using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Game;
public class SequenceBehaviour : MonoBehaviour
{
    public First_ChaseSequence sequence;
    public RespawnController respawnController;
    public bool isExitTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isExitTrigger && !sequence.room1Init)
        {
            sequence.InitRoom1();
        }
        else if (other.tag == "Player" && isExitTrigger)
        {
            sequence.CloseExit();
            respawnController.currentRespawnLocation = 2;
        }
    }
}
