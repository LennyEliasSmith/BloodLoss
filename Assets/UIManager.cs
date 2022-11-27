using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI bottomText;
    public GameObject startText;
    // Start is called before the first frame update
    void OnEnable()
    {
        bottomText.text = "";
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            startText.gameObject.SetActive(false);
        }
    }

}
