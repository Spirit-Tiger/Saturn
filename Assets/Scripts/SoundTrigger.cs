using UnityEngine;
using System.Collections;

public class SoundTrigger : MonoBehaviour
{
    public EnemyAI Guard;
    public Transform Waypoint;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            Guard.SetDestinition(Waypoint.position);
            Guard.ChangeState(EnemyAI.EnemyState.Patrolling);
        }
    }
}