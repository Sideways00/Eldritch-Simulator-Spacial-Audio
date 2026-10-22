using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CarMovement : MonoBehaviour
{
    public Transform drivingDirection;

    public float acceleration = 20f;
    public float maxSpeed = 30f;
    public float reverseSpeed = 10f;
    public float steeringSpeed = 60f;
    public float brakeStrength = 15f;

    private Rigidbody rb;
    private float moveInput;
    private float steerInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Prevent the car from flipping over
        rb.constraints |= RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationZ;
        rb.angularDamping = 5f;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        moveInput = 0f;
        steerInput = 0f;

        if (Keyboard.current == null)
            return;

        if (Keyboard.current.wKey.isPressed ||
            Keyboard.current.upArrowKey.isPressed)
            moveInput += 1f;

        if (Keyboard.current.sKey.isPressed ||
            Keyboard.current.downArrowKey.isPressed)
            moveInput -= 1f;

        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
            steerInput -= 1f;

        if (Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
            steerInput += 1f;
    }

    void FixedUpdate()
    {
        // Use the assigned direction, or the car's forward direction
        Transform direction = drivingDirection != null
            ? drivingDirection
            : transform;

        Vector3 forward = direction.forward;
        forward.y = 0f;
        forward.Normalize();

        float forwardSpeed = Vector3.Dot(
            rb.linearVelocity, forward
        );

        if (moveInput > 0f)
        {
            if (forwardSpeed < maxSpeed)
            {
                rb.AddForce(
                    forward * acceleration,
                    ForceMode.Acceleration
                );
            }
        }
        else if (moveInput < 0f)
        {
            if (forwardSpeed > 0.5f)
            {
                rb.AddForce(
                    -forward * brakeStrength,
                    ForceMode.Acceleration
                );
            }
            else if (forwardSpeed > -reverseSpeed)
            {
                rb.AddForce(
                    -forward * acceleration,
                    ForceMode.Acceleration
                );
            }
        }

        if (Mathf.Abs(forwardSpeed) > 0.5f)
        {
            float directionSign = forwardSpeed >= 0f ? 1f : -1f;

            float turn = steerInput * steeringSpeed
                       * Time.fixedDeltaTime * directionSign;

            rb.MoveRotation(
                rb.rotation * Quaternion.Euler(0f, turn, 0f)
            );
        }
    }
}