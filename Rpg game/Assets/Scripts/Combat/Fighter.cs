using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [Header("Weapon")]
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        Weapon currentWeapon = null;
        [SerializeField] Weapon defaultWeaponPrefab = null;

        float timeSinceLastAttack = Mathf.Infinity;
        private Health target;
        Mover mover;
        Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            mover = GetComponent<Mover>();
        }
        void Start()
        {
            EquipWeapon(defaultWeaponPrefab);
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead())
            {
                return;
            }
            if (!IsInRange())
            {
                mover.MoveTo(target.transform.position,1f);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0f;
                //Attack handled by Hit() animation event
            }
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
        }

        public void Cancel()
        {
            target = null;
            StopAttack();
            mover.Cancel();
            timeSinceLastAttack += timeBetweenAttacks;

        }
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health checkTarget = combatTarget.GetComponent<Health>();
            return checkTarget != null && !checkTarget.IsDead();

        }
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
            print("Attack!!");
        }
        private void StopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }
        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            weapon.SpawnWeapon(rightHandTransform, leftHandTransform, animator);
        }
        //Animation Event - Attack Animation
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(currentWeapon.GetDamage());
        }

        //Animation Event - Shoot
        void Shoot()
        {
            if (target == null) return;
            currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
        }
    }

}