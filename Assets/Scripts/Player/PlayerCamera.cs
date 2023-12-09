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


    private Vector2 _turn;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _turn.x += Input.GetAxisRaw("Mouse X")  * sensX * Time.deltaTime;
        _turn.y += Input.GetAxisRaw("Mouse Y")  * sensY * Time.deltaTime;

        _turn.y = Mathf.Clamp(_turn.y, _downLimit, _upLimit);

        transform.rotation = Quaternion.Euler(-_turn.y, _turn.x, 0);
        _orientation.rotation = Quaternion.Euler(0, _turn.x, 0);
        _playerModel.rotation = _orientation.rotation;

    }
}
