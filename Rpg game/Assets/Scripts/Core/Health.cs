using RPG.Saving;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float maxHealth = 100f, currentHealth = 100f;
        Animator animator;
        
        bool isDead = false;
        ActionScheduler actionScheduler;
        public bool IsDead()
        {
            return isDead;
        }
        private void Awake()
        {
            currentHealth = maxHealth;
            if(TryGetComponent<Animator>(out Animator anim))
            {
                animator = anim;
            }
        }

        public void TakeDamage(float damage)
        {
            if(isDead) return;
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
                return;
            }
            Debug.Log($"Current Health {currentHealth}");
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            animator.SetTrigger("death");
            actionScheduler = GetComponent<ActionScheduler>();
            actionScheduler.CancelCurrentAction();
        }

        public object CaptureState()
        {
            return currentHealth;
        }

        public void RestoreState(object state)
        {
            currentHealth = (float)state;
            if (currentHealth <= 0)
            {
                Die();
                return;
            }
        }
    }
}