using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;



public class AudioSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource source;
    public TextMeshProUGUI valueText;

    public void OnChangeSlider(float Value)
    {
        valueText.SetText($"{Value.ToString("N2")}");
        mixer.SetFloat("Volume", Mathf.Log10(Value) * 20);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
