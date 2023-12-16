using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using static UnityEngine.GraphicsBuffer;
using Unity.Burst.CompilerServices;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _runSpeed;
    [SerializeField]
    private float _ladderSpeed;

    [SerializeField]
    private float _groundDrag;
    [SerializeField]
    private float _playerHeight;

    private LayerMask _groundLayer;

    public Transform orientation;

    private float _horizontalInput;
    private float _verticalInput;

    Vector3 moveDirection;

    private Rigidbody _rb;
    private AudioSource _audio;

    [SerializeField]
    private AudioClip[] _footstepSounds;

    [SerializeField]
    private float _minTimeBetweenFootsteps = 0.3f;
    [SerializeField]
    private float _maxTimeBetweenFootsteps = 0.45f;
    [SerializeField]
    private float _minTimeBetweenFootstepsRun = 0.1f;
    [SerializeField]
    private float _maxTimeBetweenFootstepsRun = 0.2f;

    private float _timeSinceLastFootstep;

    private bool _isMoving = false;
    private bool _isRunning = false;
    private bool _isGrounded = false;
    private bool _isOnFirstPoint = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _audio = GetComponent<AudioSource>();
        _groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f);
        GetInput();
        if (GameManager.Instance.CanAct)
        {

            if (_isGrounded)
            {
                _rb.drag = _groundDrag;
            }
            else
            {
                _rb.drag = 0;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _isRunning = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _isRunning = false;
            }
            SpeedControl();
        }

        if (GameManager.Instance.IsOnLadder)
        {
      
            RaycastHit hit;
            float hitDist = 2f;


            if (GameManager.Instance.IsEnteringLadderDown == false)
            {
                if (Physics.Raycast(transform.position + Vector3.down * 0.3f, orientation.forward, out hit, hitDist, LayerMask.GetMask("Ladder")))
                {
                    Debug.DrawLine(transform.position + Vector3.down * 0.3f, transform.position + Vector3.down * 0.3f + orientation.forward * hitDist, Color.yellow);
                    //Debug.Log(hit.collider.transform.forward);
                }
                else
                {
                    Debug.DrawLine(transform.position + Vector3.down * 0.3f, transform.position + Vector3.down * 0.3f + orientation.forward * hitDist, Color.yellow);
                    GameManager.Instance.IsExitingLadder = true;
                    //GameManager.Instance.IsOnLadder = false;
                }
            }


        }

       
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CanAct)
        {
            _rb.useGravity = true;
            MovePlayer();
        }
        if (GameManager.Instance.IsOnLadder)
        {
            _rb.useGravity = false;
            MoveOnLadder();
        }

        if (GameManager.Instance.IsEnteringLadderDown)
        {
            Transform point3 = GameManager.Instance.CurrentLadder.transform.GetChild(2);
            GameManager.Instance.Player.transform.GetChild(0).rotation = point3.rotation;
            GameManager.Instance.CameraPoint.transform.localRotation = Quaternion.Slerp(GameManager.Instance.CameraPoint.transform.localRotation, Quaternion.Euler(0, 0, 0), 3f * Time.deltaTime);
            orientation.localEulerAngles = GameManager.Instance.CurrentLadder.transform.eulerAngles;
            orientation.forward = point3.forward;

            transform.position = Vector3.MoveTowards(transform.position, point3.position, _ladderSpeed * Time.deltaTime);
            if (transform.position == point3.position)
            {
                GameManager.Instance.IsEnteringLadderDown = false;
            }
        }

        if (GameManager.Instance.IsExitingLadder)
        {

            Transform point1 = GameManager.Instance.CurrentLadder.transform.GetChild(0);
            Transform point2 = GameManager.Instance.CurrentLadder.transform.GetChild(1);

            if (_isOnFirstPoint == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, point1.position, _ladderSpeed * Time.fixedDeltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, point2.position, _ladderSpeed * Time.fixedDeltaTime);
            }

            if (transform.position == point1.position)
            {
                _isOnFirstPoint = true;
            }

            if (transform.position == point2.position)
            {
                _isOnFirstPoint = false;
                GameManager.Instance.IsExitingLadder = false;
                GameManager.Instance.IsOnLadder = false;
                GameManager.Instance.CanAct = true;
            }

        }
    }

    private void GetInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        if (_horizontalInput != 0 || _verticalInput != 0)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }

        if (_isMoving)
        {
            PlayStepSounds();
        }
        if (!_isMoving)
        {
            _audio.Stop();

        }

        moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;

        float speed = 0;
        if (_isRunning)
        {
            speed = _runSpeed;
        }
        else
        {
            speed = _moveSpeed;
        }

        if (_isGrounded)
        {
            _rb.AddForce(moveDirection.normalized * speed, ForceMode.Force);
        }
    }

    private void MoveOnLadder()
    {
        if (_verticalInput != 0 && !GameManager.Instance.IsExitingLadder && !GameManager.Instance.IsEnteringLadderDown)
        {
            if(_verticalInput < 0 && _isGrounded)
            {
                GameManager.Instance.IsOnLadder = false;
                GameManager.Instance.CanAct = true;
            }
            transform.position += new Vector3(0, _ladderSpeed * _verticalInput * Time.fixedDeltaTime, 0);
        }
    }

    private void SpeedControl()
    {

        Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        float speed = 0;

        if (_isRunning)
        {
            speed = _runSpeed;
        }
        else
        {
            speed = _moveSpeed;
        }

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }

    private void PlayStepSounds()
    {
        float minTime = 0;
        float maxTime = 0;

        int arrayLength = 0;

        if (_isRunning)
        {
            minTime = _minTimeBetweenFootstepsRun;
            maxTime = _maxTimeBetweenFootstepsRun;
            arrayLength = _footstepSounds.Length;
        }
        else
        {
            minTime = _minTimeBetweenFootsteps;
            maxTime = _maxTimeBetweenFootsteps;
            arrayLength = _footstepSounds.Length - 1;
        }

        if (Time.time - _timeSinceLastFootstep >= UnityEngine.Random.Range(minTime, maxTime))
        {
            AudioClip footstepSound = _footstepSounds[UnityEngine.Random.Range(0, arrayLength)];
            _audio.PlayOneShot(footstepSound);
            _timeSinceLastFootstep = Time.time;
        }
    }
}