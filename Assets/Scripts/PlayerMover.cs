using Main.Game;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Main.Game
{
    public class PlayerMover : MonoBehaviour
    {
        public PlayerData playerData;
        public GameObject playerObj;
        public Animator animator;

        [Range(0, 100)] public float speed;
        public float runSpeed;
        public float slowdownSpeed;
        public float rotationSpeed;
     
        public float mass;
        public float howfast;
        public Vector3 playerVelocity;
        private Vector3 previosPosition;
        public Vector3 offset;
        private Rigidbody rb;
        private Camera cam;
        public Transform cameraFollower;

        public float stepInterval;
        public float runStepInterval;
        public AudioSource audioSource;
        public AudioClip[] steps;

        float timer;
        int stepIndex;
        float initialSpeed;
        float desiredSpeed;
        float initialStepInterval;
        void Start()
        {
            rb = playerData.rb;
            cam = playerData.playerCamera;
            timer = stepInterval;
            initialSpeed = speed;
            initialStepInterval = stepInterval;
        }

        void FixedUpdate()
        {
            playerVelocity = (transform.position - previosPosition) / Time.deltaTime;
            howfast = playerVelocity.magnitude;

            if (Input.GetButton("Fire3"))
            {
                desiredSpeed = runSpeed;
                stepInterval = runStepInterval;
            }
            else
            {
                desiredSpeed = initialSpeed;
                stepInterval = initialStepInterval;
            }

            speed = Mathf.Lerp(speed, desiredSpeed, slowdownSpeed * Time.fixedDeltaTime);
            Mover();
            if (playerVelocity.magnitude > 0.1f)
            {
                animator.Play("Armature_Walk");


                if (timer <= 0)
                {
                    Step();
                    timer = stepInterval;
                }
                timer -= Time.fixedDeltaTime;
            }
            else
            {
                animator.Play("Armature_0Tpose");
            }
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

            var bodyRotation = cam.transform.rotation;
            bodyRotation.x = 0;
            bodyRotation.z = 0;
            
            playerObj.transform.rotation = bodyRotation;
            playerObj.transform.position = new Vector3(cameraFollower.position.x, playerObj.transform.position.y, cameraFollower.position.z);
            
            //Moving the rigidbody
            rb.MovePosition(rb.position + new Vector3(direction.x, 0, direction.z));

            
        }


        public void Step()
        {
            
            if(stepIndex == 0)
            {
                stepIndex = 1;
            }
            else
            {
                stepIndex = 0;
            }
            audioSource.PlayOneShot(steps[stepIndex]);
            
        }

    }

}
