using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Game;
public class EnemyStopper : MonoBehaviour
{
    public FinalChaseSequence chaseSequenceManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == GameConstants.PlayerTag)
        {
            chaseSequenceManager.ResolveHunt();
        }
    }

}
