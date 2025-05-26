using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float waypointRadius = 0.25f;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(transform.GetChild(i).position, waypointRadius);
                Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));
            }
        }
        public int GetNextIndex(int i)
        {
            if(i == transform.childCount - 1)
            {
                return 0;
            }
            return i + 1;
        }
        public Vector3 GetWayPoint(int i)
        {
            return transform.GetChild(i).position;
        }

    }
}