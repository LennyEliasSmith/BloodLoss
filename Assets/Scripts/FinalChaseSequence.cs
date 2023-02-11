using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Game
{
    public class FinalChaseSequence : MonoBehaviour
    {
     
        public AudioManager audioManager;
        public EnemySeeker[] enemySeekers;
        public Transform[] initialEnemyPosition;
        public DoorOpen[] tubDoors;

        public GameObject door;
        public float desiredDoorY;
        public float doorSpeed;

        public bool finalHuntInProgress;
        public bool elevatorMoving;
        public bool isDoorOpening;

        public Transform elevatorDesiredPos;
        private Vector3 elevatorInitialPos;
        public float elevatorSpeed;
        public GameObject elevator;
        public GameObject easle1;
        public GameObject easle2;
        public float desiredY;

        public Transform entranceDoor;
        public Transform doorDestination;

       private Vector3 entranceDoorInitialPos;
        private Vector3 initialDoorPos;

        public float secondHunterWaitTime;
        public float thirdHunterWaitTime;
        public float fourthHunterWaitTime;

        private Quaternion initialEasle1Rot;
        private Quaternion initialEasle2Rot;

        private List<Vector3> initialPos = new List<Vector3>();
        // Start is called before the first frame update

        void Start()
        {
            elevatorMoving = false;

            initialDoorPos = door.transform.position;
            entranceDoorInitialPos = entranceDoor.position;
            elevatorInitialPos = elevator.transform.position;

            doorDestination.position = new Vector3(door.transform.position.x, doorDestination.position.y, door.transform.position.z);
            initialEasle1Rot = easle1.transform.rotation;
            initialEasle2Rot = easle2.transform.rotation;
            Reset.CallReset += ResetValues;
            Reset.CallReset += ResetEnemyPositions;

            foreach (var enemy in enemySeekers)
            {
                initialPos.Add(enemy.transform.position);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isDoorOpening)
            {
                door.transform.position = Vector3.Lerp(door.transform.position, doorDestination.position, doorSpeed * Time.deltaTime);
            }

            if (elevatorMoving)
            {
                MoveElevator();
            }
        }

        public void ResetValues()
        {

            isDoorOpening = false;
            finalHuntInProgress = false;
            elevatorMoving = false;

            easle1.transform.localRotation = new Quaternion(0f, 90f, 0f, 0f);
            easle2.transform.localRotation = new Quaternion(0, 90, 0, 0);

            door.transform.position = initialDoorPos;
            audioManager.audioSource.clip = audioManager._ambienceTrack;
            elevator.transform.position = elevatorInitialPos;


        }

        public void ResetEnemyPositions()
        {
            int i = 0;
            foreach (var enemy in enemySeekers)
            {
                if (tubDoors[i].obstacle.enabled)
                {
                    tubDoors[i].obstacle.enabled = false;
                }

                enemy.agent.Warp(initialPos[i]);
                enemy.agent.isStopped = true;
                enemy.isHunting = false;
                enemy.anim.Play(GameConstants.enemyGlitchAnim);

                i++;
                Debug.Log(enemy.name + " has been reset to position " + enemy.transform.localPosition);
            }
        }

        IEnumerator FinalHunt()
        {
            finalHuntInProgress = true;
            isDoorOpening = true;
            audioManager.SetTrack(audioManager._chaseSequenceTrack);

            int i = 0;
            foreach (var enemy in enemySeekers)
            {
                tubDoors[i].OpenDoor();
                enemy.isHunting = true;
                enemy.agent.isStopped = false;
                enemy.enabled = true;
                yield return new WaitForSeconds(secondHunterWaitTime);
            }
           
        }
        public void StartFinalHunt()
        {
            if (audioManager.audioSource.clip != audioManager._chaseSequenceTrack)
            {
                audioManager.SetTrack(audioManager._chaseSequenceTrack);
            }
            StartCoroutine(FinalHunt());
        }
        public void ResolveHunt()
        {
            foreach (var enemy in enemySeekers)
            {
                enemy.agent.isStopped = true;
                enemy.isHunting = false;
                enemy.anim.Play(GameConstants.enemyGlitchAnim);
            }
            finalHuntInProgress = false;
        }
        
        public void MoveElevator()
        {
            if (!elevatorMoving)
            {
                audioManager.audioSource.clip = audioManager._ambienceTrack;
                audioManager.audioSource.PlayOneShot(audioManager._stinger);
                elevatorMoving = true;
               
            }

            easle1.transform.rotation = new Quaternion(0f,0f,0f, 0f);
            easle2.transform.rotation = new Quaternion(0, 180, 0, 0);
            elevator.transform.position = Vector3.Lerp(elevator.transform.position, elevatorDesiredPos.position, elevatorSpeed * Time.deltaTime);
        }



    }
}

