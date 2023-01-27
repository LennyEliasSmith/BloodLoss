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

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (isSpawned)
            {
                anim.enabled = false;
            }
        }

        // Update is called once per frame
        void Update()
        {

            if (isHunting && GameConstants.gamestates == GameConstants.Gamestates.RUNNING)
            {
                ChasePlayer();
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
    }
}

