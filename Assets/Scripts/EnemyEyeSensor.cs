using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyEyeSensor : MonoBehaviour
{

    public float Radius = 5f;
    [Range(0f, 360f)]
    public float Angle = 30f;

    private LayerMask _playerMask;
    private LayerMask _obstacleMask;

    public GameObject playerRef;
    public bool CanSeePlayer;

    private EnemyAI _enemyAI;

    private enum EnemyType
    {
        Guard,
        Monster
    };
    [SerializeField]
    private EnemyType _type;
    private void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        _enemyAI = GetComponent<EnemyAI>();
        _playerMask = LayerMask.GetMask("Player");
        _obstacleMask = LayerMask.GetMask("Wall");
    }

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, Radius, _playerMask);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToPlayer = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToPlayer) < Angle)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToPlayer, distance, _obstacleMask))
                {
                    CanSeePlayer = true;
                    if (_type == EnemyType.Guard && _enemyAI.CurrentEnemyState != EnemyAI.EnemyState.Detect)
                    {
                        _enemyAI.ChangeState(EnemyAI.EnemyState.Detect);
                    }
                    if(_type == EnemyType.Monster && _enemyAI.CurrentEnemyState != EnemyAI.EnemyState.Chase) {
                        _enemyAI.ChangeState(EnemyAI.EnemyState.Chase);
                    }
                }
                else
                {
                    CanSeePlayer = false;
                }
            }
            else
            {
                CanSeePlayer = false;
            }
        }
        else if (CanSeePlayer)
        {
            CanSeePlayer = false;
        }
    }
}
