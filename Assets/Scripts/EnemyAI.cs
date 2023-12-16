using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private float _speed;

    public bool Walking;

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

        _speed = _navAgent.speed;
    }

    private void Start()
    {
        ChangeState(EnemyState.Idle);
        if (Walking)
        {
            UpdateDestinition();
            ChangeState(EnemyState.Patrolling);
        }
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
      /*      if(_fieldOfView.CanSeePlayer == false)
            {
                ChangeState(EnemyState.Patrolling);
            }*/
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
        Debug.Log("IdleState");
    }

    private void PatrollingStateTurnOn()
    {
        _navAgent.isStopped = false;
        _navAgent.speed = _speed;
        GetComponent<Animator>().Play("Gwalking");
        GetComponent<Animator>().SetTrigger("Go");

        _navAgent.SetDestination(_target);
        Debug.Log("PatrollingState");
    }

    private void DetectStateTurnOn()
    { 
        _navAgent.isStopped = true;
        _navAgent.speed = 0;
        GetComponent<Animator>().Play("Gwhisle");
        StartCoroutine(TimeToDeath());
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

    private IEnumerator TimeToDeath()
    {
        yield return new WaitForSeconds(3f);
        GameManager.Instance.CanAct = false;
        StoryManager.Instance.BlackScreen.gameObject.SetActive(true);
        GameManager.Instance.Player.transform.position = GameManager.Instance.RespawnPoint.position;
        GameManager.Instance.Player.transform.position = Vector3.MoveTowards(GameManager.Instance.Player.transform.position, GameManager.Instance.RespawnPoint.position, 1000f);
        StartCoroutine(ShowPlayer());
    }

    private IEnumerator ShowPlayer()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.CanAct = true;
        StoryManager.Instance.BlackScreen.gameObject.SetActive(false);
    }

    public void DetectSound()
    {
        GetComponent<AudioSource>().Play();
    }
}
