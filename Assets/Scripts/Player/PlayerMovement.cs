using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Walk Settings")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _gravity = -9.81f;

    [Header("Rotation Settings")]
    [SerializeField] private float _mouseSensitivity = 0.1f;
    [SerializeField] private Transform _cameraTransform;

    private CharacterController _controller;
    private Vector2 _moveInput;
    private Vector2 _rotationInput;
    private float _xRotation = 0f;

    private Vector3 _velocity;

    private void Awake() 
    {
        _controller = GetComponent<CharacterController>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        ApplyGravity();
        HandleMovement();
        HandleRotation();
    }

    public void OnMove(InputValue value) 
    {
        _moveInput = value.Get<Vector2>();
    }

    public void OnRotation(InputValue value) 
    {
        _rotationInput = value.Get<Vector2>();
    }
     
    private void HandleMovement()
    {
        Vector3 move = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        _controller.Move(move * _speed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        float mouseX = _rotationInput.x * _mouseSensitivity;
        float mouseY = _rotationInput.y * _mouseSensitivity;


        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f); 

        _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX); 
    }


    private void ApplyGravity()
    {
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }



}