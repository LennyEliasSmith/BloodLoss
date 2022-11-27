using Main.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Main.Game
{
    public class Scare : MonoBehaviour
    {
        public enum scareCase
        {
            SCAREBLIP,
            SCAREWALK
        }
        public scareCase scareEnum;

        public GameObject scarePrefab;
        public GameObject spotLight;
        public Animator animator;

        public Transform end;

        public bool isRunning;
        public bool isGlitching;

        public float flashTime = 0.1f;
        public float walkTime;
        public float walkSpeed;
        public bool hasActivated = false;

        public IEnumerator spotlightScare;
        public IEnumerator walkScare;
        // Start is called before the first frame update

        private void Start()
        {
            spotlightScare = SpotlightScare();
            walkScare = WalkScare();
        }

        IEnumerator WalkScare()
        {
            float elapsedTime = 0;
            animator.Play(GameConstants.enemyWalkAnim);
            Vector3 startPos = scarePrefab.transform.position;

            while (elapsedTime < walkSpeed)
            {
                scarePrefab.transform.position = Vector3.Lerp(startPos, end.position, (elapsedTime / walkSpeed));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(walkTime);

            scarePrefab.SetActive(false);
        }
        IEnumerator SpotlightScare()
        {
            hasActivated = true;
            spotLight.SetActive(true);
            scarePrefab.SetActive(true);
            animator.Play(GameConstants.enemyGlitchAnim);
            yield return new WaitForSeconds(flashTime);
            spotLight.SetActive(false);
            scarePrefab.SetActive(false);

        }
    }

}
