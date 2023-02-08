using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Game;

public class SetRespawn : MonoBehaviour
{
    public RespawnController respawnController;
    public int respawnInt;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == GameConstants.PlayerTag)
        {
            respawnController.currentRespawnLocation = respawnInt;
        }
    }
}
