using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Game
{
    [RequireComponent(typeof(Collider))]
    public class Pickup : MonoBehaviour
    {

        public Collider pickupCollider;
        // Start is called before the first frame update


        public virtual void OnPickedUp(Blood blood)
        {
            //add something here. Most likely will be overwritten.
        }
    }
}


