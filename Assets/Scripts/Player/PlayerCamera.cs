using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        _turn.y = Mathf.Clamp(_turn.y, -80f, 80f);

        transform.rotation = Quaternion.Euler(-_turn.y, _turn.x, 0);
        _orientation.rotation = Quaternion.Euler(0, _turn.x, 0);
    }
}
