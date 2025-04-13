using UnityEngine;

public class TestInputs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            CombatEventPublisher.TestEventCall();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            CombatEventPublisher.TakeDamageEventCall(this.gameObject,2);
        }
    }
}
