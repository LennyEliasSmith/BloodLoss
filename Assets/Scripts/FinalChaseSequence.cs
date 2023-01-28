using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Game
{
    public class FinalChaseSequence : MonoBehaviour
    {
     
        public AudioManager audioManager;
        public EnemySeeker[] enemySeekers;
        public DoorOpen[] tubDoors;

        public GameObject door;
        public float desiredDoorY;
        public float doorSpeed;

        public bool isDoorOpening;

        public Transform elevatorDesiredPos;
        private Transform elevatorInitialPos;
        public float elevatorSpeed;
        public GameObject elevator;
        public GameObject easle1;
        public GameObject easle2;
        public float desiredY;

        public Transform entranceDoor;
        public Transform doorDestination;

        public Vector3 entranceDoorInitialPos;
        public Vector3 initialDoorPos;

        public float secondHunterWaitTime;
        public float thirdHunterWaitTime;
        public float fourthHunterWaitTime;

        public IEnumerator finalHunt;
        public bool finalHuntInProgress;
        public bool elevatorMoving;

        private Vector3 yPos;
        // Start is called before the first frame update
        void Start()
        {
            elevatorMoving = false;
            finalHunt = FinalHunt();
            initialDoorPos = door.transform.position;
            entranceDoorInitialPos = entranceDoor.position;
            elevatorInitialPos.position = elevator.transform.position;
            doorDestination.position = new Vector3(door.transform.position.x, doorDestination.position.y, door.transform.position.z); ;
            //elevatorDesiredPos.position = new Vector3(elevator.transform.position.x, desiredY, elevator.transform.position.z);
            Reset.CallReset += ResetValues;

            //foreach (var enemy in enemySeekers)
            //{
            //    enemy.anim.SetFloat("")
            //}
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
            door.transform.position = initialDoorPos;
            audioManager.audioSource.clip = audioManager._ambienceTrack;
        }

        IEnumerator FinalHunt()
        {
            finalHuntInProgress = true;
            audioManager.SetTrack(audioManager._chaseSequenceTrack);
            isDoorOpening = true;
            tubDoors[0].OpenDoor();
            enemySeekers[0].isHunting = true;
            enemySeekers[0].enabled = true;
            yield return new WaitForSeconds(secondHunterWaitTime);
            tubDoors[1].OpenDoor();
            enemySeekers[1].isHunting = true;
            yield return new WaitForSeconds(thirdHunterWaitTime);
            tubDoors[2].OpenDoor();
            enemySeekers[2].isHunting = true;
            yield return new WaitForSeconds(fourthHunterWaitTime);
            tubDoors[3].OpenDoor();
            enemySeekers[3].isHunting = true;
        }

        public void ResolveHunt()
        {
            foreach (var enemy in enemySeekers)
            {
                enemy.agent.isStopped = true;
                enemy.isHunting = false;
            }
            finalHuntInProgress = false;
        }
        
        public void MoveElevator()
        {
            if (!elevatorMoving)
            {
                elevatorMoving = true;
            }
            audioManager.audioSource.clip = audioManager._ambienceTrack;
            easle1.transform.rotation = new Quaternion(0f,0f,0f, 0f);
            easle2.transform.rotation = new Quaternion(0, 90, 0, 0);
            elevator.transform.position = Vector3.Lerp(elevator.transform.position, elevatorDesiredPos.position, elevatorSpeed * Time.deltaTime);
        }


    }
}

