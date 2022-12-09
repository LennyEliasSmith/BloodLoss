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


        protected virtual void OnPickedUp(Blood blood)
        {
            //add something here. Most likely will be overwritten.
        }
    }
}


