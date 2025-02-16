using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCam : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        Camera cam;
        void Awake()
        {
            cam = Camera.main;
        }

        void LateUpdate()
        {
            cam.transform.position = target.position + offset;
        }
    }
}
