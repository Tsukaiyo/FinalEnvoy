using UnityEngine;

//Summary:
//Sets isKinematic variable of RB component of the falling object to True upon interact() call
//Toggle prompt visibility depending on Player distance

public class FallingObjectInteractable : MonoBehaviour, IInteractable
{
    GameObject fallingObject;
    Rigidbody fallingRB;
    InteractPromptUI interactPromptUI;
    [SerializeField] GameObject player;
    [SerializeField] float promptRange;
    [SerializeField] string fallingObjectName;
    [SerializeField] string interactPromptText;
    public string Prompt => interactPromptText;
    void Start()
    {
        fallingObject = GameObject.Find(fallingObjectName);
        
        if (fallingObject == null)
        {
            Debug.Log("No falling object assigned.");
        }
        else
        {
            fallingRB = fallingObject.GetComponent<Rigidbody>();
        }

        if (fallingRB == null)
        {
            Debug.Log($"{fallingObject.name} has no RigidBody component.");
        }
        else
        {
            fallingRB.isKinematic = true;
        }

        interactPromptUI = GetComponentInChildren<InteractPromptUI>();  //must have child w/ this component active 
                                                                        //closeprompt() in InteractPromptUI start() will cause it to be null

        if (interactPromptUI == null)
        {
            Debug.Log($"{fallingObject.name} has no interact prompt UI.");
        }
    }
    void Update()
    {
        if( interactPromptUI != null)
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) < promptRange)
            {
                interactPromptUI.OpenPrompt(Prompt);
            }
            else
            {
                interactPromptUI.ClosePrompt();
            }
        }
    }
   public void interact(GameObject interactor)
   {
        fallingRB.isKinematic = false;
        Debug.Log("Falling Object ....");
   }
}
