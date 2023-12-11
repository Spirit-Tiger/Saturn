using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float sensX;
    [SerializeField]
    private float sensY;
    [SerializeField]
    private Transform _orientation;
    [SerializeField]
    private Transform _gun;
    [SerializeField]
    private Transform _playerModel;

    [SerializeField]
    private float _downLimit = -90f;
    [SerializeField]
    private float _upLimit = 90f;

    [SerializeField]
    [Range(0.0f,90.0f)]
    private float _rotationLimit = 45f;


    private Vector2 _turn;
    private float _turnX2;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _turn.x += Input.GetAxisRaw("Mouse X") * sensX * Time.deltaTime;
        _turnX2 += Input.GetAxisRaw("Mouse X") * sensX * Time.deltaTime;
        _turn.y += Input.GetAxisRaw("Mouse Y") * sensY * Time.deltaTime;

        _turn.y = Mathf.Clamp(_turn.y, _downLimit, _upLimit);
        _turn.x = Mathf.Clamp(_turn.x, -_rotationLimit, _rotationLimit);

    }

    private void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(-_turn.y, _turn.x, 0);
        _orientation.rotation = Quaternion.Euler(0, _turnX2, 0);

        if (Mathf.Abs(_turn.x) == _rotationLimit)
        {

            _playerModel.rotation = Quaternion.Euler(0, _turnX2 - transform.localEulerAngles.y, 0);
        }
    }
}
