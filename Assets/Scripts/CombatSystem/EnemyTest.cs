using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<EntityTest>().PrepareToTakeDamage();
            CombatEventPublisher.TakeDamageEventCall(player, 2);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            GameObject princess = GameObject.FindGameObjectWithTag("Princess");
            princess.GetComponent<EntityTest>().PrepareToTakeDamage();
            CombatEventPublisher.TakeDamageEventCall(princess,2);
        }
    }
}
