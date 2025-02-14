using NodeCanvas.Tasks.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //input fields
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction rotate;
    private InputAction accelerate;
    private InputAction deaccelerate;

    private float accelerationSpeed = 5f;
    private float deaccelerationSpeed = 1f;
    private float turnSpeed = 2f;

    //movement fields
    private Rigidbody rb;

    [SerializeField]
    private Camera playerCamera;



    // Start is called before the first frame update
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
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
        rb.AddForce(transform.forward * accelerate.ReadValue<float>() * accelerationSpeed);
        rb.AddForce(-transform.forward * deaccelerate.ReadValue<float>() * accelerationSpeed);


    }

    private void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(0, rotate.ReadValue<Vector2>().x * turnSpeed, 0);

        rb.MoveRotation(rb.rotation * deltaRotation);

        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        localVelocity.x = 0;
        rb.velocity = transform.TransformDirection(localVelocity);


        //transform.Translate(new Vector3(rotate.ReadValue<Vector2>().x, 0, rotate.ReadValue<Vector2>().y) * maxSpeed * Time.deltaTime);

        //forceDirection += move.ReadValue<Vector2>().x * movementForce;
        //forceDirection += move.ReadValue<Vector2>().y * movementForce;

        //rb.AddForce(forceDirection, ForceMode.Impulse);
        //forceDirection = Vector3.zero;

        //if (rb.velocity.y < 0f)
        //    rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        //Vector3 horizontalVelocity = rb.velocity;
        //horizontalVelocity.y = 0;
        //if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        //    rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
    }

    private void Move()
    {

    }

}
