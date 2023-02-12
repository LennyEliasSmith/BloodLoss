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
        CHASE,
        ELEVATOR,
        KEYCARD
    }

    public ButtonState state;
    public Renderer screenRenderer;
    public Light screenLight;
    public GameObject[] affectedObjects;
    public GameObject[] endPoint;
    public bool hasBeenPressed = false;
    public float doorTime;
    public float doorSpeed;

    public int buttonsPressed;
    public int buttonTreshold;
    private List<Vector3> initialPositions = new List<Vector3>();
    private List<Quaternion> initialRotations = new List<Quaternion>();
    public bool hasKeyCard = false;
    public GameObject keyCard;

    // Start is called before the first frame update

    private void Awake()
    {
        Reset.CallReset += this.ResetValues;
    }
    void Start()
    {

        hasBeenPressed = false;
        int i = 0;
        foreach (var item in affectedObjects)
        {
            if (affectedObjects[i] != null)
            {
                initialPositions.Add(affectedObjects[i].transform.position);
                initialRotations.Add(affectedObjects[i].transform.rotation);
                i++;
            }
           
        }
    }

    public void Open()
    {
        if(state != ButtonState.KEYCARD)
        {
            StartCoroutine(OpenDoor());
        }
        else
        {
            StartCoroutine(OpenSwivel());
        }
       
    }

    public void ResetValues()
    {
        LoopThroughObjects();
        if(keyCard != null)
        {
            hasKeyCard = false;
            screenLight.color = Color.red;
            keyCard.SetActive(true);
        }
        screenLight.color = Color.yellow;
        screenRenderer.materials[2].SetColor("_EmissionColor", Color.yellow);
    }


    public void LoopThroughObjects()
    {
        int i = 0;
        foreach (var item in affectedObjects)
        {
            hasBeenPressed = false;
            if (affectedObjects[i] != null)
            {
                affectedObjects[i].transform.position = initialPositions[i];
                affectedObjects[i].transform.rotation = initialRotations[i];
               i++;
            }
        }
    }


    IEnumerator OpenDoor()
    {
        hasBeenPressed = true;
        float timer = 0;
        int i = 0;
        foreach (var item in affectedObjects)
        {

            while (timer < doorTime)
            {
                item.transform.position = Vector3.Lerp(item.transform.position, endPoint[i].transform.position, doorSpeed * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }
            timer = 0;
            i++;
        }
    }

    IEnumerator OpenSwivel()
    {
        Debug.Log("Swiveling");
        hasBeenPressed = true;
        float timer = 0;
        int i = 0;
        foreach (var item in affectedObjects)
        {

            while (timer < doorTime)
            {
                item.transform.rotation = Quaternion.Lerp(item.transform.rotation, endPoint[i].transform.rotation, doorSpeed * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }
            timer = 0;
            i++;
        }
    }

    public void TakeCard()
    {
        keyCard.SetActive(false);
        hasKeyCard = true;
    }
}
