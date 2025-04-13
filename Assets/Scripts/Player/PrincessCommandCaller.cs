using JetBrains.Annotations;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;

public class CommandSender : MonoBehaviour
{
    [SerializeField] GameObject receiver;
    [SerializeField] bool isCommanding = false;
    [SerializeField] bool isPreparingMoveCommand = false;
    [SerializeField] CommandType commandType;
    [SerializeField] float commandRange;
    int commandId;
    CommandManager commandManager;

    public Vector3 targetLocation;
    public float radius = 0.5f;
    public GameObject targetMarkerPrefab;  // Assign a cylinder prefab in the Inspector
    private GameObject currentTargetMarker;
    private GameObject cursorFollowTargetMarker;



    Transform commandMenu;
    Camera camera;
    Vector3 height;

    public enum CommandType
    {
        Follow,
        Wait,
        MoveToLocation,
        None
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //commandManager = GameObject.Find("CommandManager").GetComponent<CommandManager>();
        commandManager = CommandManager.instance;

        height = new Vector3(0, 3.5f, 0);
        camera = FindAnyObjectByType<Camera>();
        commandMenu = transform.Find("Command Menu");
        commandMenu.gameObject.SetActive(false);

        CommandManager.OnCommandToggle += ToggleCommand;
        commandType = CommandType.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (commandMenu != null)        //Command Menu faces camera
        {
            commandMenu.transform.rotation = camera.transform.rotation;
            commandMenu.transform.position = commandMenu.parent.transform.position + height;
        }
       

        if (Input.GetKeyDown(KeyCode.F))        //Open and close command menu
        {
            commandId = 0;
            isCommanding = !isCommanding;
            AbortMoveCommand();
            ToggleCommandMenu();
            //commandManager.OnCommandToggleCall(this.gameObject, receiver, isCommanding);
        }
        if (isCommanding)
        {
            
            if (Input.GetKeyDown(KeyCode.V) & !isPreparingMoveCommand)        //Move option selector
            {
                commandId += 1;
                if (commandId > 2) commandId = 0; 
                MoveSelector();
            }
            if(Input.GetKeyDown(KeyCode.B) | isPreparingMoveCommand)         //Confirm selection
            {
                if (commandId != 2) isCommanding = false;
                
                ToggleCommandMenu();
                Debug.Log("COMMAND: " + commandId);
              
                SendCommand(commandId);
                
            }
        }
    }

    void ToggleCommand(GameObject sender, GameObject receiver, bool isCommandToggleOn)
    {
        Debug.Log($"Is {sender.name} giving command to {receiver.name}? " + isCommandToggleOn);
        
    }
   
   void ToggleCommandMenu()
   {
        commandMenu.gameObject.SetActive(isCommanding);
        if (isPreparingMoveCommand) commandMenu.gameObject.SetActive(false);
        if (commandMenu.gameObject.activeInHierarchy)
        {
            RectTransform selectorRectTransform = commandMenu.Find("Selector").GetComponent<RectTransform>();
            Vector3 selectorLocalPos = selectorRectTransform.localPosition;
            selectorLocalPos.y = 0.75f;
            selectorRectTransform.localPosition = selectorLocalPos;
        }       // reset menu to top option after toggle on
      
    }
    void MoveSelector()
    {
        //y-values found thru trial and error for each change to menu dimensions
        float selectorMoveDistance = -0.5f;

        RectTransform selectorRectTransform = commandMenu.Find("Selector").GetComponent<RectTransform>();

        Vector3 selectorLocalPos = selectorRectTransform.localPosition;     //move selector down

        selectorLocalPos.y += selectorMoveDistance;
        
        if (selectorLocalPos.y < -0.5f)
        {
            selectorLocalPos.y = 0.75f;              //reset position
        }

        // Apply the new position
        selectorRectTransform.localPosition = selectorLocalPos;
    }
    void SendCommand(int commandId)
    {
        switch(commandId)
        {
            case 0:
                commandManager.CharacterFollowCall(this.gameObject);
                commandManager.OnCommandToggleCall(this.gameObject, receiver, isCommanding);
                break;

            case 1: 
                commandManager.CharacterWaitEventCall(this.gameObject);
                commandManager.OnCommandToggleCall(this.gameObject, receiver, isCommanding);
                break;
            case 2:
                isPreparingMoveCommand = true;
                PrepareMoveCommand();
                break;
                
            default: break;
        }
    }
    void PrepareMoveCommand()
    {
        Vector3 cursorPosition = GetCursorHoverWorldPosition();

        if (cursorFollowTargetMarker == null)
        {
            cursorFollowTargetMarker = CreateTargetMarker(targetMarkerPrefab, cursorPosition);
        }
        if (cursorPosition != Vector3.zero)
        {
            cursorFollowTargetMarker.transform.position = cursorPosition + Vector3.up * 0.5f;
        }
        if (Input.GetMouseButtonDown(1)) // RMB to move princess to target location
        {
            isPreparingMoveCommand = false;
            isCommanding = false;
            commandManager.OnCommandToggleCall(this.gameObject, receiver, isCommanding);
            targetLocation = GetCursorClickWorldPosition();
            Debug.Log("Target Location for Princess: " + targetLocation);
            currentTargetMarker = CreateTargetMarker(currentTargetMarker, targetLocation);
            currentTargetMarker.GetComponent<CapsuleCollider>().enabled = true;
            currentTargetMarker.GetComponent<CapsuleCollider>().isTrigger = true;       //allows princess to destroy upon collision
            //currentTargetMarker.transform.position = targetLocation;
            commandManager.CharacterMoveToLocation(this.gameObject, currentTargetMarker.transform.position);
            //currentTargetMarker.GetComponent<CapsuleCollider>().enabled = true;
            DestroyTargetMarker(cursorFollowTargetMarker);
            commandManager.OnCommandToggleCall(this.gameObject, receiver, isCommanding);
        }
    }
    void AbortMoveCommand()
    {
        isPreparingMoveCommand = false;  
        DestroyTargetMarker(cursorFollowTargetMarker);   
    }
    Vector3 GetCursorHoverWorldPosition()
    {
        Ray camToGroundRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit camToGroundHit;

        if (Physics.Raycast(camToGroundRay, out camToGroundHit, Mathf.Infinity)) // Raycast to ground
        {
            return camToGroundHit.point;
        }
        return Vector3.zero; // Default if no hit
    }

    Vector3 GetCursorClickWorldPosition()
    {
        Ray camToGroundRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit camToGroundHit;
       // int groundLayerMask = LayerMask.GetMask("Floor"); // Ensure the ground is in this layer
       

        // Step 1: Raycast from the camera to detect the ground
        if (Physics.Raycast(camToGroundRay, out camToGroundHit, Mathf.Infinity))
        {
            Vector3 clickedPosition = camToGroundHit.point;
            Debug.DrawRay(Camera.main.transform.position, clickedPosition - Camera.main.transform.position, Color.green, 2f);

            // Step 2: Check if the clicked position is within range
            if (Vector3.Distance(transform.position, clickedPosition) <= commandRange)
            {
                return clickedPosition; // Return the exact clicked position
            }
            else
            {
                Debug.Log("Clicked position is out of range.");
            }
        }
        else
        {
            Debug.Log("No ground hit.");
        }

        return Vector3.zero; // Return default if no valid position found
    }

    GameObject CreateTargetMarker(GameObject targetMarker, Vector3 position)
    {
        GameObject newTargetMarker = Instantiate(targetMarkerPrefab, position + Vector3.up * 0.5f, Quaternion.identity);
        
        //newTargetMarker.transform.position = position + Vector3.up * 0.5f;
        return newTargetMarker;

    }

    void DestroyTargetMarker(GameObject targetMarker)
    {
        if (targetMarker != null)
        {
            Destroy(targetMarker);
            targetMarker = null;
        }
    }
}
