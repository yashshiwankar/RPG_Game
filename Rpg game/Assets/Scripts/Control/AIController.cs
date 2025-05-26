using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f, suspicionTimer = 3f, dwellingTime = 1.5f, waypointTolerence = 1f;
        [Range(0,1)] [SerializeField] 
        private float patrolSpeedFraction = 0.2f;
        private GameObject player;
        private float distance;
        private Fighter fighter;
        private Health Health;
        private Mover mover;
        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceLastArrivedAtWaypoint = Mathf.Infinity;

        private int currentWaypointIndex = 0;

        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] bool canPatrol;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            Health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardPosition = transform.position;
        }
        private void Update()
        {
            if (Health.IsDead()) return;

            //Attack State
            if (IsPlayerInRange() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0f;
                AttackBehaviour();
            }
            //Suspicion State
            else if (timeSinceLastSawPlayer < suspicionTimer)
            {
                SuspicionBehaviour();
            }
            else if (canPatrol)
            {
                PatrolBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceLastArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    timeSinceLastArrivedAtWaypoint = 0f;
                    CycleWaypoint();
                }
                    nextPosition = GetCurrentWaypoint();
            }
            if (timeSinceLastArrivedAtWaypoint > dwellingTime)
                mover.StarMoveAction(nextPosition, patrolSpeedFraction);
        }

        private bool AtWayPoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerence;
        }

        private void CycleWaypoint()
        {
             currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            //if(currentWaypointIndex <= 0)
            //{
            //    return patrolPath.GetWayPoint(0);
            //}
            return patrolPath.GetWayPoint(currentWaypointIndex);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool IsPlayerInRange()
        {
            if (player == null)
            {
                Debug.LogError("Player not found. NULL error");
                return false;
            }
            distance = DistanceToPlayer();

            if(distance < chaseDistance) return true;
            else return false;
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}