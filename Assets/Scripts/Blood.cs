using Main.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Game
{
    public class Blood : MonoBehaviour
    {
        public GameObject Player;
        public PlayerData playerData;
        public PlayerMover playerMover;
        public RespawnController respawnController;
        public Reset reset;

        public GameObject bloodObject;
        public Vector3 bloodObjectPos;
        private Quaternion initialRotation;

        public float maxBlood;
        public float minBlood;
        [SerializeField] public float currentBlood;
        public Renderer bloodRenderer;
        Material bloodMaterial;

        public float lossTime;
        public float lossRate;
        public float lossAmount;
        public float lossModifier;
        public float lossLerpModifier;
        public float invincibilityTime;

        private float initialLossTime;
        public float enemyDamage;

        public float defaultBobAmount;
        public float bobFrequency;

        public bool canTakeDamage = true;

        void Start()
        {
            currentBlood = maxBlood;
            initialLossTime = lossTime;
            bloodMaterial = bloodRenderer.material;
            bloodMaterial.SetFloat("_Fill", 0.19f);
            bloodObjectPos = bloodObject.transform.localPosition;
            initialRotation = bloodObject.transform.localRotation;

        }

        private void Update()
        {
            if (GameConstants.gamestates == GameConstants.Gamestates.RUNNING)
            {
                LoseBlood(lossAmount, lossModifier);
                Bobble();
            }


        }


        public void LoseBlood(float amount, float modifier)
        {
            float initialBlood = currentBlood;
            float loss = currentBlood - amount * modifier;

            currentBlood = Mathf.Lerp(initialBlood, loss, lossRate * Time.deltaTime);
            bloodMaterial.SetFloat("_Fill", currentBlood);
            currentBlood = Mathf.Clamp(currentBlood, minBlood, maxBlood);

            if (currentBlood <= 0)
            {
                CheckDeath();
            }

#if UNITY_EDITOR
            //Cheat
            if (Input.GetKeyDown(KeyCode.G))
            {
                currentBlood += 0.5f;
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                StartCoroutine(PlayerTakeDamage());
            }
#endif
        }

        public void CheckDeath()
        {
            Debug.Log("Ya Dead");
            Player.transform.position = respawnController.respawnLocations[respawnController.currentRespawnLocation].position;
            currentBlood = maxBlood;
            reset.ResetAll();
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

        public void TakeDamage()
        {
            if (canTakeDamage)
            {
                Debug.Log("DamgeStart");
                StartCoroutine(PlayerTakeDamage());
            }
        }

        IEnumerator PlayerTakeDamage()
        {
            Debug.Log("DamgeTaken");
            canTakeDamage = false;
            //float initialBlood = currentBlood;
            currentBlood -= enemyDamage;
            bloodMaterial.SetFloat("_Fill", currentBlood);
            currentBlood = Mathf.Clamp(currentBlood, 0f, maxBlood);

            if (currentBlood <= 0)
            {
                CheckDeath();
            }

            yield return new WaitForSeconds(invincibilityTime);
            canTakeDamage = true;
        }

    }

}
