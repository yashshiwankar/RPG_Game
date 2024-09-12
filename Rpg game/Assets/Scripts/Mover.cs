using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    Transform target;

    NavMeshAgent player;

    private void Awake()
    {
        player = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        player.SetDestination(target.position);
    }
}
