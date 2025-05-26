using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private Weapon weaponSO = null;
        [SerializeField] bool animate = true;
        [SerializeField] float rotationSpeed = 10f;
        private void Update()
        {
            AnimatePickup();
        }
        void AnimatePickup()
        {
            if (animate == false) return;
            transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Fighter fighter = other.GetComponent<Fighter>();
                fighter.EquipWeapon(weaponSO);
                Destroy(gameObject);
            }
        }
    }
}