using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Game;

public class EndTextTrigger : MonoBehaviour
{
    public UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConstants.PlayerTag &&  !uiManager.isEnding)
        {
            uiManager.StartEndText();
        }
    }
}
