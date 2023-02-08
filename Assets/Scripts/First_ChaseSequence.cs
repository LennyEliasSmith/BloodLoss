using Main.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First_ChaseSequence : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject Door1;
    public GameObject Door2;

    public Transform door1Destination;
    public Transform door2Destination;
    public Transform chaseSequenceSpawn;

    public float doorSpeed;


    public Light[] roomLights;
    public MeshRenderer[] roomLightMaterials;

    public EnemySeeker enemySeeker;

    public bool room1Init;



    public static int s_ButtonsPressed;
    public static int s_buttonPressedThreshold;
    public int buttonsPressedThreshold;

    private Vector3 initialEnemyPosition;
    private Vector3 door1InitialPosition;
    private Vector3 door2InitialPosition;
    private float initialRoomLightTemperature;


    // Start is called before the first frame update



    void Start()
    {
        s_buttonPressedThreshold = buttonsPressedThreshold;
        door1InitialPosition = Door1.transform.position;
        door2InitialPosition = Door2.transform.position;
        initialEnemyPosition = enemySeeker.transform.position;
        initialRoomLightTemperature = roomLights[0].colorTemperature;
        Reset.CallReset += ResetValues;
    }



    public void ResetValues() 
    {
        enemySeeker.isHunting = false;
        //enemySeeker.transform.localPosition = chaseSequenceSpawn.localPosition;
        enemySeeker.agent.isStopped = true;
        enemySeeker.agent.Warp(chaseSequenceSpawn.localPosition);
        enemySeeker.anim.Play(GameConstants.enemyGlitchAnim);
        room1Init = false;
        Door1.transform.position = door1InitialPosition;
        Door2.transform.position = door2InitialPosition;
        s_ButtonsPressed = 0;

        foreach (var roomlight in roomLights)
        {
            roomlight.colorTemperature = initialRoomLightTemperature;
        }

        foreach (var renderer in roomLightMaterials)
        {
            renderer.material.SetColor("_EmissionColor", Color.white);

        }
    }

    public void InitRoom1()
    {
        room1Init = true;
        enemySeeker.isHunting = true;
        enemySeeker.agent.isStopped = false;
        enemySeeker.player = GameObject.FindGameObjectWithTag(GameConstants.PlayerTag).transform;
        float elapsedTime = 0;

        if (audioManager.audioSource.clip != audioManager._chaseSequenceTrack)
        {
            audioManager.SetTrack(audioManager._chaseSequenceTrack);
        }
        

        foreach(var roomlight in roomLights)
        {
            roomlight.colorTemperature = 1500;
        }

        foreach (var renderer in roomLightMaterials)
        {
            renderer.material.SetColor("_EmissionColor", Color.red);

        }

        while (elapsedTime < doorSpeed)
        {
            Door1.transform.position = Vector3.Lerp(Door1.transform.position,door1Destination.position, elapsedTime / doorSpeed);
            Door2.transform.position = Vector3.Lerp(Door2.transform.position, door2Destination.position, elapsedTime / doorSpeed);
            elapsedTime += Time.deltaTime;
        }
    }

    public void CloseExit()
    {

        StartCoroutine(Close());

    }

    public void ResetLights()
    {
        foreach (var roomlight in roomLights)
        {
            roomlight.colorTemperature = 8000;
        }
    }


    IEnumerator Close()
    {
        audioManager.SetTrack(audioManager._ambienceTrack);
        enemySeeker.isHunting = false;
        enemySeeker.agent.isStopped = true;

        float elapsedTime = 0;
        while (elapsedTime < doorSpeed)
        {
            Door2.transform.position = Vector3.Lerp(Door2.transform.position, door2Destination.position, doorSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
