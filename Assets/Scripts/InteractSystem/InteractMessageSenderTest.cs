using UnityEngine;

public class InteractMessageSenderTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MonoBehaviour[] allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        foreach (var obj in allObjects)
        {
            if (obj is IInteractable interactable)
            {
                interactable.interact(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
