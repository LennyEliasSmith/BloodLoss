using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.AI;

public class DoorOpen : MonoBehaviour
{
    public float doorSpeed;

    public float desiredRot;
    public GameObject door;
    public Renderer doorRenderer;
    public float negativeTime;
    public NavMeshObstacle obstacle;
    // Start is called before the first frame update

    private void Start()
    {
        obstacle.enabled = false;
    }
    public void OpenDoor()
    {
        float elapsedTime = 0;
        float emptyTime = negativeTime;

        while (elapsedTime < doorSpeed)
        {
            doorRenderer.material.SetFloat("_Fill", -doorSpeed * Time.deltaTime);
            var wantedFloat = Mathf.Lerp(0, desiredRot, doorSpeed * Time.deltaTime);
            door.transform.localRotation = new Quaternion(0, 0, wantedFloat, 0);
            elapsedTime += Time.deltaTime;
            emptyTime -= Time.deltaTime;
        }

        float obstacleWatitTime = 5f;

        while(obstacleWatitTime > 0)
        {
            obstacleWatitTime -= Time.deltaTime;
        }
        obstacle.enabled = true;
    }

    void ResetValues()
    {
        doorRenderer.material.SetFloat("_Fill", 1);
    }
}
