using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceBehaviour : MonoBehaviour
{
    public First_ChaseSequence sequence;
    public bool isExitTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isExitTrigger )
        {
            sequence.InitRoom1();
        }
        else if (other.tag == "Player" && isExitTrigger)
        {
            sequence.CloseExit();
        }
    }
}
