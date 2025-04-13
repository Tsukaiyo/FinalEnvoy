using UnityEngine;

public class ExplodingBarrel_QTE : MonoBehaviour
{
    // INSTRUCTIONS/INFO:
    // - When the Player enters the trigger zone, a QTE pops up and the player dodges or gets blown into the air (depending on the QTE outcome).
    // - This script subscribes its methods to the QTE script's event handlers so QTE script can determine the correct outcome.

    [SerializeField] private QTE qteScript; // QTE System script
    [SerializeField] private Character player; // Change the type to whichever script you are attaching this to (should probably be PlayerMovement).
    private bool isExplosive; // The barrels are untouched and able to explode (true), or the barrels are exploded (false).

    // *** Remove this when Initialize() is correctly implemented in the GameManager script (in Start()). ***
    void Start()
    {
        Initialize();
    }

    // Put this in Start() in the GameManager (assuming there is a GameManager script) script.
    public void Initialize()
    {
        // Add "QTE" tag to the GameManager GameObject or, use the tag corresponding to the GameManager in the below code instead.
        qteScript = GameObject.FindWithTag("QTE").GetComponent<QTE>(); 
        player = GameObject.FindWithTag("Player").GetComponent<Character>(); // Add "Player" tag to the Player GameObject
        isExplosive = true;
    }

    // Method to subscribe to the QTE SUCCESS Event (in QTE script)
    private void OnQTESuccessHandler()
    {
        player.Dodge();
        isExplosive = false;
    }

    // Method to subscribe to the QTE FAIL Event
    private void OnQTEFailHandler()
    {
        player.BlastOff();
        isExplosive = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isExplosive)
        {
            qteScript.OnQTESuccess += OnQTESuccessHandler; // Subscribe the SUCCESS method to the QTE Script so it can be invoked
            qteScript.OnQTEFail += OnQTEFailHandler; // Subscribe the FAIL method to the QTE Script so it can be invoked
            qteScript.PlayQTE(KeyCode.Q, 0.75f); // Play the QTE
        }
    }
}
