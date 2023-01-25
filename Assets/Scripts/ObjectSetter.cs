using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSetter : MonoBehaviour
{

    public GameObject locationIndicator;
    // Start is called before the first frame update
    void Start()
    {
        locationIndicator.SetActive(false);
    }


}
