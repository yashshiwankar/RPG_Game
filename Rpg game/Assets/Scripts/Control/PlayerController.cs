using UnityEngine;

using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fighter fighter;
        Ray ray;
        bool isMoving = false;
        void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        void Update()
        {
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
            print("Nothing to do");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.collider.GetComponent<CombatTarget>();
                if (target == null) continue;
                
                if (Input.GetMouseButtonDown(1))            
                    fighter.Attack(target);


                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            ray = GetMouseRay();
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //target.position = hit.point;
                if (Input.GetMouseButton(0))
                {
                    mover.StarMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 45, Color.red);
        }
    }
}
