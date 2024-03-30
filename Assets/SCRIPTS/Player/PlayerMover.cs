using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
public class PlayerMover : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] private CharacterController charController;
    private Vector2 _rotation;
    private Vector3 _moveDirection;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string mouseX = "Mouse X";
    private const string mouseY = "Mouse Y";
    [SerializeField] private List<Animator> _animators;
    float _animValue;
    float _gravity = 0;
    //[SerializeField] private CinemachineFreeLook _cinemachineFreeLook;
    private Vector3 _forceDirection = Vector3.zero;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float _movementForce;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    private ThirdPersonInputSystem playerActionsAsset;
    private InputAction _move;
    private InputAction _run;
    private Rigidbody _rb;
    private void Awake()
    {
        
        _rb = GetComponent<Rigidbody>();
        playerActionsAsset = new ThirdPersonInputSystem();
        _movementForce = _walkSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }
    private void OnEnable()
    {
        playerActionsAsset.Enable();
        _move = playerActionsAsset.Player.MoveInput;
        _run = playerActionsAsset.Player.RunInput;
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
        GetInput();
        Move();  
    }
    private void GetInput()
    {
        
      
    }
    private void Move()
    {
        if(_run.IsPressed() && _move.IsPressed())
        {
            foreach (var animator in _animators)
                animator.SetFloat("speed", 1, 3f, 0.3f);
            _movementForce = _runSpeed;
        }
        else if(!_run.IsPressed() && _move.IsPressed())
        {
            foreach (var animator in _animators)
                animator.SetFloat("speed", 0.5f, 3f, 0.3f);
            _movementForce = _walkSpeed;
        }
        else
        {
            foreach (var animator in _animators)
                animator.SetFloat("speed", 0, 3f, 0.3f);
            _movementForce = 0;
        }
        var vec = playerCamera.transform.forward;
        vec.y = 0f;

        var rotation = Quaternion.Euler(0, playerCamera.transform.rotation.eulerAngles.y, 0);
        var input = new Vector3(_move.ReadValue<Vector2>().x, 0, _move.ReadValue<Vector2>().y);
        var dir = rotation * input;
        dir.y = 0;
        dir = dir * _movementForce * Time.deltaTime;
        charController.Move(dir);
        LookAt();
        
    }
    private void LookAt()
    {
        Vector3 direction = new Vector3(_move.ReadValue<Vector2>().x, 0, _move.ReadValue<Vector2>().y);
        direction.y = 0f;
        var rotation = Quaternion.Euler(0, playerCamera.transform.rotation.eulerAngles.y, 0);

        direction = rotation * direction  ;
        if (_move.ReadValue<Vector2>().sqrMagnitude > 0.1f )
        {
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), turnSpeed);

        }
        
    }

}
