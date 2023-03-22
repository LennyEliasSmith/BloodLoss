using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Game;

public class Pitfall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == GameConstants.PlayerTag)
        {
            Blood blood = other.GetComponent<Blood>();

            blood.currentBlood = 0;
        }
    }
}

