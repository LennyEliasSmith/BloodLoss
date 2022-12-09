using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class DoorOpen : MonoBehaviour
{
    public float doorSpeed;

    public float desiredRot;
    public GameObject door;
    public Renderer doorRenderer;
    public float negativeTime;
    // Start is called before the first frame update



    public void OpenDoor()
    {
        float elapsedTime = 0;
        float emptyTime = negativeTime;

        while (elapsedTime < doorSpeed)
        {
            doorRenderer.material.SetFloat("Fill", elapsedTime / doorSpeed);
            var wantedFloat = Mathf.Lerp(0, desiredRot, elapsedTime / doorSpeed);
            door.transform.localRotation = new Quaternion(0, 0, wantedFloat, 0);
            elapsedTime += Time.deltaTime;
            emptyTime -= Time.deltaTime;
        }
    }
}
