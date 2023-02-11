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
        public AudioManager audioManager;
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
                                audioManager.audioSource.PlayOneShot(audioManager._scare);
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

                    if (!buttonBehaviour.hasBeenPressed && buttonBehaviour.keyCard == null)
                    {
                        uiBottomText.text = "Press " + GameConstants.interactionInput;
                        
                    }
                    else if(!buttonBehaviour.hasBeenPressed && buttonBehaviour.keyCard != null && !buttonBehaviour.hasKeyCard)
                    {
                        uiBottomText.text = GameConstants.keyCardInteractionInput;

                    }else if(!buttonBehaviour.hasBeenPressed && buttonBehaviour.keyCard != null && buttonBehaviour.hasKeyCard)
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
                                    audioManager.audioSource.PlayOneShot(audioManager._terminalButton);
                                    audioManager.audioSource.PlayOneShot(audioManager._doorOpen);
                                    buttonBehaviour.Open();
                                    break;

                                case ButtonBehaviour.ButtonState.SEQUENCE:
                                    Debug.Log("Button Pressed");
                                    if (!buttonBehaviour.hasBeenPressed)
                                    {
                                        buttonBehaviour.hasBeenPressed = true;
                                        audioManager.audioSource.PlayOneShot(audioManager._terminalButton);
                                        First_ChaseSequence.s_ButtonsPressed += 1;
                                        buttonBehaviour.screenRenderer.materials[2].SetColor("_EmissionColor", Color.green);
                                        buttonBehaviour.screenLight.color = Color.green;
                                    }
                                    if (First_ChaseSequence.s_ButtonsPressed == First_ChaseSequence.s_buttonPressedThreshold)
                                    {
                                    audioManager.audioSource.PlayOneShot(audioManager._doorOpen);
                                    buttonBehaviour.Open();
                                    }
                                    break;
                                case ButtonBehaviour.ButtonState.CHASE:
                                    if (!FinalChaseSequence.isDoorOpening)
                                    {
                                    FinalChaseSequence.StartFinalHunt();
                                    }
                                    break;

                                case ButtonBehaviour.ButtonState.ELEVATOR:
                                    FinalChaseSequence.MoveElevator();
                                 break;

                                case ButtonBehaviour.ButtonState.KEYCARD:

                                if (buttonBehaviour.hasKeyCard)
                                {
                                    buttonBehaviour.screenRenderer.materials[2].SetColor("_EmissionColor", Color.green);
                                    buttonBehaviour.screenLight.color = Color.green;
                                    audioManager.audioSource.PlayOneShot(audioManager._doorOpen);
                                    audioManager.audioSource.PlayOneShot(audioManager._terminalButton);
                                    buttonBehaviour.Open();
                                }
                             
                                break;


                            }
                  
                      
                        
                    }
                }
                else
                {
                    uiBottomText.text = GameConstants.empty;
                }

                if(hit.collider.tag == "KeyCard" && hit.distance <= 3f)
                {
                    ParentContext parent = hit.collider.GetComponent<ParentContext>();
                    uiBottomText.text = "Press " + GameConstants.interactionInput;

                    if (!parent.parentTerminal.hasKeyCard && Input.GetKey(KeyCode.E))
                    {
                        parent.parentTerminal.TakeCard();
                        audioManager.audioSource.PlayOneShot(audioManager._pickup);
                    }
                }


                if (hit.collider.tag == "Blood" && hit.distance <= 3f)
                {

                    BloodPack bloodPack = hit.transform.gameObject.GetComponent<BloodPack>();
                    if (!bloodPack.hasBeenPickedUp)
                    {

                        uiBottomText.text = "Press " + GameConstants.interactionInput;
                    }

                    if(!bloodPack.hasBeenPickedUp && Input.GetKey(KeyCode.E))
                    {
                        bloodPack.OnPickedUp(playerBlood);
                        audioManager.audioSource.PlayOneShot(audioManager._pickup);
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

