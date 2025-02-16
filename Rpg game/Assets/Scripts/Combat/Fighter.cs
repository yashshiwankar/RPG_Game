using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float range = 2f;
        private Transform target;
        Mover Mover;
        bool isInRange;
        private void Awake()
        {
            Mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (target == null) return;
            if (!IsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < range;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;            
            print("Attack!!");
        }
        public void Cancel()
        {
            target = null;
        }
    }

}