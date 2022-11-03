using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public PlayerData playerData;
    public PlayerMover playerMover;

    public float maxBlood;
    [SerializeField]public float currentBlood;
    public Renderer bloodRenderer;
    Material bloodMaterial;

    public float lossTime;
    public float lossRate;
    public float lossAmount;
    public float lossModifier;
    public float lossLerpModifier;

    private float initialLossTime;
    void Start()
    {
        currentBlood = maxBlood;
        initialLossTime = lossTime;
        bloodMaterial = bloodRenderer.material;
        bloodMaterial.SetFloat("_Fill", 0.19f);

        Debug.Log(bloodRenderer.material.name);
    }

    private void Update()
    {
        LoseBlood(lossAmount, lossModifier);
    
    }

    public void LoseBlood( float amount, float modifier)
    {
        float initialBlood = currentBlood;
        float loss = currentBlood - amount * modifier;

        currentBlood = Mathf.Lerp(initialBlood, loss, lossRate * Time.deltaTime);
        bloodMaterial.SetFloat("_Fill", currentBlood);
        currentBlood = Mathf.Clamp(currentBlood, 0f, maxBlood);

        if (currentBlood <= 0)
        {
            CheckDeath();
        }

        //Cheat
        if(Input.GetKeyDown(KeyCode.G))
        {
            currentBlood += 0.5f;
        }
    }

    public void CheckDeath()
    {

    }

}
