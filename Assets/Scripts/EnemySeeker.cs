using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Main.Game
{
    public class EnemySeeker : MonoBehaviour
    {
        public Animator anim;
        public NavMeshAgent agent;
        public float speed;
        public Transform player;

        public bool isRunning;
        public bool isHunting;
        public bool isSpawned;

        public int distance = 10;
        public Vector3 initialPos;

        private First_ChaseSequence first;
        private FinalChaseSequence final;
        // Start is called before the first frame update
        void Start()
        {
            first = FindObjectOfType<First_ChaseSequence>();
            final = FindObjectOfType<FinalChaseSequence>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (isSpawned)
            {
                anim.enabled = false;
            }

            Reset.CallReset += ResetValues;
            initialPos = transform.localPosition;
            
        }

        // Update is called once per frame
        void Update()
        {
            if(final.finalHuntInProgress || first.room1Init)
            {
                if (isHunting && GameConstants.gamestates != GameConstants.Gamestates.PAUSED)
                {
                    ChasePlayer();
                    agent.isStopped = false;
                }
                else
                {
                    agent.isStopped = true;
                }
            }
    
        }

        void ChasePlayer()
        {
            //agent.Move(player.position);
            agent.SetDestination(player.position);
            anim.Play("Armature_Run");
        }

        public void MoveToPoint(Transform movePoint)
        {

        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == GameConstants.PlayerTag)
            {
                Blood playerBlood = other.gameObject.GetComponent<Blood>();
                playerBlood.TakeDamage();
            }
        }

        public void ResetValues()
        {
            //transform.localPosition = initialPos;
            //agent.isStopped = true;
            //isHunting = false;
            //anim.Play(GameConstants.enemyGlitchAnim);
        }
    }
}

