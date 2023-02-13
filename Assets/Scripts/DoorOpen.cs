using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.AI;

public class DoorOpen : MonoBehaviour
{
    public float doorSpeed;

    public float desiredRot;
    public Transform door;
    public Renderer doorRenderer;
    public float negativeTime;
    public NavMeshObstacle obstacle;
    private Quaternion initialRot;
    // Start is called before the first frame update

    private void Start()
    {
        obstacle.enabled = false;
        initialRot = door.transform.localRotation;
        Reset.CallReset += ResetValues;
    }
    public void OpenDoor()
    {
        StartCoroutine(Open());
    }

    void ResetValues()
    {
        //doorRenderer.material.SetFloat("_Fill", 1);
        door.transform.localRotation = initialRot;
        obstacle.enabled = false;
    }

    IEnumerator Open()
    {
        door.localRotation = new Quaternion(0, 0, desiredRot, 0);
        obstacle.enabled = true;
        //doorRenderer.material.SetFloat("_Fill", -doorSpeed * Time.deltaTime);
        Debug.Log("This door has opened");
        yield return null;
    }
}
