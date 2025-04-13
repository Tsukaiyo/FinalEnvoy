using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, ISaveManager
{
    [SerializeField] public InteractPromptUI _prompt;

    public float crouchSpeed = 1.5f;
    public float walkingSpeed = 3f;
    public float sprintSpeed = 6f;

    private float currentMoveSpeed;

    private Vector2 moveInput;
    private bool isSprinting;
    private bool isCrouching;

    private Vector3 forwardVector, rightVector;
    private float interactDistance = 2F;

    private PlayerInput playerInput;
    private Rigidbody rb;
    private InputAction moveAction, sprintAction, crouchAction, interactAction;

    private float dodgeForce = 4F;
    private float launchForce = 4F;

    // TEMPORARY FOR DEMONSTRATING SAVE LOAD
    public void LoadData(SaveData saveData)
    {
        transform.position = saveData.playerPos;
    }

    // TEMPORARY FOR DEMONSTRATING SAVE LOAD
    public void SaveData(ref SaveData saveData)
    {
        saveData.playerPos = transform.position;
        saveData.quests["Quest 1"] = (int) transform.position.x;
        saveData.quests["Quest 2"] = (int) transform.position.z;
    }

    private SphereCollider _sphereCollider;


    // Start is called before the first frame update
    void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();

        forwardVector = Camera.main.transform.forward;
        forwardVector.y = 0f;
        forwardVector = forwardVector.normalized;
        
        playerInput = GetComponent<PlayerInput>();

        rightVector = Quaternion.Euler(new Vector3(0, 90, 0)) * forwardVector;

        currentMoveSpeed = walkingSpeed;

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;


        moveAction = playerInput.actions["Move"];
        sprintAction = playerInput.actions["Sprint"];
        crouchAction = playerInput.actions["Crouch"];
        interactAction = playerInput.actions["Interact"];
        //attackAction = playerInput.actions["Attack"];
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();  // Read movement input

        isSprinting = sprintAction.IsPressed();  // Check if sprinting
        isCrouching = crouchAction.IsPressed();  // Check if crouching

        // Set movement speed based on input
        if (isSprinting)
        {
            currentMoveSpeed = sprintSpeed;
        }
        else if (isCrouching)
        {
            currentMoveSpeed = crouchSpeed;
        }
        else
        {
            currentMoveSpeed = walkingSpeed;
        }
            
        if (moveInput != Vector2.zero)
        {
            Move();
        }

        // Handle interaction
        if (interactAction.WasPressedThisFrame())
        {
            Interact();
        }
    }

    void Move()
    {
        Vector3 rightMovement = rightVector * currentMoveSpeed * moveInput.x;
        Vector3 forwardMovement = forwardVector * currentMoveSpeed * moveInput.y;

        // Calculate what is forward
        Vector3 direction = Vector3.Normalize(rightMovement + forwardMovement);

        Vector3 newPosition = transform.position + rightMovement + forwardMovement;

        if (direction != Vector3.zero)
        {
            transform.forward = direction;  // Rotate towards movement direction
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
    }


    void Interact()
    {
        Vector3 boxCenter = transform.position + transform.forward * (interactDistance / 2);

        Vector3 boxSize = new Vector3(1f, 1f, interactDistance / 2);

        Collider[] hits = Physics.OverlapBox(boxCenter, boxSize, transform.rotation);

        foreach (Collider hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.interact(gameObject);
            }
        }
    }

    /// <summary>
    /// Displays prompt if gameobject contains IInteractable component.
    /// </summary>
    /// <param name="other">Used to retrieve IInteractable component</param>
    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.gameObject.GetComponent<IInteractable>();

        if (interactable != null)
            _prompt.OpenPrompt(interactable.Prompt);
    }

    private void OnTriggerExit(Collider other)
    {
        _prompt.ClosePrompt();
    }

    public void Dodge()
    {
        rb.AddForce(transform.forward * -1 * dodgeForce, ForceMode.VelocityChange);
    }
    public void BlastOff()
    {
        rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
    }
}
