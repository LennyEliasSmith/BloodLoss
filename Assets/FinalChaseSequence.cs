using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalChaseSequence : MonoBehaviour
{
    public EnemySeeker[] enemySeekers;

    public GameObject door;

    public float doorSpeed;

    public bool isDoorOpening;


    public Transform doorDestination;

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
        yPos = new Vector3(door.transform.position.x, doorDestination.transform.position.y, door.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDoorOpening)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, yPos,doorSpeed * Time.deltaTime);
        }
    }



    IEnumerator FinalHunt()
    {
        isDoorOpening = true;
        enemySeekers[0].isHunting = true;
        yield return new WaitForSeconds(secondHunterWaitTime);
        enemySeekers[1].isHunting = true;
        yield return new WaitForSeconds(thirdHunterWaitTime);
        enemySeekers[2].isHunting = true;
        yield return new WaitForSeconds(fourthHunterWaitTime);
        enemySeekers[3].isHunting = true;
    }
}
