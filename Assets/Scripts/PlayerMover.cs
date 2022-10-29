using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public PlayerData playerData;

    [Range(0,100)]public float speed;
    public float rotationSpeed;
    public float innertia;
    public float mass;

    private Rigidbody rb;
    private Camera cam;

    void Start()
    {
        rb = playerData.rb;
        cam = playerData.playerCamera;

    }

    void FixedUpdate()
    {
        //Setting axis
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        // Creating movement vector and normalizing it a.k.a making things smooth
        Vector3 movementVector = new Vector3(horizontal,0,vertical);
        movementVector = movementVector.normalized * speed * Time.fixedDeltaTime;

        Vector3 direction = cam.transform.rotation * movementVector;

        //Moving the rigidbody
        rb.MovePosition(rb.position + new Vector3(direction.x,0, direction.z));
       
    }
}
