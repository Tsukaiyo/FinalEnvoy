using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements.Experimental;

public class EntityTest : MonoBehaviour, IDamageable
{
    public float health = 10;
    public GameObject ragdoll;
    private bool isIssuingCommand = false;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        DamageArgs args = new DamageArgs(10f);
        CombatEventPublisher.testEvent += EntityTestEvent;
        //CombatEventPublisher.OnCharacterDamaged += TakeDamage;
        
    }
    public void PrepareToTakeDamage()
    {
        CombatEventPublisher.OnCharacterDamaged -= TakeDamage;
        CombatEventPublisher.OnCharacterDamaged += TakeDamage;

        CombatEventPublisher.OnCharacterDeath -= Die;
        CombatEventPublisher.OnCharacterDeath += Die;
    }

    public void EntityTestEvent(object sender, EventArgs e)
    {
        Debug.Log("COMBAT EVENT TEST");
    }
    public void TakeDamage(GameObject target,float damage)
    {
        if (target != this.gameObject)
        {
            Debug.Log("Wrong Target...");
            return;
        }
       // animator.SetTrigger("damage");
        health -= damage;
        CombatEventPublisher.OnCharacterDamaged -= TakeDamage;
    }
    public void Die(GameObject target, GameObject g)
    {
        if (target != this.gameObject)
        {
            Debug.Log("Wrong Target...No one dies");
            return;
        }
        Debug.Log("DEAD");
        Instantiate(g, transform.position, transform.rotation);
        CombatEventPublisher.OnCharacterDeath -= Die;
        Destroy(this.gameObject);
        
    }
    private void OnDisable()
    {
        CombatEventPublisher.testEvent -= EntityTestEvent;
        CombatEventPublisher.OnCharacterDeath -= Die;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            CombatEventPublisher.DeathEventCall(this.gameObject,ragdoll);
        }
    }

}

public class DamageArgs : EventArgs
{
    public float damage { get; set; }
    public DamageArgs(float damage) { this.damage = damage; }
}

