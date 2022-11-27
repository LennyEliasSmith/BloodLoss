using Main.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Game
{
    public class Blood : MonoBehaviour
    {
        public PlayerData playerData;
        public PlayerMover playerMover;

        public GameObject bloodObject;
        public Vector3 bloodObjectPos;
        private Quaternion initialRotation;

        public float maxBlood;
        [SerializeField] public float currentBlood;
        public Renderer bloodRenderer;
        Material bloodMaterial;

        public float lossTime;
        public float lossRate;
        public float lossAmount;
        public float lossModifier;
        public float lossLerpModifier;

        private float initialLossTime;

        public float defaultBobAmount;
        public float bobFrequency;
        void Start()
        {
            currentBlood = maxBlood;
            initialLossTime = lossTime;
            bloodMaterial = bloodRenderer.material;
            bloodMaterial.SetFloat("_Fill", 0.19f);
            bloodObjectPos = bloodObject.transform.localPosition;
            initialRotation = bloodObject.transform.localRotation;

            Debug.Log(bloodRenderer.material.name);

        }

        private void Update()
        {
            LoseBlood(lossAmount, lossModifier);
            Bobble();

        }


        public void LoseBlood(float amount, float modifier)
        {
            float initialBlood = currentBlood;
            float loss = currentBlood - amount * modifier;

            currentBlood = Mathf.Lerp(initialBlood, loss, lossRate * Time.deltaTime);
            bloodMaterial.SetFloat("_Fill", currentBlood);
            currentBlood = Mathf.Clamp(currentBlood, 0f, maxBlood);

            if (currentBlood <= 0)
            {
                CheckDeath();
            }

            //Cheat
            if (Input.GetKeyDown(KeyCode.G))
            {
                currentBlood += 0.5f;
            }
        }

        public void CheckDeath()
        {

        }

        void Bobble()
        {
            if (playerMover.playerVelocity.magnitude > 0.1f)
            {
                float frequency = bobFrequency;
                float hBobValue = Mathf.Sin(Time.time * frequency) * defaultBobAmount;
                float vBobValue = ((Mathf.Sin(Time.time * frequency * 2f) * 0.5f) + 0.5f) * defaultBobAmount;

                bloodObjectPos.x = hBobValue;
                bloodObjectPos.y = Mathf.Abs(vBobValue);
                bloodObject.transform.localPosition = bloodObjectPos;
            }

        }

    }

}
