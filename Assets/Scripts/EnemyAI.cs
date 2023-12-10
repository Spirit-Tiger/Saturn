using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Transform[] _waypoints;

    [SerializeField]
    private float _delay;

    private WaitForSeconds _waitSeconds;

    private NavMeshAgent _navAgent;
    private int _waypointIndex = 0;
    private Vector3 _target;

    private EnemyEyeSensor _fieldOfView;

    public enum EnemyState
    {
        Idle,
        Patrolling,
        Chase,
        Attack,
        Detect,
    }

    public EnemyState CurrentEnemyState;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _fieldOfView = GetComponent<EnemyEyeSensor>();
        _waitSeconds = new WaitForSeconds(_delay);
    }

    private void Start()
    {
        UpdateDestinition();
    }

    private void Update()
    {
        if (CurrentEnemyState == EnemyState.Patrolling)
        {
            if (Vector3.Distance(transform.position, _target) < 2)
            {
                IterateWaypointIndex();
                UpdateDestinition();
            }
            if(_fieldOfView.CanSeePlayer == false)
            {
                ChangeState(EnemyState.Patrolling);
            }
        }
        if (CurrentEnemyState == EnemyState.Chase)
        {
            if (_fieldOfView.CanSeePlayer == false)
            {
                ChangeState(EnemyState.Patrolling);
            }
        }
        if (CurrentEnemyState == EnemyState.Detect)
        {
            if (_fieldOfView.CanSeePlayer == false)
            {
                ChangeState(EnemyState.Patrolling);
            }
        }
    }

    public void ChangeState(EnemyState state)
    {
        CurrentEnemyState = state;
        switch (CurrentEnemyState)
        {
            case EnemyState.Idle:
                IdleStateTurnOn();
                break;
            case EnemyState.Patrolling:
                PatrollingStateTurnOn();
                break;
            case EnemyState.Detect:
                DetectStateTurnOn();
                break;
            case EnemyState.Chase:
                ChaseStateTurnOn();
                break;
        }
    }

    private void IdleStateTurnOn()
    {
        //_anim.SetTriger("Idel");
        Debug.Log("IdleState");
    }

    private void PatrollingStateTurnOn()
    {
        _navAgent.isStopped = false;
        //_anim.SetTriger("Move");
        _navAgent.SetDestination(_target);
        Debug.Log("PatrollingState");
    }

    private void DetectStateTurnOn()
    {
        //_anim.SetTriger("Detect");
        _navAgent.isStopped = true;
        Debug.Log("DetectState");
    }
    private void ChaseStateTurnOn()
    {
        //_anim.SetTriger("Chase");
        Debug.Log("ChaseState");
    }

    private void UpdateDestinition()
    {
        _target = _waypoints[_waypointIndex].position;
        StartCoroutine(TimeToSwitch());
    }

    private void IterateWaypointIndex()
    {
        _waypointIndex++;
        if (_waypointIndex == _waypoints.Length)
        {
            _waypointIndex = 0;
        }
    }

    private IEnumerator TimeToSwitch()
    {
        yield return _waitSeconds;
        _navAgent.SetDestination(_target);
    }
}
