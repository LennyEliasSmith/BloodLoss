using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Game
{
    public class BloodPack : Pickup
    {
        public enum BloodType
        {
            SMALL,
            MEDIUM,
            LARGE
        }

        public BloodType type;

        [HideInInspector] public float bloodAmount;

        [SerializeField] private float smallBloodAmount;
        [SerializeField] private float mediumBloodAmount;
        [SerializeField] private float largeBloodAmount;

        // Start is called before the first frame update
        void Start()
        {
            switch (type)
            {
                case BloodType.SMALL:
                    bloodAmount = smallBloodAmount;
                    break;
                case BloodType.MEDIUM:
                    bloodAmount = mediumBloodAmount;
                    break;
                case BloodType.LARGE:
                    bloodAmount = largeBloodAmount;
                    break;

            }
        }

        protected override void OnPickedUp(Blood blood)
        {
            Blood playerBlood = blood.GetComponent<Blood>();

            playerBlood.currentBlood += bloodAmount;

            //Replace when object pool is done
            Destroy(gameObject);
        }

    }
}

