using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CubePeople
{
    public class PeopleMove : MonoBehaviour
    {
        NavMeshAgent agent;
        public Vector2 minmaxSpeed = new Vector2(0.5f, 1.5f);

        public int playerState = 0; //0=entry, 1=stay
        public bool refreshDestination = false;
        bool dice;

        public float pauseTime = 1;
        float timeCount;

        //anim
        Animator anim;

        void Start()
        {
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            timeCount = pauseTime;

            agent.speed = RandomSpeed();
            refreshDestination = true;
        }


        void Update()
        {
            float dist = Vector3.Distance(agent.destination, transform.position);
            if (dist < 0.35f)
            {
                //arrived
                if (!dice)
                {
                    playerState = Random.Range(0, 2);
                    dice = true;
                }

                if (playerState == 1)
                {
                    timeCount -= Time.deltaTime;    //wait
                    if (timeCount < 0)
                    {
                        timeCount = pauseTime;
                        dice = false;
                        playerState = 0;    //return zero
                    }
                }
                else
                {
                    if (dice) dice = false;
                    refreshDestination = true;    //new destination
                }
            }

            if (refreshDestination)
            {
                Vector3 randomPoint;
                if (RandomPoint(transform.position, 30.0f, out randomPoint))
                {
                    agent.SetDestination(randomPoint);
                    refreshDestination = false;
                }
            }

            anim.SetFloat("Walk", agent.velocity.magnitude);
        }

        private bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * range;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }

            result = Vector3.zero;
            return false;
        }

        public float RandomSpeed()
        {
            return Random.Range(minmaxSpeed.x, minmaxSpeed.y);
        }
    }
}
