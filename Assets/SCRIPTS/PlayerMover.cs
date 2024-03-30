using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    //Animator _animator;
    float _animValue;
    float _gravity = 0;
    [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;
    private Vector3 _forceDirection = Vector3.zero;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float _movementForce;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    private ThirdPersonInputSystem playerActionsAsset;
    private InputAction _move;
    private InputAction _mouseLook;
    private InputAction _run;
    private Rigidbody _rb;
    private void Awake()
    {
        //_animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        playerActionsAsset = new ThirdPersonInputSystem();
        _movementForce = _walkSpeed;
    }
    private void OnEnable()
    {
        playerActionsAsset.Enable();
        _move = playerActionsAsset.Player.MoveInput;
        _mouseLook = playerActionsAsset.Player.MouseInput;
    }
    private void OnDisable()
    {
        playerActionsAsset.Disable();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(1);
        }
        LookAt();
        GetInput();
        Move();  
    }
    private void GetInput()
    {
        _forceDirection += _move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * _movementForce;
        _forceDirection += _move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * _movementForce;
    }
    private void Move()
    {
        if(_run.IsPressed() && _move.IsPressed())
        {

        }
        _rb.AddForce(_forceDirection, ForceMode.Impulse);
        _forceDirection = Vector3.zero;
        if (_rb.velocity.y < 0f)
            _rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        Vector3 horizontalVelocity = _rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > _movementForce * _movementForce)
            _rb.velocity = horizontalVelocity.normalized * _movementForce + Vector3.up * _rb.velocity.y;
    }
    private void LookAt()
    {
        Vector3 direction = _rb.velocity;
        direction.y = 0f;
        if (_move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            this._rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, this._rb.rotation, turnSpeed);

        }
        else
            _rb.angularVelocity = Vector3.zero;
    }
    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }
}
