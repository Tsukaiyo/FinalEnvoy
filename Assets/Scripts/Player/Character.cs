using UnityEngine;
using UnityEngine.InputSystem;
public class Character : MonoBehaviour, ISaveManager
{
    [Header("Controls")]
    public float playerSpeed = 5.0f;
    public float crouchSpeed = 2.0f;
    public float sprintSpeed = 7.0f;
    public float jumpHeight = 0.8f;
    public float gravityMultiplier = 2;
    public float rotationSpeed = 5f;
    public float crouchColliderHeight = 1.35f;

    [Header("Animation Smoothing")]
    [Range(0, 1)]
    public float speedDampTime = 0.1f;
    [Range(0, 1)]
    public float velocityDampTime = 0.9f;
    [Range(0, 1)]
    public float rotationDampTime = 0.2f;
    [Range(0, 1)]
    public float airControl = 0.5f;

    // Migration from player controller
    [SerializeField] public InteractPromptUI _prompt;
    private float interactDistance = 2F;
    private InputAction interactAction;
    private float dodgeForce = 4F;
    private float launchForce = 4F;
    private SphereCollider _sphereCollider;
    //

    public StateMachine movementSM;
    public StandingState standing;
    public JumpingState jumping;
    public CrouchingState crouching;
    public LandingState landing;
    public SprintState sprinting;
    public SprintJumpState sprintJumping;
    public CombatState combatting;
    public AttackState attacking;

    [HideInInspector]
    public float gravityValue = -9.81f;
    [HideInInspector]
    public float normalColliderHeight;
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public PlayerInput playerInput;
    [HideInInspector]
    public Transform cameraTransform;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Vector3 playerVelocity;


    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;

        movementSM = new StateMachine();
        standing = new StandingState(this, movementSM);
        jumping = new JumpingState(this, movementSM);
        crouching = new CrouchingState(this, movementSM);
        landing = new LandingState(this, movementSM);
        sprinting = new SprintState(this, movementSM);
        sprintJumping = new SprintJumpState(this, movementSM);
        combatting = new CombatState(this, movementSM);
        attacking = new AttackState(this, movementSM);

        movementSM.Initialize(standing);

        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;

        // Migration from player controller
        interactAction = playerInput.actions["Interact"];
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        movementSM.currentState.HandleInput();

        movementSM.currentState.LogicUpdate();

        if (interactAction.WasPressedThisFrame())
        {
            Interact();
        }
    }

    private void FixedUpdate()
    {
        movementSM.currentState.PhysicsUpdate();
    }

    // Migration from player controller
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
        print("Dodged");
    }
    public void BlastOff()
    {
        print("Killed");
    }

    // TEMPORARY FOR DEMONSTRATING SAVE LOAD
    public void LoadData(SaveData saveData)
    {
        transform.position = saveData.playerPos;
    }

    // TEMPORARY FOR DEMONSTRATING SAVE LOAD
    public void SaveData(ref SaveData saveData)
    {
        saveData.playerPos = transform.position;
        saveData.quests["Quest 1"] = (int)transform.position.x;
        saveData.quests["Quest 2"] = (int)transform.position.z;
    }
}