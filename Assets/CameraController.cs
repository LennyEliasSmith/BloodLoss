using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerData playerData;
    public Camera cam;

    public float camSensitivity;
    public float yRotationLimit;

    Vector2 camRotation = Vector2.zero;
    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        camRotation.x += Input.GetAxisRaw(xAxis) * camSensitivity;
        camRotation.y += Input.GetAxisRaw(yAxis) * camSensitivity;
        camRotation.y = Mathf.Clamp(camRotation.y, -yRotationLimit, yRotationLimit);

        var xQuaterinion = Quaternion.AngleAxis(camRotation.x, Vector3.up);
        var yQuaternion = Quaternion.AngleAxis(camRotation.y, Vector3.left);

        transform.localRotation = xQuaterinion * yQuaternion;
        
    
    }
}
