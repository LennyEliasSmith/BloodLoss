using Main.Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Main.Game
{
    public class Raycaster : MonoBehaviour
    {
        public GameManager gameManager;
        public FinalChaseSequence FinalChaseSequence;
        public Blood playerBlood;

        public Light flashLight;
        public float maxDistance;
        public float intensity;
        public float intensitySpeed;
        public float closeIntensity;
        public float initialIntensity;
        public float currentIntensity;
   
        public TextMeshProUGUI uiBottomText;
        private void Start()
        {
            initialIntensity = flashLight.intensity;
            uiBottomText = gameManager.uIManager.bottomText;
        }
        void FixedUpdate()
        {
            Vector3 forwardRay = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, forwardRay, out hit))
            {
                Debug.DrawLine(transform.position, hit.point);

                if (hit.collider.tag == GameConstants.ScareTag)
                {
                    Scare scare = hit.transform.gameObject.GetComponent<Scare>();

                    if (!scare.hasActivated)
                    {
                        switch (scare.scareEnum)
                        {
                            case Scare.scareCase.SCAREBLIP:
                                StartCoroutine(scare.spotlightScare);
                                break;
                            case Scare.scareCase.SCAREWALK:
                                StartCoroutine(scare.walkScare);
                                break;
                        }

                    }
                }

                if(hit.collider.tag == "Terminal" && hit.distance <= 3f)
                {
                    ButtonBehaviour buttonBehaviour = hit.transform.gameObject.GetComponent<ButtonBehaviour>();

                    if (!buttonBehaviour.hasBeenPressed)
                    {

                        uiBottomText.text = "Press " + GameConstants.interactionInput;
                    }

                    if (!buttonBehaviour.hasBeenPressed && Input.GetKey(KeyCode.E))
                    {
                 
                        switch (buttonBehaviour.state)
                            {
                                case ButtonBehaviour.ButtonState.DOOR:
                                    buttonBehaviour.screenRenderer.materials[2].SetColor("_EmissionColor", Color.green);
                                    buttonBehaviour.screenLight.color = Color.green;
                                    StartCoroutine(buttonBehaviour.openCouroutine);
                                    break;

                                case ButtonBehaviour.ButtonState.SEQUENCE:
                                    Debug.Log("Button Pressed");
                                    if (!buttonBehaviour.hasBeenPressed)
                                    {
                                        buttonBehaviour.hasBeenPressed = true;
                                        First_ChaseSequence.s_ButtonsPressed += 1;
                                        buttonBehaviour.screenRenderer.materials[2].SetColor("_EmissionColor", Color.green);
                                        buttonBehaviour.screenLight.color = Color.green;
                                    }
                                    if (First_ChaseSequence.s_ButtonsPressed == First_ChaseSequence.s_buttonPressedThreshold)
                                    {
                                        StartCoroutine(buttonBehaviour.openCouroutine);
                                    }
                                    break;
                                case ButtonBehaviour.ButtonState.CHASE:
                                    if (!FinalChaseSequence.isDoorOpening)
                                    {
                                        StartCoroutine(FinalChaseSequence.finalHunt);
                                    }
                                    break;
                            case ButtonBehaviour.ButtonState.ELEVATOR:
                                FinalChaseSequence.MoveElevator();
                                break;

                            }
                  
                      
                        
                    }
                }
                else
                {
                    uiBottomText.text = GameConstants.empty;
                }

                if(hit.collider.tag == "Blood" && hit.distance <= 3f)
                {
                

                    BloodPack bloodPack = hit.transform.gameObject.GetComponent<BloodPack>();
                    if (!bloodPack.hasBeenPickedUp)
                    {

                        uiBottomText.text = "Press " + GameConstants.interactionInput;
                    }

                    if(!bloodPack.hasBeenPickedUp && Input.GetKey(KeyCode.E))
                    {
                        bloodPack.OnPickedUp(playerBlood);
                    }

                }

                //Flashlight intensity
                if(hit.distance > maxDistance)
                {
                    intensity = initialIntensity;

                }
                else
                {
                    intensity = closeIntensity;
                }

                currentIntensity = flashLight.intensity;
                flashLight.intensity = Mathf.Lerp(flashLight.intensity, intensity, intensitySpeed * Time.fixedDeltaTime);
            }
        }
    }
}

