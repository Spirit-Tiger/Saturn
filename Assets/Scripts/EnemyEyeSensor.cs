using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyEyeSensor : MonoBehaviour
{
    [SerializeField]
    private float _distance = 50f;
    private float _angle = 30f;
    private float _height = 2f;

    private GameObject _player;

    private Mesh _mesh;

    private void Awake()
    {
        _player = GameObject.Find("PLAYER");
    }

    private void FieldOfViewCheck()
    {
        Vector3 directionToPlayer = (_player.transform.position - transform.position).normalized;
        if(Vector3.Angle(transform.forward,directionToPlayer) < _angle)
        {

        }
    }

    private Mesh CreateEyeSightMesh()
    {
        Mesh mesh = new Mesh();
        int numTriangles = 8;
        int numVertices = 3 * numTriangles;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -_angle, 0) * Vector3.forward * _distance;
        Vector3 bottomRight = Quaternion.Euler(0, _angle, 0) * Vector3.forward * _distance;

        Vector3 topCenter = bottomCenter + Vector3.up * _height;
        Vector3 topLeft = bottomLeft + Vector3.up * _height;
        Vector3 topRight = bottomRight + Vector3.up * _height;

        return mesh;
    }

}
