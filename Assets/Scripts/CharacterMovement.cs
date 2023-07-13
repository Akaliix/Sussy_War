using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10f;

    //[SerializeField]
    //private float SpeedAcceleration = 3f;
    //[SerializeField]
    //private float SpeedDeceleration = 5f;

    [SerializeField]
    private float MovementThreshHold = 0.1f;

    [SerializeField]
    private float Gravity = -10f;

    [SerializeField]
    private float RotationalSpeed = 1f;

    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private VariableJoystick movementJoystick;

    //[SerializeField]
    //private VariableJoystick rotationJoystick;

    private Vector2 movementVector2D;
    private Vector3 movementVector;
    private Vector3 lastFramePos;

    private Plane plane = new Plane(Vector3.up, new Vector3(0, 0.309f, 0));

    private void Awake()
    {
        if (_characterController == null && !TryGetComponent(out _characterController))
        {
            Debug.LogError("CharacterController bulunamadi!");
            UnityEditor.EditorApplication.isPlaying = false;
        }

        if (_playerController == null && !TryGetComponent(out _playerController))
        {
            Debug.LogError("PlayerController bulunamadi!");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        lastFramePos = transform.position;
    }

    private void Update()
    {
        Move();
        Rotate();
        Animation();
    }

    private void Move()
    {
        if (GameSettings.singleton.isMobile)
        {
            movementVector2D = new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
        }
        else
        {
            movementVector2D = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        //Vector2 movementVector2D = new Vector2((Input.GetKey(KeyCode.D) ? 1f :0f) + (Input.GetKey(KeyCode.A) ? -1f : 0f), (Input.GetKey(KeyCode.W) ? 1f : 0f) + (Input.GetKey(KeyCode.S) ? -1f : 0f));

        float movementMagnitudeSquared = movementVector2D.x * movementVector2D.x + movementVector2D.y * movementVector2D.y;
        if (movementMagnitudeSquared < MovementThreshHold)
        {
            movementVector2D = new Vector2();
            movementVector = new Vector3();
            _playerController._playerAnimator.SetFloat("SpeedX", 0f);
            _playerController._playerAnimator.SetFloat("SpeedY", 0f);
            return;
        }
        else
        {
            if (movementMagnitudeSquared > 1)
            {
                movementVector2D.Normalize();
            }
            movementVector = new Vector3(movementVector2D.x, 0, movementVector2D.y) * (Speed * Time.deltaTime);
        }

        if (!_characterController.isGrounded)
        {
            movementVector.y = Gravity * Time.deltaTime;
        }

        _characterController.Move(movementVector);
        /*_playerController._playerAnimator.SetFloat("SpeedX", _characterController.velocity.x);
        _playerController._playerAnimator.SetFloat("SpeedY", _characterController.velocity.z);*/
    }

    private void Rotate()
    {
        Vector2 rotationVector2D = new Vector2();

        if (GameSettings.singleton.isMobile)
        {
            rotationVector2D = new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
            _playerController._playerModel.rotation = Quaternion.Slerp(_playerController._playerModel.transform.rotation, Quaternion.Euler(new Vector3(0, Mathf.Atan2(rotationVector2D.x, rotationVector2D.y) * 180 / Mathf.PI, 0)), Time.deltaTime * RotationalSpeed);
        }
        else
        {
            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                Vector3 worldPosition = ray.GetPoint(distance);
                //Debug.Log(worldPosition);
                Vector3 orientationForwardVector = worldPosition - transform.position;
                orientationForwardVector.y = 0f;
                Quaternion tempRot = Quaternion.LookRotation(orientationForwardVector);
                _playerController._playerModel.rotation = Quaternion.Slerp(_playerController._playerModel.transform.rotation, tempRot, Time.deltaTime * RotationalSpeed);
            }
        }
    }

    private void Animation()
    {
        Vector3 _newSpeed = new Vector3();
        if (Time.deltaTime != 0f)
        {
            _newSpeed = (transform.position - lastFramePos) / Time.deltaTime;
        }
        lastFramePos = transform.position;
        Vector3 _relativeSpeed = _playerController._playerModel.transform.InverseTransformDirection(_newSpeed);
        _playerController._playerAnimator.SetFloat("SpeedX", _relativeSpeed.x);
        _playerController._playerAnimator.SetFloat("SpeedY", _relativeSpeed.z);
        Debug.Log(_relativeSpeed);
    }
}
