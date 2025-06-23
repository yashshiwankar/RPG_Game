using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Make new Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject equippedWeaponPrefab = null;
        [SerializeField] Projectile projectile = null;
        [SerializeField] AnimatorOverrideController weaponAnimatiorOverrideController = null;
        [SerializeField] float range = 2f, weaponDamage = 5f;
        [SerializeField] bool isRightHanded = true;

        const string weaponName = "Weapon";


        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetWeaponRange()
        {
            return range;
        }
        public void SpawnWeapon(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            DestroyOldWeapon(rightHandTransform, leftHandTransform);
            if (equippedWeaponPrefab != null)
            {
                Transform handTransform = isRightHanded? rightHandTransform : leftHandTransform;
                GameObject weaponInstance = Instantiate(equippedWeaponPrefab, handTransform);
                weaponInstance.name = weaponName;

            }
            if(weaponAnimatiorOverrideController != null)
            {
                animator.runtimeAnimatorController = weaponAnimatiorOverrideController;
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        public void LaunchProjectile(Transform rightHandTransform, Transform leftHandTransform, Health health)
        {
            Transform handTransform = isRightHanded ? rightHandTransform : leftHandTransform;

            Projectile projectileInstance = Instantiate(projectile, handTransform.position, Quaternion.identity);
            projectileInstance.SetTarget(health, weaponDamage);
        }
    }
}