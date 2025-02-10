using NodeCanvas.Tasks.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //input fields
    //private ThirdPersonActionsAsset playerActionsAsset;
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;

    //movement fields
    private Rigidbody rb;
    [SerializeField]
    private float movementForce = 1f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

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
        move = player.FindAction("Move");
        player.Enable();
    }

    private void OnDisable()
    {
        player.Disable();
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y) * maxSpeed * Time.deltaTime);

        //LookAt();

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

    //private void LookAt()
    //{
    //    Vector3 direction = rb.velocity;
    //    direction.y = 0f;

    //    if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
    //        this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
    //    else
    //        rb.angularVelocity = Vector3.zero;
    //}


    // Update is called once per frame
    //void Update()
    //{
    //    transform.Translate(new Vector3(movementInput.x, 0, movementInput.y) * speed * Time.deltaTime);
    //}

    //public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();


}
