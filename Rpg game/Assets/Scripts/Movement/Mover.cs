using UnityEngine.AI;
using UnityEngine;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
       // [SerializeField] Transform target; // to visualize destination point

        Animator animator;

        NavMeshAgent navAgent;

        //functionality - character moves on mouse click
        /* Shoot a ray from cam to screen convert it in world points. use that as target position */
        Ray ray;
        float speed;
        [SerializeField] float maxSpeed = 6f;
        Vector3 localVel;
        Vector3 dest;
        Health health;
        private void Awake()
        {
            navAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        private void LateUpdate()
        {
            navAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }
        public void StarMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }
        public void MoveTo(Vector3 destiantion, float speedFraction)
        {
            //For visualization
//            if (target != null)
            navAgent.isStopped = false;
            navAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navAgent.SetDestination(destiantion);            
        }
       
        private void UpdateAnimator()
        {
            localVel = transform.InverseTransformDirection(navAgent.velocity);
            speed = localVel.z;
            animator.SetFloat("forwardSpeed", speed);

        }
        public void Cancel()
        {
            navAgent.isStopped = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(dest, 0.25f);
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 pos = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = pos.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}

