using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] float speed = 10f, destoryAfterImpactTime = 0.2f, maxProjectileLifetime = 7f;
        private float damage;
        Health target;
        [SerializeField] bool isHoming = false;

        [SerializeField] GameObject hitImpactEffect;
        [SerializeField] GameObject[] destoryOnHit;

        void Start()
        {
            if (!isHoming)
                transform.LookAt(GetAimLocation());
        }
        void Update()
        {
            if (target == null) return;
            Move();
        }

        private void Move()
        {
            if (isHoming && !target.IsDead())
                transform.LookAt(GetAimLocation());

            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();

            if (targetCollider != null)
                return target.transform.position + Vector3.up * targetCollider.height / 2f;
            else
                return target.transform.position;
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
            Destroy(gameObject, maxProjectileLifetime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Health>() != target) return;
            if (target.IsDead()) return;
            Instantiate(hitImpactEffect, GetAimLocation(), transform.rotation);
            target.TakeDamage(damage);
            speed = 0;
            foreach(GameObject toDestroy in destoryOnHit)
            {
                Destroy(toDestroy);
            }
            Destroy(gameObject,destoryAfterImpactTime);
        }

    }
}