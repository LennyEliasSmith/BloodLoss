using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public enum ButtonState
    {
        DOOR,
        SEQUENCE,
        CHASE
    }

    public ButtonState state;
    public Renderer screenRenderer;
    public Light screenLight;
    public GameObject[] affectedObjects;
    public GameObject[] endPoint;
    public bool hasBeenPressed = false;
    public float doorTime;

    public int buttonsPressed;
    public int buttonTreshold;
    private List<Vector3> initialPositions = new List<Vector3>();

    public IEnumerator openCouroutine;

    // Start is called before the first frame update
    void Start()
    {
        hasBeenPressed = false;
        openCouroutine = Open();
        Reset.CallReset += ResetDoor;

        
        int i = 0;
        foreach (var item in affectedObjects)
        {
            if (affectedObjects[i] != null)
            {
                initialPositions.Add(affectedObjects[i].transform.position);
                Debug.Log(initialPositions[i] + ", "+ affectedObjects[i].transform.position);
                i++;
            }
           
        }
    }

    IEnumerator Open()
    {
        hasBeenPressed = true;
        float timer = 0;
        int i = 0;
        foreach (var item in affectedObjects)
        {

            while (timer < doorTime)
            {

                item.transform.position = Vector3.Lerp(item.transform.position, endPoint[i].transform.position, timer);
                timer += Time.deltaTime;
                yield return null;
            }
            timer = 0;
            i++;
        }
      
        yield break;
    }

    public void ResetDoor()
    {
        switch (state)
        {
            case ButtonState.DOOR:
                hasBeenPressed = false;
                LoopThroughObjects();
                break;
            case ButtonState.CHASE:
                hasBeenPressed = false;
                LoopThroughObjects();
                break;
            case ButtonState.SEQUENCE:
                hasBeenPressed = false;
                LoopThroughObjects();
                break;
        }
    }


    public void LoopThroughObjects()
    {
        int i = 0;
        foreach (var item in affectedObjects)
        {
            if (affectedObjects[i] != null)
            {
               affectedObjects[i].transform.position = initialPositions[i];
                i++;
            }
        }
    }
}
