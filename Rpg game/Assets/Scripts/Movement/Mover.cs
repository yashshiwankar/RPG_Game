using UnityEngine.AI;
using UnityEngine;
using RPG.Combat;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField]
        Transform target; // to visualize destination point

        Animator playerAnimator;

        NavMeshAgent playerNavAgent;

        //functionality - character moves on mouse click
        /* Shoot a ray from cam to screen convert it in world points. use that as target position */
        Ray ray;
        float speed;
        Vector3 localVel;
        Vector3 dest;

        private void Awake()
        {
            playerNavAgent = GetComponent<NavMeshAgent>();
            playerAnimator = GetComponent<Animator>();
        }

        void Update()
        {
            //if (Input.GetMouseButton(0))
            //{
            //    MoveToCursor();
            //}
        }
        private void LateUpdate()
        {
            UpdateAnimator();
        }
        public void StarMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }
        public void MoveTo(Vector3 destiantion)
        {
            target.position = destiantion;
            dest = destiantion;
            playerNavAgent.isStopped = false;
            playerNavAgent.SetDestination(destiantion);            
        }
        public void Stop()
        {
            playerNavAgent.isStopped = true;
        }
        private void UpdateAnimator()
        {
            localVel = transform.InverseTransformDirection(playerNavAgent.velocity);
            speed = localVel.z;
            playerAnimator.SetFloat("forwardSpeed", speed);

        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(dest, 0.25f);
        }

    }
}

