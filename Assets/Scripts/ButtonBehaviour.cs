using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public enum ButtonState
    {
        DOOR,
        SEQUENCE
    }

    public ButtonState state;
    public Renderer screenRenderer;
    public Light screenLight;
    public GameObject[] affectedObjects;
    public GameObject[] endPoint;
    [HideInInspector] public Transform initialPositions;
    public bool hasBeenPressed = false;
    public float doorTime;

    public int buttonsPressed;
    public int buttonTreshold;

    public IEnumerator openCouroutine;

    // Start is called before the first frame update
    void Start()
    {
        hasBeenPressed = false;
        openCouroutine = Open();
    }

    // Update is called once per frame


    //private void OnTriggerStay(Collider other)
    //{
    //    if (Input.GetKeyDown(KeyCode.E) && !hasMoved)
    //    {
    //        Debug.Log("PressedButton");
    //        StartCoroutine("Open");
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (Input.GetKeyDown(KeyCode.E) && !hasMoved)
    //    {
    //        Debug.Log("PressedButton");
    //        StartCoroutine("Open");
    //    }
    //}

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
        
    }
}
