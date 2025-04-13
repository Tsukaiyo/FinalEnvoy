using Unity.AppUI.UI;
using UnityEngine;

public class PrincessCommander : MonoBehaviour
{
    public bool isGivingCommand = false;
    public PrincessCommand currentCommand;
    public Vector3 targetLocation;
    public float radius = 0.5f;
    public GameObject targetMarkerPrefab;  // Assign a cylinder prefab in the Inspector
    private GameObject currentTargetMarker;
    private GameObject cursorFollowTargetMarker;

    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isGivingCommand = !isGivingCommand;  // Toggle command mode
            currentCommand = isGivingCommand ? PrincessCommand.MoveToLocation : PrincessCommand.None;
            Debug.Log("PRINCESS MOVE CMD");

            if (!isGivingCommand)
            {
                //Destroy();  // Remove cylinder if command mode is off
            }
            
            //cursorFollowTargetMarker = InstantiateTargetMarker(targetMarkerPrefab, targetLocation);
        }


        if (isGivingCommand && currentCommand == PrincessCommand.MoveToLocation)
        {
            if (Input.GetMouseButtonDown(1)) // Right Mouse Button (RMB) Place target marker
            {
                
                targetLocation = GetCursorWorldPosition();
                Debug.Log("Target Location for Princess: " + targetLocation);
                Debug.DrawRay(transform.position,  targetLocation - transform.position, Color.cyan,0.5f);

                //currentTargetMarker = InstantiateTargetMarker(targetMarkerPrefab, targetLocation);
            }

            // Move cylinder with cursor
            if (currentTargetMarker != null)
            {
                //InstantiateTargetMarker(cursorFollowTargetMarker, targetLocation);

                Vector3 cursorPosition = GetCursorWorldPosition();
                if (cursorPosition != Vector3.zero)
                {
                    cursorFollowTargetMarker.transform.position = cursorPosition;
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && currentTargetMarker != null) // Cancel cylinder on RMB press
        {
            //DestroyTargetMarker(currentTargetMarker);
           // DestroyTargetMarker(cursorFollowTargetMarker);

        }
    }

   
    public enum PrincessCommand
    {
        None,
        MoveToLocation,
        Return,
        Interact
    }

    Vector3 GetCursorWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 20))
        {
            return hit.point; 
        }
        return Vector3.zero; 
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(targetLocation, radius);
    }
}


