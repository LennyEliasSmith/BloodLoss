using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Game;

public class Reparent : MonoBehaviour
{

    public GameObject parent;
    public GameObject player;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == GameConstants.PlayerTag)
        {
            player.transform.SetParent(parent.transform,true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == GameConstants.PlayerTag)
        {
            player.transform.SetParent(null, true);
        }
    }
}
