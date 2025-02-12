using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPackObjectPool : MonoBehaviour
{

    public Transform[] locations;
    public GameObject bloodPack;
    public Transform poolLocation;

    private List<GameObject> packs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        SetPool();

        Reset.CallReset += ResetPool;
    }

    public void SetPool()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            var instatiatedBloodPack = Instantiate(bloodPack, locations[i]);
            packs.Add(instatiatedBloodPack);
            instatiatedBloodPack.transform.parent = locations[i];
            instatiatedBloodPack.transform.position= locations[i].position;

        }
    }

    void ResetPool()
    {
        foreach (var pack in packs)
        {
            Destroy(pack);
        }
        SetPool();
    }
}
