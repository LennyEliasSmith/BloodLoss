using Main.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First_ChaseSequence : MonoBehaviour
{
    public GameObject Door1;
    public GameObject Door2;

    public Transform door1Destination;
    public Transform door2Destination;

    public float doorSpeed;


    public Light[] roomLights;
    public MeshRenderer[] roomLightMaterials;

    public EnemySeeker enemySeeker;

    private Vector3 door1InitialPosition;
    private Vector3 door2InitialPosition;

    public static int s_ButtonsPressed;
    public static int s_buttonPressedThreshold;
    public int buttonsPressedThreshold;
    // Start is called before the first frame update
    void Start()
    {
        s_buttonPressedThreshold = buttonsPressedThreshold;
        door1InitialPosition = Door1.transform.position;
        door2InitialPosition = Door2.transform.position;
        ResetRoom();
    }



    public void ResetRoom()
    {
        Door1.transform.position = door1InitialPosition;
        Door2.transform.position = door2InitialPosition;
    }

    public void InitRoom1()
    {

        enemySeeker.isHunting = true;
        float elapsedTime = 0;


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
        enemySeeker.isHunting = false;
        enemySeeker.agent.isStopped = true;
        float elapsedTime = 0;

       
        while (elapsedTime < doorSpeed)
        {
            Door2.transform.position = Vector3.Lerp(Door2.transform.position, door2Destination.position, elapsedTime / doorSpeed);
            elapsedTime += Time.deltaTime;
        }
    }

    public void ResolveRoom()
    {

    }

    public void ResetLights()
    {
        foreach (var roomlight in roomLights)
        {
            roomlight.colorTemperature = 8000;
        }
    }
}
