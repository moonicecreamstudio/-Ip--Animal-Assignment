using NodeCanvas.Tasks.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Input
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction rotate;
    private InputAction accelerate;
    private InputAction deaccelerate;

    //private float accelerationSpeed = 5f;
    //private float deaccelerationSpeed = 1f;
    //private float turnSpeed = 2f;

    // Movement
    private Rigidbody rb;
    public WheelControl[] wheels;

    [SerializeField]
    private Camera playerCamera;

    public float motorTorque = 2000;
    public float brakeTorque = 2000;
    public float maxSpeed = 20;
    public float steeringRange = 30;
    public float steeringRangeAtMaxSpeed = 10;
    public float centreOfGravityOffset = -1f;

    private void Start()
    {
        wheels = GetComponentsInChildren<WheelControl>();
    }
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        //rb.interpolation = RigidbodyInterpolation.Interpolate;

    }
    private void OnEnable()
    {
        rotate = player.FindAction("Rotate");
        accelerate = player.FindAction("Accelerate");
        deaccelerate = player.FindAction("Deaccelerate");
        player.Enable();

    }

    private void OnDisable()
    {
        player.Disable();
    }

    private void Update()
    {
        //rb.AddForce(transform.forward * accelerate.ReadValue<float>() * (accelerationSpeed));
        //Debug.Log("accelerate: " + accelerate.ReadValue<float>());
        //Debug.Log("acceleration: " + accelerationSpeed);
        //rb.AddForce(-transform.forward * deaccelerate.ReadValue<float>() * accelerationSpeed);
    }

    private void FixedUpdate()
    {

        float vInput = accelerate.ReadValue<float>() + -deaccelerate.ReadValue<float>();
        float hInput = rotate.ReadValue<Vector2>().x;

        // Calculate current speed in relation to the forward direction of the car
        // (this returns a negative number when traveling backwards)
        float forwardSpeed = Vector3.Dot(transform.forward, rb.velocity);


        // Calculate how close the car is to top speed
        // as a number from zero to one
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);

        // Use that to calculate how much torque is available 
        // (zero torque at top speed)
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

        // …and to calculate how much to steer 
        // (the car steers more gently at top speed)
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        // Check whether the user input is in the same direction 
        // as the car's velocity
        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

        foreach (var wheel in wheels)
        {
            // Apply steering to Wheel colliders that have "Steerable" enabled
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
            }

            if (isAccelerating)
            {
                // Apply torque to Wheel colliders that have "Motorized" enabled
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;
                }
                wheel.WheelCollider.brakeTorque = 0;
            }
            else
            {
                // If the user is trying to go in the opposite direction
                // apply brakes to all wheels
                wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
                wheel.WheelCollider.motorTorque = 0;
            }
        }

        //foreach (var wheel in wheels)
        //{
        //    wheel.motorTorque = accelerate.ReadValue<float>() * motorPower;
        //}

        //for (int i = 0; i < wheels.Length; i++)
        //{
        //    if (i < 2)
        //    {
        //        wheels[i].steerAngle = rotate.ReadValue<Vector2>().x * steerPower;
        //    }
        //}

        //Quaternion deltaRotation = Quaternion.Euler(0, rotate.ReadValue<Vector2>().x * turnSpeed, 0);

        //rb.MoveRotation(rb.rotation * deltaRotation);

        //Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        //localVelocity.x = 0;
        //rb.velocity = transform.TransformDirection(localVelocity);


        //transform.Translate(new Vector3(rotate.ReadValue<Vector2>().x, 0, rotate.ReadValue<Vector2>().y) * maxSpeed * Time.deltaTime);

    }

}
