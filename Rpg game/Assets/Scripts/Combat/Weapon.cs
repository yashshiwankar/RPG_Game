using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Make new Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject equippedWeaponPrefab = null;
        [SerializeField] AnimatorOverrideController weaponAnimatiorOverrideController = null;
        [SerializeField] float range = 2f, weaponDamage = 5f;
        [SerializeField] bool isRightHanded = true;
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
            if (equippedWeaponPrefab != null)
            {
                Transform handTransform = isRightHanded? rightHandTransform : leftHandTransform;
                Instantiate(equippedWeaponPrefab, handTransform);
            }
            if(weaponAnimatiorOverrideController != null)
            {
                animator.runtimeAnimatorController = weaponAnimatiorOverrideController;
            }
        }
    }
}