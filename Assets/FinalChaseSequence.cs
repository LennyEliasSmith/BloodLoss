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

        public float doorSpeed;

        public bool isDoorOpening;

        public GameObject elevator;

        public Transform entranceDoor;
        public Transform doorDestination;

        public Vector3 entranceDoorInitialPos;
        public Vector3 initialDoorPos;

        public float secondHunterWaitTime;
        public float thirdHunterWaitTime;
        public float fourthHunterWaitTime;

        public IEnumerator finalHunt;

        private Vector3 yPos;
        // Start is called before the first frame update
        void Start()
        {
           
            finalHunt = FinalHunt();
            initialDoorPos = door.transform.position;
            entranceDoorInitialPos = entranceDoor.position;
            yPos = new Vector3(door.transform.position.x, doorDestination.transform.position.y, door.transform.position.z);
            Reset.CallReset += ResetValues;
        }

        // Update is called once per frame
        void Update()
        {
            if (isDoorOpening)
            {
                door.transform.position = Vector3.Lerp(door.transform.position, yPos, doorSpeed * Time.deltaTime);
            }
        }

        public void ResetValues()
        {
            door.transform.position = initialDoorPos;
        }

        IEnumerator FinalHunt()
        {
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
    
        public void SlowDownEnemies()
        {
            //Slow down enemies when trigger entered
        }
        
        public void Elevator()
        {
           //Stop hunters and move elevator up
        }


    }
}

