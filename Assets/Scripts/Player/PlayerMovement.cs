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
    private float _gravity = 9.81f;
    [SerializeField]
    private float _groundCheckRadius = 1f;

    [SerializeField]
    private Transform _groundCheckTransform;

    [SerializeField]
    private float _groundDrag;
    [SerializeField]
    private float _playerHeight;

    private LayerMask _groundLayer;
    private int _playerLayer;

    public Transform orientation;

    private float _horizontalInput;
    private float _verticalInput;

    Vector3 moveDirection;

    private Rigidbody _rb;
    private CharacterController _charController;
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

    public Animator animator;
    private float _velocityY = 0.0f;
    private float _velocityX = 0.0f;
    public float Acceleration = 0.1f;
    public float Deceleration = 0.5f;
    private int velocityHashY;
    private int velocityHashX;

    private void Start()
    {
        velocityHashY = Animator.StringToHash("MoveY");
        velocityHashX = Animator.StringToHash("MoveX");
        _charController = GetComponent<CharacterController>();
        _audio = GetComponent<AudioSource>();
        _groundLayer = LayerMask.GetMask("Ground");
        _playerLayer = ~LayerMask.GetMask("Player");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_groundCheckTransform.position, _groundCheckRadius);
    }

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheckTransform.position, _groundCheckRadius, _playerLayer);
        GetInput();

        if (GameManager.Instance.CanAct)
        {

            if ((Input.GetKey(KeyCode.LeftControl)))
            {
                animator.SetBool("isCrouching", true);
                return;
            }
            else
            {
                animator.SetBool("isCrouching", false);
            }
            //W///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if ((Input.GetKey(KeyCode.W)) && _velocityY < 0.5f)
            {

                _velocityY += Time.deltaTime * Acceleration;
            }

            if (!(Input.GetKey(KeyCode.W)) && _velocityY > 0.0f)
            {

                _velocityY -= Time.deltaTime * Deceleration;
            }
            //S///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if ((Input.GetKey(KeyCode.S)) && _velocityY > -0.5f)
            {

                _velocityY -= Time.deltaTime * Acceleration;
            }

            if (!(Input.GetKey(KeyCode.S)) && _velocityY < 0.0f)
            {

                _velocityY += Time.deltaTime * Deceleration;
            }
            //D///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if ((Input.GetKey(KeyCode.D)) && _velocityX < 0.5f)
            {

                _velocityX += Time.deltaTime * Acceleration;
            }

            if (!(Input.GetKey(KeyCode.D)) && _velocityX > 0.0f)
            {

                _velocityX -= Time.deltaTime * Deceleration;
            }
            //A///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if ((Input.GetKey(KeyCode.A)) && _velocityX > -0.5f)
            {

                _velocityX -= Time.deltaTime * Acceleration;
            }

            if (!(Input.GetKey(KeyCode.A)) && _velocityX < 0.0f)
            {

                _velocityX += Time.deltaTime * Deceleration;
            }
            //Shift///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W)) && _velocityY < 1.0f)
            {
                _isRunning = true;
                _velocityY += Time.deltaTime * Acceleration;
            }

            if (!Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W)) && _velocityY > 0.5f)
            {
                _isRunning = false;
                _velocityY -= Time.deltaTime * Deceleration;
            }
            //Shift///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.S)) && _velocityY > -1.0f)
            {
                _isRunning = true;
                _velocityY -= Time.deltaTime * Acceleration;
            }

            if (!Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.S)) && _velocityY < -0.5f)
            {
                _isRunning = false;
                _velocityY += Time.deltaTime * Deceleration;
            }
            //Shift///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.D)) && _velocityX < 1.0f)
            {
                _isRunning = true;
                _velocityX += Time.deltaTime * Acceleration;
            }

            if (!Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.D)) && _velocityX > 0.5f)
            {
                _isRunning = false;
                _velocityX -= Time.deltaTime * Deceleration;
            }
            //Shift///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A)) && _velocityX > -1.0f)
            {
                _isRunning = true;
                _velocityX -= Time.deltaTime * Acceleration;
            }

            if (!Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A)) && _velocityX < -0.5f)
            {
                _isRunning = false;
                _velocityX += Time.deltaTime * Deceleration;
            }
            //Stop///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (!(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftShift)) && Mathf.Abs(_velocityY) < 0.06f && Mathf.Abs(_velocityX) < 0.06f)
            {
                _isRunning = false;
                _velocityY = 0;
                _velocityX = 0;
            }

            animator.SetFloat(velocityHashY, _velocityY);
            animator.SetFloat(velocityHashX, _velocityX);
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
        if (!GameManager.Instance.IsOnLadder)
        {
            _charController.Move(Vector3.down * _gravity * Time.fixedDeltaTime);
        }

        if (GameManager.Instance.CanAct)
        {
            MovePlayer();
        }
        if (GameManager.Instance.IsOnLadder)
        {
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
            _charController.Move(moveDirection.normalized * speed * Time.fixedDeltaTime);
        }
    }

    private void MoveOnLadder()
    {
        if (_verticalInput != 0 && !GameManager.Instance.IsExitingLadder && !GameManager.Instance.IsEnteringLadderDown)
        {
            if (_verticalInput < 0 && _isGrounded)
            {
                GameManager.Instance.IsOnLadder = false;
                GameManager.Instance.CanAct = true;
            }
            transform.position += new Vector3(0, _ladderSpeed * _verticalInput * Time.fixedDeltaTime, 0);
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