using Main.Game;
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
    public float howfast;
    public Vector3 playerVelocity;
    private Vector3 previosPosition;

    private Rigidbody rb;
    private Camera cam;

    void Start()
    {
        rb = playerData.rb;
        cam = playerData.playerCamera;

    }

    void FixedUpdate()
    {
        playerVelocity = (transform.position - previosPosition) / Time.deltaTime;
        howfast = playerVelocity.magnitude;
        Mover();
        previosPosition = transform.position;
    }


    void Mover()
    {
        //Setting axis
        float horizontal = Input.GetAxisRaw(GameConstants.HorizontalAxis);
        float vertical = Input.GetAxisRaw(GameConstants.VerticalAxis);


        // Creating movement vector and normalizing it a.k.a making things smooth
        Vector3 movementVector = new Vector3(horizontal, 0, vertical);
        movementVector = movementVector.normalized * speed * Time.fixedDeltaTime;

        Vector3 direction = cam.transform.rotation * movementVector;

        //Moving the rigidbody
        rb.MovePosition(rb.position + new Vector3(direction.x, 0, direction.z));
    }
}
