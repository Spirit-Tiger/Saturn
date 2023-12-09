using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Transform[] _waypoints;

    private NavMeshAgent _navAgent;
    private int _waypointIndex = 0;
    private Vector3 _target;

    private void Awake()
    {
        _navAgent= GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        UpdateDestinition();
    }

    private void Update()
    {
       
        if (Vector3.Distance(transform.position, _target) < 2)
        {
            IterateWaypointIndex();
            UpdateDestinition();
        }
    }

    private void UpdateDestinition()
    {
        _target = _waypoints[_waypointIndex].position;
        _navAgent.SetDestination( _target );
        transform.LookAt(_target, Vector3.up);
    }

    private void IterateWaypointIndex()
    {
        _waypointIndex++;
        if (_waypointIndex == _waypoints.Length)
        {
            _waypointIndex = 0;
        }
    }
}
