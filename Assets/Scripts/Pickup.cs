using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Game
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Pickup : MonoBehaviour
    {

        public Collider pickupCollider;
        public Rigidbody pickupRigidbody;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            Blood blood = other.GetComponent<Blood>();
            OnPickedUp(blood);

            Debug.Log("Hello");
        }

        protected virtual void OnPickedUp(Blood blood)
        {
            //add something here. Most likely will be overwritten.
        }
    }
}


