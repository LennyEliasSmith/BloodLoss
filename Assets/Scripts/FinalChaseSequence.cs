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

        public IEnumerator finalHunt;
        public bool finalHuntInProgress;
        public bool elevatorMoving;

        private List<Vector3> enemyInitialPositions = new List<Vector3>();
        // Start is called before the first frame update
        void Start()
        {
            elevatorMoving = false;
            finalHunt = FinalHunt();
            initialDoorPos = door.transform.position;
            entranceDoorInitialPos = entranceDoor.position;
            elevatorInitialPos = elevator.transform.position;
            doorDestination.position = new Vector3(door.transform.position.x, doorDestination.position.y, door.transform.position.z); 
            Reset.CallReset += ResetValues;

            int i = 0;
            foreach (var enemy in enemySeekers)
            {
                enemyInitialPositions.Add(enemySeekers[i].transform.position);
                i++;
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
            door.transform.position = initialDoorPos;
            audioManager.audioSource.clip = audioManager._ambienceTrack;
            elevator.transform.position = elevatorInitialPos;

            easle1.transform.rotation = new Quaternion(0f, 90f, 0f, 0f);
            easle2.transform.rotation = new Quaternion(0, 90, 0, 0);

            isDoorOpening = false;
            finalHuntInProgress = false;
            elevatorMoving = false;

            int i = 0;
            foreach (var enemy in enemySeekers)
            {
                enemy.anim.Play(GameConstants.enemyGlitchAnim);
                enemy.isHunting = false;
                enemy.transform.position = enemyInitialPositions[i];
                i++;
            }
        }

        IEnumerator FinalHunt()
        {
            finalHuntInProgress = true;
            audioManager.SetTrack(audioManager._chaseSequenceTrack);
            isDoorOpening = true;
            tubDoors[0].OpenDoor();
            enemySeekers[0].isHunting = true;
            enemySeekers[0].agent.isStopped = false;
            enemySeekers[0].enabled = true;
            yield return new WaitForSeconds(secondHunterWaitTime);
            tubDoors[1].OpenDoor();
            enemySeekers[1].isHunting = true;
            enemySeekers[0].agent.isStopped = false;
            enemySeekers[0].enabled = true;
            yield return new WaitForSeconds(thirdHunterWaitTime);
            tubDoors[2].OpenDoor();
            enemySeekers[2].isHunting = true;
            enemySeekers[0].agent.isStopped = false;
            enemySeekers[0].enabled = true;
            yield return new WaitForSeconds(fourthHunterWaitTime);
            tubDoors[3].OpenDoor();
            enemySeekers[3].isHunting = true;
            enemySeekers[0].agent.isStopped = false;
            enemySeekers[0].enabled = true;
        }
        public void StartFinalHunt()
        {
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
                elevatorMoving = true;
            }
            audioManager.audioSource.clip = audioManager._ambienceTrack;
            easle1.transform.rotation = new Quaternion(0f,0f,0f, 0f);
            easle2.transform.rotation = new Quaternion(0, 180, 0, 0);
            elevator.transform.position = Vector3.Lerp(elevator.transform.position, elevatorDesiredPos.position, elevatorSpeed * Time.deltaTime);
        }


    }
}

